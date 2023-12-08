using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialUsagePermissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.CreateMaterialUsagePermission;

public interface ICreateMaterialUsagePermissionHandler
{
    Task<Either<CreateMaterialUsagePermissionCommandResponse, Errors>> Handle(CreateMaterialUsagePermissionCommand command, CancellationToken cancellationToken);
}

public class CreateMaterialUsagePermissionHandler : ICreateMaterialUsagePermissionHandler
{
    private readonly ILogger<CreateMaterialUsagePermissionHandler> _logger;
    private readonly CreateMaterialUsagePermissionCommandValidator _validator;
    private readonly ICreateMaterialUsagePermissionMapper _mapper;
    private readonly IMaterialUsagePermissionWriteRepository _writeRepository;

    public CreateMaterialUsagePermissionHandler(
        ILogger<CreateMaterialUsagePermissionHandler> logger,
        CreateMaterialUsagePermissionCommandValidator validator,
        ICreateMaterialUsagePermissionMapper mapper,
        IMaterialUsagePermissionWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateMaterialUsagePermissionCommandResponse, Errors>> Handle(
        CreateMaterialUsagePermissionCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        MaterialUsagePermission materialusagepermission = _mapper.Map(command);

        try
        {
            Either<MaterialUsagePermission, Errors> response = await _writeRepository.Create(materialusagepermission, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            MaterialUsagePermission createdMaterialUsagePermission =
                response.Left ?? throw new Exception("This is a bug: materialusagepermission value must be defined");
            return new(new CreateMaterialUsagePermissionCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialUsagePermission");
            throw;
        }
    }
}