using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUserByExternalId;

public class ReadUserByExternalIdQueryValidator : AbstractValidator<ReadUserByExternalIdQuery>
{
    public ReadUserByExternalIdQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}