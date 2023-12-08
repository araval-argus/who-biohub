using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.EForms.ListEForms;

public class ListEFormsQueryValidator : AbstractValidator<ListEFormsQuery>
{
    public ListEFormsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}