using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.PriorityRequestTypes.ReadPriorityRequestType;

public class ReadPriorityRequestTypeQueryValidator : AbstractValidator<ReadPriorityRequestTypeQuery>
{
    public ReadPriorityRequestTypeQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}