using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.UpdateCultivabilityType;

public class UpdateCultivabilityTypeCommandValidator : AbstractValidator<UpdateCultivabilityTypeCommand>
{
    public UpdateCultivabilityTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}