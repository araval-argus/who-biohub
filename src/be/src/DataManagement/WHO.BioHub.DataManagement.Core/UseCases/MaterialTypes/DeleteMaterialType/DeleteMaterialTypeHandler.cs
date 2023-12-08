using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.MaterialTypes;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.DeleteMaterialType;

public interface IDeleteMaterialTypeHandler
{
    Task<Either<DeleteMaterialTypeCommandResponse, Errors>> Handle(DeleteMaterialTypeCommand command, CancellationToken cancellationToken);
}

public class DeleteMaterialTypeHandler : IDeleteMaterialTypeHandler
{
    private readonly ILogger<DeleteMaterialTypeHandler> _logger;
    private readonly DeleteMaterialTypeCommandValidator _validator;
    private readonly IMaterialTypeWriteRepository _writeRepository;

    public DeleteMaterialTypeHandler(
        ILogger<DeleteMaterialTypeHandler> logger,
        DeleteMaterialTypeCommandValidator validator,
        IMaterialTypeWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteMaterialTypeCommandResponse, Errors>> Handle(
        DeleteMaterialTypeCommand command,
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

            return new(new DeleteMaterialTypeCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the MaterialType with {id}", command.Id);
            throw;
        }
    }
}