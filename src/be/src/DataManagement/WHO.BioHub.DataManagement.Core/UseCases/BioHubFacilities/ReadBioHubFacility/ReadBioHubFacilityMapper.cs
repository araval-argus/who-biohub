using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ReadBioHubFacility;

public interface IReadBioHubFacilityMapper
{
    BioHubFacilityViewModel Map(BioHubFacility bioHubFacility);
}

public class ReadBioHubFacilityMapper : IReadBioHubFacilityMapper
{
    public BioHubFacilityViewModel Map(BioHubFacility bioHubFacility)
    {
        BioHubFacilityViewModel bioHubFacilityViewModel = new()
        {
            Id = bioHubFacility.Id,
            Name = bioHubFacility.Name,
            Description = bioHubFacility.Description,
            Abbreviation = bioHubFacility.Abbreviation != null ? bioHubFacility.Abbreviation : string.Empty,
            Address = bioHubFacility.Address,
            Latitude = bioHubFacility.Latitude,
            Longitude = bioHubFacility.Longitude,
            CountryId = bioHubFacility.CountryId,
            BSLLevelId = bioHubFacility.BSLLevelId,
            IsActive = bioHubFacility.IsActive,
            IsPublicFacing = bioHubFacility.IsPublicFacing,
        };

        return bioHubFacilityViewModel;
    }
}