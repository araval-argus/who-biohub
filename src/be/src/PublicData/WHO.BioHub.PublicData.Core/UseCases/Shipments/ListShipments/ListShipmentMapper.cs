using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListShipments;

public interface IListShipmentMapper
{
    IEnumerable<ShipmentPublicViewModel> Map(IEnumerable<Shipment> laboratories);
}

public class ListShipmentMapper : IListShipmentMapper
{
    public IEnumerable<ShipmentPublicViewModel> Map(IEnumerable<Shipment> shipments)
    {
        List<ShipmentPublicViewModel> shipmentPublicViewModels = new List<ShipmentPublicViewModel>();

        foreach (var shipment in shipments)
        {
            ShipmentPublicViewModel shipmentPublicViewModel = new ShipmentPublicViewModel();

            shipmentPublicViewModel.Id = shipment.Id;
            shipmentPublicViewModel.ReferenceNumber = shipment.ReferenceNumber;
            shipmentPublicViewModel.From = shipment.WorklistFromBioHubItem != null ? $"{shipment.WorklistFromBioHubItem.RequestInitiationFromBioHubFacility.Country.Iso3}-{shipment.WorklistFromBioHubItem.RequestInitiationFromBioHubFacility.Name}" : (shipment.WorklistToBioHubItem != null ? $"{shipment.WorklistToBioHubItem.RequestInitiationFromLaboratory.Country.Iso3}-{shipment.WorklistToBioHubItem.RequestInitiationFromLaboratory.Name}" : string.Empty);
            shipmentPublicViewModel.To = shipment.WorklistFromBioHubItem != null ? $"{shipment.WorklistFromBioHubItem.RequestInitiationToLaboratory.Country.Iso3}-{shipment.WorklistFromBioHubItem.RequestInitiationToLaboratory.Name}" : (shipment.WorklistToBioHubItem != null ? $"{shipment.WorklistToBioHubItem.RequestInitiationToBioHubFacility.Country.Iso3}-{shipment.WorklistToBioHubItem.RequestInitiationToBioHubFacility.Name}" : string.Empty);
            shipmentPublicViewModel.CompletedOn = shipment.CreationDate;
            shipmentPublicViewModel.StatusOfRequest = shipment.StatusOfRequest;
            shipmentPublicViewModel.BioHubFacilityId = shipment.BioHubFacilityId;
            shipmentPublicViewModel.LaboratoryId = shipment.QELaboratoryId;
            shipmentPublicViewModel.ShipmentDirection = shipment.WorklistFromBioHubItem != null ? Shared.Enums.ShipmentDirection.FromBioHub : Shared.Enums.ShipmentDirection.ToBioHub;

            shipmentPublicViewModels.Add(shipmentPublicViewModel);
        }

        shipmentPublicViewModels = shipmentPublicViewModels.OrderByDescending(x => x.CompletedOn).ToList();

        return shipmentPublicViewModels;
    }
}