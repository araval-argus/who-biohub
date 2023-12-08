using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListLaboratories;

public class ListLaboratoriesQueryValidator : AbstractValidator<ListLaboratoriesQuery>
{
    public ListLaboratoriesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}