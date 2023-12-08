using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.ReadShipment;

public class ReadShipmentQueryValidator : AbstractValidator<ReadShipmentQuery>
{
    public ReadShipmentQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}