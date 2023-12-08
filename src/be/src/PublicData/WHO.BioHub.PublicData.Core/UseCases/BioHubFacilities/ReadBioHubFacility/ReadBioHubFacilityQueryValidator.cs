using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ReadBioHubFacility;

public class ReadBioHubFacilityQueryValidator : AbstractValidator<ReadBioHubFacilityQuery>
{
    public ReadBioHubFacilityQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}