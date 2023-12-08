using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratory;

public class CreateLaboratoryCommandValidator : AbstractValidator<CreateLaboratoryCommand>
{
    public CreateLaboratoryCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty().WithMessage("'Name' is required");

        RuleFor(cmd => cmd.Latitude)
            .NotNull().WithMessage("'Latitude' is required");

        RuleFor(cmd => cmd.Longitude)
            .NotNull().WithMessage("'Longitude' is required");
    }
}