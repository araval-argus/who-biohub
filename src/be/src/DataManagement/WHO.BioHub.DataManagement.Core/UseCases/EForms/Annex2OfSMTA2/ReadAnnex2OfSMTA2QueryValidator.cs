using FluentValidation;

namespace WHO.BioHub.Data.Core.UseCases.EForms.Annex2OfSMTA2;

public class ReadAnnex2OfSMTA2QueryValidator : AbstractValidator<ReadAnnex2OfSMTA2Query>
{
    public ReadAnnex2OfSMTA2QueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}