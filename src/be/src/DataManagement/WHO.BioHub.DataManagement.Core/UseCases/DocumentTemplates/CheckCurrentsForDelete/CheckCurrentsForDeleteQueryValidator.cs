using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CheckCurrentsForDelete;

public class CheckCurrentsForDeleteQueryValidator : AbstractValidator<CheckCurrentsForDeleteQuery>
{
    public CheckCurrentsForDeleteQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}