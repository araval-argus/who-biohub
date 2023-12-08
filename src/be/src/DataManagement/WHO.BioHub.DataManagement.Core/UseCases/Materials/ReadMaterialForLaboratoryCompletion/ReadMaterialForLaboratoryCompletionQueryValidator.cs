using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterialForLaboratoryCompletion;

public class ReadMaterialForLaboratoryCompletionQueryValidator : AbstractValidator<ReadMaterialForLaboratoryCompletionQuery>
{
    public ReadMaterialForLaboratoryCompletionQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}