using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTARequests.ListSMTARequests;

public class ListSMTARequestsQueryValidator : AbstractValidator<ListSMTARequestsQuery>
{
    public ListSMTARequestsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}