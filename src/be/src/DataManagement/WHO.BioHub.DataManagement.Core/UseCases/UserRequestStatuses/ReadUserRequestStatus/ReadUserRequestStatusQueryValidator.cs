using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequestStatuses.ReadUserRequestStatus;

public class ReadUserRequestStatusQueryValidator : AbstractValidator<ReadUserRequestStatusQuery>
{
    public ReadUserRequestStatusQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}