using FluentValidation;

namespace WHO.BioHub.Search.Core.UseCases.Aggregates.FrontEndGlobalSearch;

public class FrontEndGlobalSearchQueryValidator : AbstractValidator<FrontEndGlobalSearchQuery>
{
    public FrontEndGlobalSearchQueryValidator()
    {
        RuleFor(cmd => cmd.LaboratoryName)
            .NotEmpty().WithMessage("'LaboratoryName' is required");
    }
}