using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.CanStartSMTA2Request;

public class CanStartSMTARequestQueryValidator : AbstractValidator<CanStartSMTARequestQuery>
{
    public CanStartSMTARequestQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}