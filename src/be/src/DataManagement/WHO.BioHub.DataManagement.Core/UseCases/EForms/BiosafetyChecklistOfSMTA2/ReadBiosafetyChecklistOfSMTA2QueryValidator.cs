using FluentValidation;

namespace WHO.BioHub.Data.Core.UseCases.EForms.BiosafetyChecklistOfSMTA2;

public class ReadBiosafetyChecklistOfSMTA2QueryValidator : AbstractValidator<ReadBiosafetyChecklistOfSMTA2Query>
{
    public ReadBiosafetyChecklistOfSMTA2QueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}