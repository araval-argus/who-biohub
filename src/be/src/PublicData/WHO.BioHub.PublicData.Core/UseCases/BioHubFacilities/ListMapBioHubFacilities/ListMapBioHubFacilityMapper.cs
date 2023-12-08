using System.Threading;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.PublicData.Core.Dto;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ListMapBioHubFacilities;

public interface IListMapBioHubFacilityMapper
{
    Task<IEnumerable<BioHubFacilityPublicMapViewModel>> Map(IEnumerable<BioHubFacility> bioHubFacilities, CancellationToken cancellationToken);
}

public class ListMapBioHubFacilityMapper : IListMapBioHubFacilityMapper
{
    private readonly IBioHubFacilityPublicReadRepository _readRepository;

    public ListMapBioHubFacilityMapper(IBioHubFacilityPublicReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<IEnumerable<BioHubFacilityPublicMapViewModel>> Map(IEnumerable<BioHubFacility> bioHubFacilities, CancellationToken cancellationToken
        )
    {
        List<BioHubFacilityPublicMapViewModel> bioHubFacilityPublicMapViewModels = new List<BioHubFacilityPublicMapViewModel>();

        foreach (var bioHubFacility in bioHubFacilities)
        {

            var worklistToBioHubItems = await _readRepository.GetWorklistToBioHubItemsForMap(bioHubFacility.Id, cancellationToken);
            var worklistFromBioHubItems = await _readRepository.GetWorklistFromBioHubItemsForMap(bioHubFacility.Id, cancellationToken);


            BioHubFacilityPublicMapViewModel bioHubFacilityPublicMapViewModel = new()
            {
                Id = bioHubFacility.Id,
                Name = bioHubFacility.Name,
                Abbreviation = bioHubFacility.Abbreviation != null ? bioHubFacility.Abbreviation : string.Empty,
                Address = bioHubFacility.Address,
                Latitude = bioHubFacility.Latitude,
                Longitude = bioHubFacility.Longitude,
                CountryId = bioHubFacility.CountryId,
                ToBioHubConnectedInstitutesLatLng = GetToBioHubConnectedInstitutesLatLng(worklistToBioHubItems),
                FromBioHubConnectedInstitutesLatLng = GetFromBioHubConnectedInstitutesLatLng(worklistFromBioHubItems),
            };
            bioHubFacilityPublicMapViewModels.Add(bioHubFacilityPublicMapViewModel);
        }


        return bioHubFacilityPublicMapViewModels;
    }

    private List<Coordinates> GetToBioHubConnectedInstitutesLatLng(IEnumerable<WorklistToBioHubItem> worklistToBioHubItems)
    {
        List<Coordinates> connectedInstitutesLatLng = new List<Coordinates>();

        foreach (var worklistToBioHubItem in worklistToBioHubItems)
        {
            if (worklistToBioHubItem.RequestInitiationFromLaboratory.DeletedOn == null && worklistToBioHubItem.RequestInitiationFromLaboratory.IsPublicFacing == true && worklistToBioHubItem.WorklistToBioHubItemMaterials.Select(x => x.Material).Any(x => x.BHFShareReadiness == Readiness.Ready && x.PublicShare == YesNoOption.Yes))
            {
                AddCoordinates(connectedInstitutesLatLng, worklistToBioHubItem.RequestInitiationFromLaboratory.Latitude, worklistToBioHubItem.RequestInitiationFromLaboratory.Longitude);
            }
        }

        return connectedInstitutesLatLng;
    }

    private List<Coordinates> GetFromBioHubConnectedInstitutesLatLng(IEnumerable<WorklistFromBioHubItem> worklistFromBioHubItems)
    {
        List<Coordinates> connectedInstitutesLatLng = new List<Coordinates>();

        foreach (var worklistFromBioHubItem in worklistFromBioHubItems)
        {
            if (worklistFromBioHubItem.RequestInitiationToLaboratory.DeletedOn == null && worklistFromBioHubItem.RequestInitiationToLaboratory.IsPublicFacing == true && worklistFromBioHubItem.WorklistFromBioHubItemMaterials.Select(x => x.Material).Any(x => x.BHFShareReadiness == Readiness.Ready && x.PublicShare == YesNoOption.Yes))
            {
                AddCoordinates(connectedInstitutesLatLng, worklistFromBioHubItem.RequestInitiationToLaboratory.Latitude, worklistFromBioHubItem.RequestInitiationToLaboratory.Longitude);
            }
        }

        return connectedInstitutesLatLng;
    }

    private void AddCoordinates(List<Coordinates> connectedInstitutesLatLng, double? latitude, double? longitude)
    {
        if (latitude != null && longitude != null && !(connectedInstitutesLatLng.Any(x => x.Latitude == latitude && x.Longitude == longitude)))
        {
            Coordinates latLngElementDto = new Coordinates();
            latLngElementDto.Latitude = latitude.GetValueOrDefault();
            latLngElementDto.Longitude = longitude.GetValueOrDefault();
            connectedInstitutesLatLng.Add(latLngElementDto);
        }
    }
}