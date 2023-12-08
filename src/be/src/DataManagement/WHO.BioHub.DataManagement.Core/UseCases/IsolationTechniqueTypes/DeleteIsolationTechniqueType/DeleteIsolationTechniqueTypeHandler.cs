using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.DeleteIsolationTechniqueType;

public interface IDeleteIsolationTechniqueTypeHandler
{
    Task<Either<DeleteIsolationTechniqueTypeCommandResponse, Errors>> Handle(DeleteIsolationTechniqueTypeCommand command, CancellationToken cancellationToken);
}

public class DeleteIsolationTechniqueTypeHandler : IDeleteIsolationTechniqueTypeHandler
{
    private readonly ILogger<DeleteIsolationTechniqueTypeHandler> _logger;
    private readonly DeleteIsolationTechniqueTypeCommandValidator _validator;
    private readonly IIsolationTechniqueTypeWriteRepository _writeRepository;

    public DeleteIsolationTechniqueTypeHandler(
        ILogger<DeleteIsolationTechniqueTypeHandler> logger,
        DeleteIsolationTechniqueTypeCommandValidator validator,
        IIsolationTechniqueTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteIsolationTechniqueTypeCommandResponse, Errors>> Handle(
        DeleteIsolationTechniqueTypeCommand command,
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

            return new(new DeleteIsolationTechniqueTypeCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the IsolationTechniqueType with {id}", command.Id);
            throw;
        }
    }
}