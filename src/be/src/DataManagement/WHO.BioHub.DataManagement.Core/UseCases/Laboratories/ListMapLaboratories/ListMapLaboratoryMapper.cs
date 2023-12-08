using System.Threading;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ListMapLaboratories;
public interface IListMapLaboratoryMapper
{
    Task<IEnumerable<LaboratoryMapViewModel>> Map(IEnumerable<Laboratory> laboratories, RoleType roleType, CancellationToken cancellationToken, Guid? instituteId = null);
}

public class ListMapLaboratoryMapper : IListMapLaboratoryMapper
{
    private readonly ILaboratoryReadRepository _readRepository;

    public ListMapLaboratoryMapper(ILaboratoryReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<IEnumerable<LaboratoryMapViewModel>> Map(IEnumerable<Laboratory> laboratories, RoleType roleType, CancellationToken cancellationToken, Guid? instituteId = null)
    {
        List<LaboratoryMapViewModel> laboratoryMapViewModels = new List<LaboratoryMapViewModel>();

        foreach (var laboratory in laboratories)
        {

            var worklistToBioHubItems = await _readRepository.GetWorklistToBioHubItemsForMap(laboratory.Id, cancellationToken);
            var worklistFromBioHubItems = await _readRepository.GetWorklistFromBioHubItemsForMap(laboratory.Id, cancellationToken);

            LaboratoryMapViewModel laboratoryMapViewModel = new()
            {
                Id = laboratory.Id,
                Name = laboratory.Name,
                Abbreviation = laboratory.Abbreviation != null ? laboratory.Abbreviation : string.Empty,
                Address = laboratory.Address,
                Latitude = laboratory.Latitude.GetValueOrDefault(),
                Longitude = laboratory.Longitude.GetValueOrDefault(),
                CountryId = laboratory.CountryId,
                ToBioHubConnectedInstitutesLatLng = GetToBioHubConnectedInstitutesLatLng(worklistToBioHubItems, roleType, instituteId),
                FromBioHubConnectedInstitutesLatLng = GetFromBioHubConnectedInstitutesLatLng(worklistFromBioHubItems, roleType, instituteId),
            };

            laboratoryMapViewModel.InstituteType = GetInstituteType(laboratoryMapViewModel);

            laboratoryMapViewModels.Add(laboratoryMapViewModel);
        }


        return laboratoryMapViewModels;
    }

    private List<Coordinates> GetToBioHubConnectedInstitutesLatLng(IEnumerable<WorklistToBioHubItem> worklistToBioHubItems, RoleType roleType, Guid? instituteId = null)
    {
        List<Coordinates> connectedInstitutesLatLng = new List<Coordinates>();

        foreach (var worklistToBioHubItem in worklistToBioHubItems)
        {
            switch (roleType)
            {
                case RoleType.Laboratory:
                    if (worklistToBioHubItem.DeletedOn == null && worklistToBioHubItem.RequestInitiationToBioHubFacility.DeletedOn == null && worklistToBioHubItem.RequestInitiationToBioHubFacility.IsPublicFacing == true && (worklistToBioHubItem.RequestInitiationFromLaboratoryId == instituteId || worklistToBioHubItem.WorklistToBioHubItemMaterials.Select(x => x.Material).Any(x => x.BHFShareReadiness == Readiness.Ready && x.PublicShare == YesNoOption.Yes)))
                    {
                        AddCoordinates(connectedInstitutesLatLng, worklistToBioHubItem.RequestInitiationToBioHubFacility.Latitude, worklistToBioHubItem.RequestInitiationToBioHubFacility.Longitude);
                    }
                    break;

                default:
                    if (worklistToBioHubItem.DeletedOn == null && worklistToBioHubItem.RequestInitiationToBioHubFacility.DeletedOn == null)
                    {
                        AddCoordinates(connectedInstitutesLatLng, worklistToBioHubItem.RequestInitiationToBioHubFacility.Latitude, worklistToBioHubItem.RequestInitiationToBioHubFacility.Longitude);
                    }
                    break;

            }

        }

        return connectedInstitutesLatLng;
    }

    private List<Coordinates> GetFromBioHubConnectedInstitutesLatLng(IEnumerable<WorklistFromBioHubItem> worklistToBioHubItems, RoleType roleType, Guid? instituteId = null)
    {
        List<Coordinates> connectedInstitutesLatLng = new List<Coordinates>();

        foreach (var worklistFromBioHubItem in worklistToBioHubItems)
        {
            switch (roleType)
            {
                case RoleType.Laboratory:
                    if (worklistFromBioHubItem.DeletedOn == null && worklistFromBioHubItem.RequestInitiationFromBioHubFacility.DeletedOn == null && worklistFromBioHubItem.RequestInitiationFromBioHubFacility.IsPublicFacing == true && (worklistFromBioHubItem.RequestInitiationToLaboratoryId == instituteId || worklistFromBioHubItem.WorklistFromBioHubItemMaterials.Select(x => x.Material).Any(x => x.BHFShareReadiness == Readiness.Ready && x.PublicShare == YesNoOption.Yes)))
                    {
                        AddCoordinates(connectedInstitutesLatLng, worklistFromBioHubItem.RequestInitiationFromBioHubFacility.Latitude, worklistFromBioHubItem.RequestInitiationFromBioHubFacility.Longitude);
                    }
                    break;

                default:
                    if (worklistFromBioHubItem.DeletedOn == null && worklistFromBioHubItem.RequestInitiationFromBioHubFacility.DeletedOn == null)
                    {
                        AddCoordinates(connectedInstitutesLatLng, worklistFromBioHubItem.RequestInitiationFromBioHubFacility.Latitude, worklistFromBioHubItem.RequestInitiationFromBioHubFacility.Longitude);
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

    private InstituteType GetInstituteType(LaboratoryMapViewModel laboratoryMapViewModel)
    {        
        if (laboratoryMapViewModel.ToBioHubConnectedInstitutesLatLng.Any() &&
             !laboratoryMapViewModel.FromBioHubConnectedInstitutesLatLng.Any())
        {
            return InstituteType.Provider;
        }

        if (!laboratoryMapViewModel.ToBioHubConnectedInstitutesLatLng.Any() &&
             laboratoryMapViewModel.FromBioHubConnectedInstitutesLatLng.Any())
        {
            return InstituteType.QE;
        }

        if (laboratoryMapViewModel.ToBioHubConnectedInstitutesLatLng.Any() &&
             laboratoryMapViewModel.FromBioHubConnectedInstitutesLatLng.Any())
        {
            return InstituteType.ProviderQE;
        }

        return InstituteType.Normal;
    }
}