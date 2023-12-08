using FluentValidation;

namespace WHO.BioHub.Data.Core.UseCases.EForms.Annex2OfSMTA1;

public class ReadAnnex2OfSMTA1QueryValidator : AbstractValidator<ReadAnnex2OfSMTA1Query>
{
    public ReadAnnex2OfSMTA1QueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}