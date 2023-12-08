using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequests.ReadUserRequest;

public class ReadUserRequestQueryValidator : AbstractValidator<ReadUserRequestQuery>
{
    public ReadUserRequestQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}