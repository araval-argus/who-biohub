using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.DeleteCultivabilityType;

public class DeleteCultivabilityTypeCommandValidator : AbstractValidator<DeleteCultivabilityTypeCommand>
{
    public DeleteCultivabilityTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}