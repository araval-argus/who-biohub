using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.ListUserRequestStatuses;

public class ListUserRequestStatusesQueryValidator : AbstractValidator<ListUserRequestStatusesQuery>
{
    public ListUserRequestStatusesQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}