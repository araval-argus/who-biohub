using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.ListSpecimenTypes;

public class ListSpecimenTypesQueryValidator : AbstractValidator<ListSpecimenTypesQuery>
{
    public ListSpecimenTypesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}