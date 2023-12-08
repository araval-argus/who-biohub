using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.MaterialUsagePermissions.ReadMaterialUsagePermission;

public class ReadMaterialUsagePermissionQueryValidator : AbstractValidator<ReadMaterialUsagePermissionQuery>
{
    public ReadMaterialUsagePermissionQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}