using FluentValidation;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ReadBioHubFacility;

public class ReadBioHubFacilityQueryValidator : AbstractValidator<ReadBioHubFacilityQuery>
{
    public ReadBioHubFacilityQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}