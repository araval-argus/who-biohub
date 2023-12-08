using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.Shipments.ReadShipment;

public class ReadShipmentQueryValidator : AbstractValidator<ReadShipmentQuery>
{
    public ReadShipmentQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}