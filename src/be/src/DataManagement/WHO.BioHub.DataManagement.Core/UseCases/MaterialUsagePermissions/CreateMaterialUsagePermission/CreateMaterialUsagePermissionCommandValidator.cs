using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.CreateMaterialUsagePermission;

public class CreateMaterialUsagePermissionCommandValidator : AbstractValidator<CreateMaterialUsagePermissionCommand>
{
    public CreateMaterialUsagePermissionCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}