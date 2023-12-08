using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.UpdateMaterialUsagePermission;

public class UpdateMaterialUsagePermissionCommandValidator : AbstractValidator<UpdateMaterialUsagePermissionCommand>
{
    public UpdateMaterialUsagePermissionCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}