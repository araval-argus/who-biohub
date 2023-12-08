using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.ShipmentRequests.ListShipmentRequests;

public class ListShipmentRequestsQueryValidator : AbstractValidator<ListShipmentRequestsQuery>
{
    public ListShipmentRequestsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}