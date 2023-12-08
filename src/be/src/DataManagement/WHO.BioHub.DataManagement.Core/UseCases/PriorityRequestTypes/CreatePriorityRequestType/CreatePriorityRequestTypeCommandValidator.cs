using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.CreatePriorityRequestType;

public class CreatePriorityRequestTypeCommandValidator : AbstractValidator<CreatePriorityRequestTypeCommand>
{
    public CreatePriorityRequestTypeCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}