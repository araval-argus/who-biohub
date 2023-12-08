using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.MaterialUsagePermissions.ListMaterialUsagePermissions;

public class ListMaterialUsagePermissionsQueryValidator : AbstractValidator<ListMaterialUsagePermissionsQuery>
{
    public ListMaterialUsagePermissionsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}