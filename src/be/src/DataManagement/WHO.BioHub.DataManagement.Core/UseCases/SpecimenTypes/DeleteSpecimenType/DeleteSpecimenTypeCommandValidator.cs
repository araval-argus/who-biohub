using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.DeleteSpecimenType;

public class DeleteSpecimenTypeCommandValidator : AbstractValidator<DeleteSpecimenTypeCommand>
{
    public DeleteSpecimenTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}