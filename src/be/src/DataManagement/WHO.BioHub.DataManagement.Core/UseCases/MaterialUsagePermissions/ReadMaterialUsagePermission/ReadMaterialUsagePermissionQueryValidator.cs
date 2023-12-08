using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.ReadMaterialUsagePermission;

public class ReadMaterialUsagePermissionQueryValidator : AbstractValidator<ReadMaterialUsagePermissionQuery>
{
    public ReadMaterialUsagePermissionQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}