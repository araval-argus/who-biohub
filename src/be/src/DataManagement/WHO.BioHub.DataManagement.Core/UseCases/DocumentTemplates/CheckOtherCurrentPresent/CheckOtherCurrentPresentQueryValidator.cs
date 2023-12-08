using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CheckOtherCurrentPresent;

public class CheckOtherCurrentPresentQueryValidator : AbstractValidator<CheckOtherCurrentPresentQuery>
{
    public CheckOtherCurrentPresentQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}