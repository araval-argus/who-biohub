using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ListUserRequests;

public class ListUserRequestsQueryValidator : AbstractValidator<ListUserRequestsQuery>
{
    public ListUserRequestsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}