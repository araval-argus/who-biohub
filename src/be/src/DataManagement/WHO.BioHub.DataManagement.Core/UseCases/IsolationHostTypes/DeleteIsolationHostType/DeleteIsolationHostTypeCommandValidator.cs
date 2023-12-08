using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.DeleteIsolationHostType;

public class DeleteIsolationHostTypeCommandValidator : AbstractValidator<DeleteIsolationHostTypeCommand>
{
    public DeleteIsolationHostTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}