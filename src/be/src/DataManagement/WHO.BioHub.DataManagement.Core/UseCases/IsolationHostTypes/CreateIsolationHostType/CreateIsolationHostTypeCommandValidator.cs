using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.CreateIsolationHostType;

public class CreateIsolationHostTypeCommandValidator : AbstractValidator<CreateIsolationHostTypeCommand>
{
    public CreateIsolationHostTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}