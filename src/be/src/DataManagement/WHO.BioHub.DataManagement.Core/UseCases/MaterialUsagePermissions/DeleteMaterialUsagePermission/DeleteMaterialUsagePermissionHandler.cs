using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.DeleteMaterialUsagePermission;

public interface IDeleteMaterialUsagePermissionHandler
{
    Task<Either<DeleteMaterialUsagePermissionCommandResponse, Errors>> Handle(DeleteMaterialUsagePermissionCommand command, CancellationToken cancellationToken);
}

public class DeleteMaterialUsagePermissionHandler : IDeleteMaterialUsagePermissionHandler
{
    private readonly ILogger<DeleteMaterialUsagePermissionHandler> _logger;
    private readonly DeleteMaterialUsagePermissionCommandValidator _validator;
    private readonly IMaterialUsagePermissionWriteRepository _writeRepository;

    public DeleteMaterialUsagePermissionHandler(
        ILogger<DeleteMaterialUsagePermissionHandler> logger,
        DeleteMaterialUsagePermissionCommandValidator validator,
        IMaterialUsagePermissionWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _writeRepository = writeRepository;
    }

    public async Task<Either<DeleteMaterialUsagePermissionCommandResponse, Errors>> Handle(
        DeleteMaterialUsagePermissionCommand command,
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

            return new(new DeleteMaterialUsagePermissionCommandResponse());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting the MaterialUsagePermission with {id}", command.Id);
            throw;
        }
    }
}