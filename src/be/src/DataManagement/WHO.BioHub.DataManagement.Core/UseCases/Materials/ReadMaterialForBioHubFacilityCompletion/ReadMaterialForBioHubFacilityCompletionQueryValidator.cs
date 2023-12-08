using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterialForBioHubFacilityCompletion;

public class ReadMaterialForBioHubFacilityCompletionQueryValidator : AbstractValidator<ReadMaterialForBioHubFacilityCompletionQuery>
{
    public ReadMaterialForBioHubFacilityCompletionQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}