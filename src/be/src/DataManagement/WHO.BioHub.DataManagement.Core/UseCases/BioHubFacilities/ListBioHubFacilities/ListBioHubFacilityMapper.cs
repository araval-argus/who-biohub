using WHO.BioHub.Models.Models;

using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListBioHubFacilities;
public interface IListBioHubFacilityMapper
{
    IEnumerable<BioHubFacilityViewModel> Map(IEnumerable<BioHubFacility> bioHubFacilities);
}

public class ListBioHubFacilityMapper : IListBioHubFacilityMapper
{
    public IEnumerable<BioHubFacilityViewModel> Map(IEnumerable<BioHubFacility> bioHubFacilities)
    {
        List<BioHubFacilityViewModel> bioHubFacilityMapViewModels = new List<BioHubFacilityViewModel>();

        foreach (var bioHubFacility in bioHubFacilities)
        {
            BioHubFacilityViewModel bioHubFacilityMapViewModel = new()
            {
                Id = bioHubFacility.Id,
                Name = bioHubFacility.Name,
                Abbreviation = bioHubFacility.Abbreviation != null ? bioHubFacility.Abbreviation : string.Empty,
                Address = bioHubFacility.Address,
                Latitude = bioHubFacility.Latitude,
                Longitude = bioHubFacility.Longitude,
                CountryId = bioHubFacility.CountryId,
                BSLLevelId = bioHubFacility.BSLLevelId,
                IsPublicFacing = bioHubFacility.IsPublicFacing,
                IsActive = bioHubFacility.IsActive,
            };
            bioHubFacilityMapViewModels.Add(bioHubFacilityMapViewModel);
        }


        return bioHubFacilityMapViewModels;
    }    
}