using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.ListShipments;

public class ListShipmentsQueryValidator : AbstractValidator<ListShipmentsQuery>
{
    public ListShipmentsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}