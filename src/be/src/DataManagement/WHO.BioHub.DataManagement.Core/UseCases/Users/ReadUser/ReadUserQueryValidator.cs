using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ReadUser;

public class ReadUserQueryValidator : AbstractValidator<ReadUserQuery>
{
    public ReadUserQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}