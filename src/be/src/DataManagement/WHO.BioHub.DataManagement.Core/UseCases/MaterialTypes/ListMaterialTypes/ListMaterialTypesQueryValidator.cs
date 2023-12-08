using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.ListMaterialTypes;

public class ListMaterialTypesQueryValidator : AbstractValidator<ListMaterialTypesQuery>
{
    public ListMaterialTypesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}