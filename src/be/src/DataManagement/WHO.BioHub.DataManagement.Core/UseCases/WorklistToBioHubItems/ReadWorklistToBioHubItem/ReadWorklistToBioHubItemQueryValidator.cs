using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ReadWorklistToBioHubItem;

public class ReadWorklistToBioHubItemQueryValidator : AbstractValidator<ReadWorklistToBioHubItemQuery>
{
    public ReadWorklistToBioHubItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}