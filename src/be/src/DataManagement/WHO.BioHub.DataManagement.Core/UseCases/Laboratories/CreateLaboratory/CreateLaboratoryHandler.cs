using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratory;

public interface ICreateLaboratoryHandler
{
    Task<Either<CreateLaboratoryCommandResponse, Errors>> Handle(CreateLaboratoryCommand command, CancellationToken cancellationToken);
}

public class CreateLaboratoryHandler : ICreateLaboratoryHandler
{
    private readonly ILogger<CreateLaboratoryHandler> _logger;
    private readonly CreateLaboratoryCommandValidator _validator;
    private readonly ICreateLaboratoryMapper _mapper;
    private readonly ILaboratoryWriteRepository _writeRepository;

    public CreateLaboratoryHandler(
        ILogger<CreateLaboratoryHandler> logger,
        CreateLaboratoryCommandValidator validator,
        ICreateLaboratoryMapper mapper,
        ILaboratoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateLaboratoryCommandResponse, Errors>> Handle(
        CreateLaboratoryCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        Laboratory laboratory = _mapper.Map(command);

        try
        {
            Either<Laboratory, Errors> response = await _writeRepository.Create(laboratory, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            Laboratory createdLaboratory =
                response.Left ?? throw new Exception("This is a bug: laboratory value must be defined");
            return new(new CreateLaboratoryCommandResponse(response.Left.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Laboratory");
            throw;
        }
    }
}