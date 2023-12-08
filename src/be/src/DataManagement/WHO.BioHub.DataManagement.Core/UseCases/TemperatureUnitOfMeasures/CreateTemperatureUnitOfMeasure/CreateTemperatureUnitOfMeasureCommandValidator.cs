using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.CreateTemperatureUnitOfMeasure;

public class CreateTemperatureUnitOfMeasureCommandValidator : AbstractValidator<CreateTemperatureUnitOfMeasureCommand>
{
    public CreateTemperatureUnitOfMeasureCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}