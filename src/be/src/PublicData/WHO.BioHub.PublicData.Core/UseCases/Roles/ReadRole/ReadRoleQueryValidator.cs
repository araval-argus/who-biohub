using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.Roles.ReadRole;

public class ReadRoleQueryValidator : AbstractValidator<ReadRoleQuery>
{
    public ReadRoleQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}