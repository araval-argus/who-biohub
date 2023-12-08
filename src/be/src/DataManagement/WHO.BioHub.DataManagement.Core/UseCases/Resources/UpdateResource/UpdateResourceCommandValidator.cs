using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.UpdateResource;

public class UpdateResourceCommandValidator : AbstractValidator<UpdateResourceCommand>
{
    public UpdateResourceCommandValidator()
    {
        RuleFor(cmd => cmd.Name)
            .NotNull()
            .WithMessage("Name is Required")
            .NotEmpty()
            .WithMessage("Name is Required");
    }
}