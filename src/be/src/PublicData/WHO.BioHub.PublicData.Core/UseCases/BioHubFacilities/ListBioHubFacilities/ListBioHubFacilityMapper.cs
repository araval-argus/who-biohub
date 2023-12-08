using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ListBioHubFacilities;

public interface IListBioHubFacilityMapper
{
    IEnumerable<BioHubFacilityPublicViewModel> Map(IEnumerable<BioHubFacility> bioHubFacilities);
}

public class ListBioHubFacilityMapper : IListBioHubFacilityMapper
{
    public IEnumerable<BioHubFacilityPublicViewModel> Map(IEnumerable<BioHubFacility> bioHubFacilities)
    {
        List<BioHubFacilityPublicViewModel> bioHubFacilityPublicViewModels = new List<BioHubFacilityPublicViewModel>();

        foreach (var bioHubFacility in bioHubFacilities)
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
            bioHubFacilityPublicViewModels.Add(bioHubFacilityPublicViewModel);
        }


        return bioHubFacilityPublicViewModels;
    }
}