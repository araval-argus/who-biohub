using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.CreateCultivabilityType;

public class CreateCultivabilityTypeCommandValidator : AbstractValidator<CreateCultivabilityTypeCommand>
{
    public CreateCultivabilityTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}