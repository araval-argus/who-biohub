using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.DeleteTemperatureUnitOfMeasure;

public class DeleteTemperatureUnitOfMeasureCommandValidator : AbstractValidator<DeleteTemperatureUnitOfMeasureCommand>
{
    public DeleteTemperatureUnitOfMeasureCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}