using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.UpdateMaterialUsagePermission;

public interface IUpdateMaterialUsagePermissionHandler
{
    Task<Either<UpdateMaterialUsagePermissionCommandResponse, Errors>> Handle(UpdateMaterialUsagePermissionCommand command, CancellationToken cancellationToken);
}

public class UpdateMaterialUsagePermissionHandler : IUpdateMaterialUsagePermissionHandler
{
    private readonly ILogger<UpdateMaterialUsagePermissionHandler> _logger;
    private readonly UpdateMaterialUsagePermissionCommandValidator _validator;
    private readonly IUpdateMaterialUsagePermissionMapper _mapper;
    private readonly IMaterialUsagePermissionWriteRepository _writeRepository;

    public UpdateMaterialUsagePermissionHandler(
        ILogger<UpdateMaterialUsagePermissionHandler> logger,
        UpdateMaterialUsagePermissionCommandValidator validator,
        IUpdateMaterialUsagePermissionMapper mapper,
        IMaterialUsagePermissionWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateMaterialUsagePermissionCommandResponse, Errors>> Handle(
        UpdateMaterialUsagePermissionCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            MaterialUsagePermission materialusagepermission = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            materialusagepermission = _mapper.Map(materialusagepermission, command);

            Errors? errors = await _writeRepository.Update(materialusagepermission, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateMaterialUsagePermissionCommandResponse(materialusagepermission));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialUsagePermission");
            throw;
        }
    }
}