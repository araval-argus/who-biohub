using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.UpdateSpecimenType;

public class UpdateSpecimenTypeCommandValidator : AbstractValidator<UpdateSpecimenTypeCommand>
{
    public UpdateSpecimenTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}