using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.CreateShipment;

public class CreateShipmentCommandValidator : AbstractValidator<CreateShipmentCommand>
{
    public CreateShipmentCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}