using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.DeleteMaterialUsagePermission;

public class DeleteMaterialUsagePermissionCommandValidator : AbstractValidator<DeleteMaterialUsagePermissionCommand>
{
    public DeleteMaterialUsagePermissionCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}