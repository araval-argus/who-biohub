using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.DeletePriorityRequestType;

public class DeletePriorityRequestTypeCommandValidator : AbstractValidator<DeletePriorityRequestTypeCommand>
{
    public DeletePriorityRequestTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}