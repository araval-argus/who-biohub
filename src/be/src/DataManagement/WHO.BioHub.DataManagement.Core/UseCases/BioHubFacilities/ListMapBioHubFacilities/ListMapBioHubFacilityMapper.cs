using System.Threading;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BioHubFacilities;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListMapBioHubFacilities;
public interface IListMapBioHubFacilityMapper
{
    Task<IEnumerable<BioHubFacilityMapViewModel>> Map(IEnumerable<BioHubFacility> bioHubFacilities, RoleType roleType, CancellationToken cancellationToken, Guid? instituteId = null);
}

public class ListMapBioHubFacilityMapper : IListMapBioHubFacilityMapper
{
    private readonly IBioHubFacilityReadRepository _readRepository;

    public ListMapBioHubFacilityMapper(IBioHubFacilityReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<IEnumerable<BioHubFacilityMapViewModel>> Map(IEnumerable<BioHubFacility> bioHubFacilities, RoleType roleType, CancellationToken cancellationToken, Guid? instituteId = null)
    {
        List<BioHubFacilityMapViewModel> bioHubFacilityMapViewModels = new List<BioHubFacilityMapViewModel>();

        foreach (var bioHubFacility in bioHubFacilities)
        {
            var worklistToBioHubItems = await _readRepository.GetWorklistToBioHubItemsForMap(bioHubFacility.Id, cancellationToken);
            var worklistFromBioHubItems = await _readRepository.GetWorklistFromBioHubItemsForMap(bioHubFacility.Id, cancellationToken);


            BioHubFacilityMapViewModel bioHubFacilityMapViewModel = new()
            {
                Id = bioHubFacility.Id,
                Name = bioHubFacility.Name,
                Abbreviation = bioHubFacility.Abbreviation != null ? bioHubFacility.Abbreviation : string.Empty,
                Address = bioHubFacility.Address,
                Latitude = bioHubFacility.Latitude,
                Longitude = bioHubFacility.Longitude,
                CountryId = bioHubFacility.CountryId,
                ToBioHubConnectedInstitutesLatLng = GetToBioHubConnectedInstitutesLatLng(worklistToBioHubItems, roleType, instituteId),
                FromBioHubConnectedInstitutesLatLng = GetFromBioHubConnectedInstitutesLatLng(worklistFromBioHubItems, roleType, instituteId),
            };
            bioHubFacilityMapViewModels.Add(bioHubFacilityMapViewModel);
        }


        return bioHubFacilityMapViewModels;
    }

    private List<Coordinates> GetToBioHubConnectedInstitutesLatLng(IEnumerable<WorklistToBioHubItem> worklistToBioHubItems, RoleType roleType, Guid? instituteId = null)
    {
        List<Coordinates> connectedInstitutesLatLng = new List<Coordinates>();

        foreach (var worklistToBioHubItem in worklistToBioHubItems)
        {
            switch (roleType)
            {
                case RoleType.Laboratory:
                    if (worklistToBioHubItem.RequestInitiationToBioHubFacility.DeletedOn == null && worklistToBioHubItem.RequestInitiationToBioHubFacility.IsPublicFacing == true && (worklistToBioHubItem.RequestInitiationFromLaboratoryId == instituteId || worklistToBioHubItem.WorklistToBioHubItemMaterials.Select(x => x.Material).Any(x => x.BHFShareReadiness == Readiness.Ready && x.PublicShare == YesNoOption.Yes)))
                    {
                        AddCoordinates(connectedInstitutesLatLng, worklistToBioHubItem.RequestInitiationFromLaboratory.Latitude, worklistToBioHubItem.RequestInitiationFromLaboratory.Longitude);
                    }
                    break;

                default:
                    if (worklistToBioHubItem.RequestInitiationToBioHubFacility.DeletedOn == null)
                    {
                        AddCoordinates(connectedInstitutesLatLng, worklistToBioHubItem.RequestInitiationFromLaboratory.Latitude, worklistToBioHubItem.RequestInitiationFromLaboratory.Longitude);
                    }
                    break;

            }

        }

        return connectedInstitutesLatLng;
    }

    private List<Coordinates> GetFromBioHubConnectedInstitutesLatLng(IEnumerable<WorklistFromBioHubItem> worklistFromBioHubItems, RoleType roleType, Guid? instituteId = null)
    {
        List<Coordinates> connectedInstitutesLatLng = new List<Coordinates>();

        foreach (var worklistFromBioHubItem in worklistFromBioHubItems)
        {
            switch (roleType)
            {
                case RoleType.BioHubFacility:
                    if (worklistFromBioHubItem.RequestInitiationFromBioHubFacility.DeletedOn == null && worklistFromBioHubItem.RequestInitiationFromBioHubFacility.IsPublicFacing == true && (worklistFromBioHubItem.RequestInitiationToLaboratoryId == instituteId || worklistFromBioHubItem.WorklistFromBioHubItemMaterials.Select(x => x.Material).Any(x => x.BHFShareReadiness == Readiness.Ready && x.PublicShare == YesNoOption.Yes)))
                    {
                        AddCoordinates(connectedInstitutesLatLng, worklistFromBioHubItem.RequestInitiationToLaboratory.Latitude, worklistFromBioHubItem.RequestInitiationToLaboratory.Longitude);
                    }
                    break;

                default:
                    if (worklistFromBioHubItem.RequestInitiationFromBioHubFacility.DeletedOn == null)
                    {
                        AddCoordinates(connectedInstitutesLatLng, worklistFromBioHubItem.RequestInitiationToLaboratory.Latitude, worklistFromBioHubItem.RequestInitiationToLaboratory.Longitude);
                    }
                    break;

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