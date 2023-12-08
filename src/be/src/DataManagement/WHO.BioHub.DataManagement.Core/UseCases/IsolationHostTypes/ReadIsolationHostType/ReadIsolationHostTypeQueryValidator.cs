using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.ReadIsolationHostType;

public class ReadIsolationHostTypeQueryValidator : AbstractValidator<ReadIsolationHostTypeQuery>
{
    public ReadIsolationHostTypeQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}