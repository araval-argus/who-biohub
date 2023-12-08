using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.ReadPriorityRequestType;

public class ReadPriorityRequestTypeQueryValidator : AbstractValidator<ReadPriorityRequestTypeQuery>
{
    public ReadPriorityRequestTypeQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}