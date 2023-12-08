using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.SpecimenTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.DeleteSpecimenType;

public interface IDeleteSpecimenTypeHandler
{
    Task<Either<DeleteSpecimenTypeCommandResponse, Errors>> Handle(DeleteSpecimenTypeCommand command, CancellationToken cancellationToken);
}

public class DeleteSpecimenTypeHandler : IDeleteSpecimenTypeHandler
{
    private readonly ILogger<DeleteSpecimenTypeHandler> _logger;
    private readonly DeleteSpecimenTypeCommandValidator _validator;
    private readonly ISpecimenTypeWriteRepository _writeRepository;

    public DeleteSpecimenTypeHandler(
        ILogger<DeleteSpecimenTypeHandler> logger,
        DeleteSpecimenTypeCommandValidator validator,
        ISpecimenTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteSpecimenTypeCommandResponse, Errors>> Handle(
        DeleteSpecimenTypeCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            Errors? errors = await _writeRepository.Delete(command.Id, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new DeleteSpecimenTypeCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the SpecimenType with {id}", command.Id);
            throw;
        }
    }
}