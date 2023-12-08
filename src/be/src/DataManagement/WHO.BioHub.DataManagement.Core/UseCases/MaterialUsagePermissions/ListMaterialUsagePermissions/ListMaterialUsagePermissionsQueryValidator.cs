using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialUsagePermissions.ListMaterialUsagePermissions;

public class ListMaterialUsagePermissionsQueryValidator : AbstractValidator<ListMaterialUsagePermissionsQuery>
{
    public ListMaterialUsagePermissionsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}