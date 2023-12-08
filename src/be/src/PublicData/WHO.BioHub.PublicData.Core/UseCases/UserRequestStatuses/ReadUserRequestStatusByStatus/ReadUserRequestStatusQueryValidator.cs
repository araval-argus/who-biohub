using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.UserRequestStatuses.ReadUserRequestStatus;

public class ReadUserRequestStatusByStatusQueryValidator : AbstractValidator<ReadUserRequestStatusByStatusQuery>
{
    public ReadUserRequestStatusByStatusQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}