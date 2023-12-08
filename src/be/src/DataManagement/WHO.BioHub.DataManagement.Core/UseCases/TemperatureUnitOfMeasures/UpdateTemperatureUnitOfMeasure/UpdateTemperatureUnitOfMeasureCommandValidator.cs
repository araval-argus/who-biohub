using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.UpdateTemperatureUnitOfMeasure;

public class UpdateTemperatureUnitOfMeasureCommandValidator : AbstractValidator<UpdateTemperatureUnitOfMeasureCommand>
{
    public UpdateTemperatureUnitOfMeasureCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}