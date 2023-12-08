using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ReadWorklistFromBioHubItem;

public class ReadWorklistFromBioHubItemQueryValidator : AbstractValidator<ReadWorklistFromBioHubItemQuery>
{
    public ReadWorklistFromBioHubItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}