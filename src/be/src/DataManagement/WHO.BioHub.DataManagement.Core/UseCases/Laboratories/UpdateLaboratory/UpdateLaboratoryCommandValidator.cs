using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.UpdateLaboratory;

public class UpdateLaboratoryCommandValidator : AbstractValidator<UpdateLaboratoryCommand>
{
    public UpdateLaboratoryCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty().WithMessage("'Name' is required");

        RuleFor(cmd => cmd.Latitude)
            .NotNull().WithMessage("'Latitude' is required");

        RuleFor(cmd => cmd.Longitude)
            .NotNull().WithMessage("'Longitude' is required");
    }
}