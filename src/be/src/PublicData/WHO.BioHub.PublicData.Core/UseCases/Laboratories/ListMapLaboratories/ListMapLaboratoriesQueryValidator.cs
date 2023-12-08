using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListMapLaboratories;

public class ListMapLaboratoriesQueryValidator : AbstractValidator<ListMapLaboratoriesQuery>
{
    public ListMapLaboratoriesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}