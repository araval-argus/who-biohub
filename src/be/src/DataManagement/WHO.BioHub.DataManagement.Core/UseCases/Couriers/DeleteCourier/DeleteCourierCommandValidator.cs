using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.DeleteCourier;

public class DeleteCourierCommandValidator : AbstractValidator<DeleteCourierCommand>
{
    public DeleteCourierCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}