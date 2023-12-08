using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.DeleteResourceFileToken;

public class DeleteResourceFileTokenQueryValidator : AbstractValidator<DeleteResourceFileTokenQuery>
{
    public DeleteResourceFileTokenQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}