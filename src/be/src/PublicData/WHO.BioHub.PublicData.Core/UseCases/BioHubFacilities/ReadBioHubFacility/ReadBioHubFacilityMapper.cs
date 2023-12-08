using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ReadBioHubFacility;

public interface IReadBioHubFacilityMapper
{
    BioHubFacilityPublicViewModel Map(BioHubFacility bioHubFacility);
}

public class ReadBioHubFacilityMapper : IReadBioHubFacilityMapper
{
    public BioHubFacilityPublicViewModel Map(BioHubFacility bioHubFacility)
    {
        BioHubFacilityPublicViewModel bioHubFacilityPublicViewModel = new()
        {
            Id = bioHubFacility.Id,
            Name = bioHubFacility.Name,
            Abbreviation = bioHubFacility.Abbreviation != null ? bioHubFacility.Abbreviation : string.Empty,
            Address = bioHubFacility.Address,
            Latitude = bioHubFacility.Latitude,
            Longitude = bioHubFacility.Longitude,
            CountryId = bioHubFacility.CountryId
        };

        return bioHubFacilityPublicViewModel;
    }
}