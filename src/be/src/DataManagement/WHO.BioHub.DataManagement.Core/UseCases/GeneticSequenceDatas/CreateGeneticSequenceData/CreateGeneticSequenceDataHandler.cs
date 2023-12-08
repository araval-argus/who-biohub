using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.CreateGeneticSequenceData;

public interface ICreateGeneticSequenceDataHandler
{
    Task<Either<CreateGeneticSequenceDataCommandResponse, Errors>> Handle(CreateGeneticSequenceDataCommand command, CancellationToken cancellationToken);
}

public class CreateGeneticSequenceDataHandler : ICreateGeneticSequenceDataHandler
{
    private readonly ILogger<CreateGeneticSequenceDataHandler> _logger;
    private readonly CreateGeneticSequenceDataCommandValidator _validator;
    private readonly ICreateGeneticSequenceDataMapper _mapper;
    private readonly IGeneticSequenceDataWriteRepository _writeRepository;

    public CreateGeneticSequenceDataHandler(
        ILogger<CreateGeneticSequenceDataHandler> logger,
        CreateGeneticSequenceDataCommandValidator validator,
        ICreateGeneticSequenceDataMapper mapper,
        IGeneticSequenceDataWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateGeneticSequenceDataCommandResponse, Errors>> Handle(
        CreateGeneticSequenceDataCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        GeneticSequenceData geneticsequencedata = _mapper.Map(command);

        try
        {
            Either<GeneticSequenceData, Errors> response = await _writeRepository.Create(geneticsequencedata, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            GeneticSequenceData createdGeneticSequenceData =
                response.Left ?? throw new Exception("This is a bug: geneticsequencedata value must be defined");
            return new(new CreateGeneticSequenceDataCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new GeneticSequenceData");
            throw;
        }
    }
}