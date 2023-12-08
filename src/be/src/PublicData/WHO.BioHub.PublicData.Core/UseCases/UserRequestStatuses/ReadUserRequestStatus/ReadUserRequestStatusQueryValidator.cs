using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequestStatuses.ReadUserRequestStatus;

public class ReadUserRequestStatusQueryValidator : AbstractValidator<ReadUserRequestStatusQuery>
{
    public ReadUserRequestStatusQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}