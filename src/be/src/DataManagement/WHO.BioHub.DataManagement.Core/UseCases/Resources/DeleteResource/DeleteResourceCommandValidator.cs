using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.DeleteResource;

public class DeleteResourceCommandValidator : AbstractValidator<DeleteResourceCommand>
{
    public DeleteResourceCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}