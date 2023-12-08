using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.DeleteShipment;

public class DeleteShipmentCommandValidator : AbstractValidator<DeleteShipmentCommand>
{
    public DeleteShipmentCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}