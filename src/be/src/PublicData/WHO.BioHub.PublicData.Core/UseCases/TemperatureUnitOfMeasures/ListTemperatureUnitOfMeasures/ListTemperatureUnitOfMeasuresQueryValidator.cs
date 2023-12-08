using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.TemperatureUnitOfMeasures.ListTemperatureUnitOfMeasures;

public class ListTemperatureUnitOfMeasuresQueryValidator : AbstractValidator<ListTemperatureUnitOfMeasuresQuery>
{
    public ListTemperatureUnitOfMeasuresQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}