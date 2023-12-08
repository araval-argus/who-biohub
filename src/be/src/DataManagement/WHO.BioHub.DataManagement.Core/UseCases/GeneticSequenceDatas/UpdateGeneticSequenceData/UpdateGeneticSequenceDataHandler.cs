using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.UpdateGeneticSequenceData;

public interface IUpdateGeneticSequenceDataHandler
{
    Task<Either<UpdateGeneticSequenceDataCommandResponse, Errors>> Handle(UpdateGeneticSequenceDataCommand command, CancellationToken cancellationToken);
}

public class UpdateGeneticSequenceDataHandler : IUpdateGeneticSequenceDataHandler
{
    private readonly ILogger<UpdateGeneticSequenceDataHandler> _logger;
    private readonly UpdateGeneticSequenceDataCommandValidator _validator;
    private readonly IUpdateGeneticSequenceDataMapper _mapper;
    private readonly IGeneticSequenceDataWriteRepository _writeRepository;

    public UpdateGeneticSequenceDataHandler(
        ILogger<UpdateGeneticSequenceDataHandler> logger,
        UpdateGeneticSequenceDataCommandValidator validator,
        IUpdateGeneticSequenceDataMapper mapper,
        IGeneticSequenceDataWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateGeneticSequenceDataCommandResponse, Errors>> Handle(
        UpdateGeneticSequenceDataCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            GeneticSequenceData geneticsequencedata = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            geneticsequencedata = _mapper.Map(geneticsequencedata, command);

            Errors? errors = await _writeRepository.Update(geneticsequencedata, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateGeneticSequenceDataCommandResponse(geneticsequencedata));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new GeneticSequenceData");
            throw;
        }
    }
}