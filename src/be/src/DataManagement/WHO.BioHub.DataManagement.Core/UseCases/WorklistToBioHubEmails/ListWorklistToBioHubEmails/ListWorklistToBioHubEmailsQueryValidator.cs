using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.ListWorklistToBioHubEmails;

public class ListWorklistToBioHubEmailsQueryValidator : AbstractValidator<ListWorklistToBioHubEmailsQuery>
{
    public ListWorklistToBioHubEmailsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}