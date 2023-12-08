using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.UpdatePriorityRequestType;

public class UpdatePriorityRequestTypeCommandValidator : AbstractValidator<UpdatePriorityRequestTypeCommand>
{
    public UpdatePriorityRequestTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}