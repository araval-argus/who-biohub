using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.CreateSpecimenType;

public class CreateSpecimenTypeCommandValidator : AbstractValidator<CreateSpecimenTypeCommand>
{
    public CreateSpecimenTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}