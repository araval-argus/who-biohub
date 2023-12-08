using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.Shipments.ListShipments;

public class ListShipmentsQueryValidator : AbstractValidator<ListShipmentsQuery>
{
    public ListShipmentsQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}