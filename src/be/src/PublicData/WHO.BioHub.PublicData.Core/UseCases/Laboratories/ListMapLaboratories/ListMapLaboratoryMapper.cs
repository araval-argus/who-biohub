using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.PublicData.Core.Dto;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.DAL.Extensions;

namespace WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListMapLaboratories;

public interface IListMapLaboratoryMapper
{
    Task<IEnumerable<LaboratoryPublicMapViewModel>> Map(IEnumerable<Laboratory> laboratories, CancellationToken cancellationToken);
}

public class ListMapLaboratoryMapper : IListMapLaboratoryMapper
{
    private readonly ILaboratoryPublicReadRepository _readRepository;

    public ListMapLaboratoryMapper(ILaboratoryPublicReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<IEnumerable<LaboratoryPublicMapViewModel>> Map(IEnumerable<Laboratory> laboratories, CancellationToken cancellationToken)
    {
        List<LaboratoryPublicMapViewModel> laboratoryPublicMapViewModels = new List<LaboratoryPublicMapViewModel>();

        foreach (var laboratory in laboratories)
        {
            var worklistToBioHubItems = await _readRepository.GetWorklistToBioHubItemsForMap(laboratory.Id, cancellationToken);
            var worklistFromBioHubItems = await _readRepository.GetWorklistFromBioHubItemsForMap(laboratory.Id, cancellationToken);

            LaboratoryPublicMapViewModel laboratoryPublicMapViewModel = new()
            {
                Id = laboratory.Id,
                Name = laboratory.Name,
                Abbreviation = laboratory.Abbreviation != null ? laboratory.Abbreviation : string.Empty,
                Address = laboratory.Address,
                Latitude = laboratory.Latitude.GetValueOrDefault(),
                Longitude = laboratory.Longitude.GetValueOrDefault(),
                CountryId = laboratory.CountryId,
                ToBioHubConnectedInstitutesLatLng = GetToBioHubConnectedInstitutesLatLng(worklistToBioHubItems),
                FromBioHubConnectedInstitutesLatLng = GetFromBioHubConnectedInstitutesLatLng(worklistFromBioHubItems),
            };

            laboratoryPublicMapViewModel.InstituteType = GetInstituteType(laboratoryPublicMapViewModel);
            laboratoryPublicMapViewModels.Add(laboratoryPublicMapViewModel);
        }


        return laboratoryPublicMapViewModels;
    }

    private List<Coordinates> GetToBioHubConnectedInstitutesLatLng(IEnumerable<WorklistToBioHubItem> worklistToBioHubItems)
    {
        List<Coordinates> connectedInstitutesLatLng = new List<Coordinates>();        

        foreach (var worklistToBioHubItem in worklistToBioHubItems)
        {
            if (worklistToBioHubItem.DeletedOn == null && worklistToBioHubItem.RequestInitiationToBioHubFacility.DeletedOn == null && worklistToBioHubItem.RequestInitiationToBioHubFacility.IsPublicFacing == true && worklistToBioHubItem.WorklistToBioHubItemMaterials.Select(x => x.Material).Any(x => x.BHFShareReadiness == Readiness.Ready && x.PublicShare == YesNoOption.Yes))
            {
                AddCoordinates(connectedInstitutesLatLng, worklistToBioHubItem.RequestInitiationToBioHubFacility.Latitude, worklistToBioHubItem.RequestInitiationToBioHubFacility.Longitude);
            }
        }

        return connectedInstitutesLatLng;
    }

    private List<Coordinates> GetFromBioHubConnectedInstitutesLatLng(IEnumerable<WorklistFromBioHubItem> worklistFromBioHubItems)
    {
        List<Coordinates> connectedInstitutesLatLng = new List<Coordinates>();

        foreach (var worklistFromBioHubItem in worklistFromBioHubItems)
        {
            if (worklistFromBioHubItem.DeletedOn == null && worklistFromBioHubItem.RequestInitiationFromBioHubFacility.DeletedOn == null && worklistFromBioHubItem.RequestInitiationFromBioHubFacility.IsPublicFacing == true && worklistFromBioHubItem.WorklistFromBioHubItemMaterials.Select(x => x.Material).Any(x => x.BHFShareReadiness == Readiness.Ready && x.PublicShare == YesNoOption.Yes))
            {
                AddCoordinates(connectedInstitutesLatLng, worklistFromBioHubItem.RequestInitiationFromBioHubFacility.Latitude, worklistFromBioHubItem.RequestInitiationFromBioHubFacility.Longitude);
            }
        }

        return connectedInstitutesLatLng;
    }

    private void AddCoordinates(List<Coordinates> connectedInstitutesLatLng, double? latitude, double? longitude)
    {
        if (latitude != null && longitude != null && !(connectedInstitutesLatLng.Any(x => x.Latitude == latitude && x.Longitude == longitude))) {
            Coordinates latLngElementDto = new Coordinates();
            latLngElementDto.Latitude = latitude.GetValueOrDefault();
            latLngElementDto.Longitude = longitude.GetValueOrDefault();
            connectedInstitutesLatLng.Add(latLngElementDto);
        }
    }

    private InstituteType GetInstituteType(LaboratoryPublicMapViewModel laboratoryPublicMapViewModel)
    {
        if (laboratoryPublicMapViewModel.ToBioHubConnectedInstitutesLatLng.Any() &&
             !laboratoryPublicMapViewModel.FromBioHubConnectedInstitutesLatLng.Any())
        {
            return InstituteType.Provider;
        }

        if (!laboratoryPublicMapViewModel.ToBioHubConnectedInstitutesLatLng.Any() &&
             laboratoryPublicMapViewModel.FromBioHubConnectedInstitutesLatLng.Any())
        {
            return InstituteType.QE;
        }

        if (laboratoryPublicMapViewModel.ToBioHubConnectedInstitutesLatLng.Any() &&
             laboratoryPublicMapViewModel.FromBioHubConnectedInstitutesLatLng.Any())
        {
            return InstituteType.ProviderQE;
        }

        return InstituteType.Normal;
    }
}