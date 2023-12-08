using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.TemperatureUnitOfMeasures.ReadTemperatureUnitOfMeasure;

public class ReadTemperatureUnitOfMeasureQueryValidator : AbstractValidator<ReadTemperatureUnitOfMeasureQuery>
{
    public ReadTemperatureUnitOfMeasureQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}