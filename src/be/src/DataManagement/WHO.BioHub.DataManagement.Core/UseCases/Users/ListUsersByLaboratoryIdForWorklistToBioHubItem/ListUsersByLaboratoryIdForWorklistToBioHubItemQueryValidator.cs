using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByLaboratoryId;

public class ListUsersByLaboratoryIdForWorklistToBioHubItemQueryValidator : AbstractValidator<ListUsersByLaboratoryIdForWorklistToBioHubItemQuery>
{
    public ListUsersByLaboratoryIdForWorklistToBioHubItemQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}