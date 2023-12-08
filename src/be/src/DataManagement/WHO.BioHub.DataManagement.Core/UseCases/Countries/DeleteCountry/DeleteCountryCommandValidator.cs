using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Countries.DeleteCountry;

public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
{
    public DeleteCountryCommandValidator()
    {
        RuleFor(cmd => cmd);
    }
}