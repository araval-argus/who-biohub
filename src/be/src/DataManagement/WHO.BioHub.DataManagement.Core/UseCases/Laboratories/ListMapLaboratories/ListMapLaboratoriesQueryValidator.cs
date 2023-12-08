using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ListMapLaboratories;

public class ListMapLaboratoriesQueryValidator : AbstractValidator<ListMapLaboratoriesQuery>
{
    public ListMapLaboratoriesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}