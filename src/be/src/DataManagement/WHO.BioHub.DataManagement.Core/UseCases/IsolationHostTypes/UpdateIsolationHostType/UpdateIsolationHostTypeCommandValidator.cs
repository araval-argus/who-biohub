using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.UpdateIsolationHostType;

public class UpdateIsolationHostTypeCommandValidator : AbstractValidator<UpdateIsolationHostTypeCommand>
{
    public UpdateIsolationHostTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}