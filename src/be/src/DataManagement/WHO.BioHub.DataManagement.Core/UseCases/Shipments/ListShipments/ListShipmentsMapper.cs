using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.ListShipments;

public interface IListShipmentsMapper
{
    IEnumerable<ShipmentViewModel> Map(IEnumerable<Shipment> shipments);
}

public class ListShipmentsMapper : IListShipmentsMapper
{
    public IEnumerable<ShipmentViewModel> Map(IEnumerable<Shipment> shipments)
    {        

        List<ShipmentViewModel> list = new List<ShipmentViewModel>();

        foreach (var shipment in shipments)
        {
            ShipmentViewModel shipmentViewModel = new ShipmentViewModel();

            shipmentViewModel.Id = shipment.Id;
            shipmentViewModel.ReferenceNumber = shipment.ReferenceNumber;
            shipmentViewModel.From = shipment.WorklistFromBioHubItem != null ? $"{shipment.WorklistFromBioHubItem.RequestInitiationFromBioHubFacility.Country.Iso3}-{shipment.WorklistFromBioHubItem.RequestInitiationFromBioHubFacility.Name}" : (shipment.WorklistToBioHubItem != null ? $"{shipment.WorklistToBioHubItem.RequestInitiationFromLaboratory.Country.Iso3}-{shipment.WorklistToBioHubItem.RequestInitiationFromLaboratory.Name}" : string.Empty);
            shipmentViewModel.To = shipment.WorklistFromBioHubItem != null ? $"{shipment.WorklistFromBioHubItem.RequestInitiationToLaboratory.Country.Iso3}-{shipment.WorklistFromBioHubItem.RequestInitiationToLaboratory.Name}" : (shipment.WorklistToBioHubItem != null ? $"{shipment.WorklistToBioHubItem.RequestInitiationToBioHubFacility.Country.Iso3}-{shipment.WorklistToBioHubItem.RequestInitiationToBioHubFacility.Name}" : string.Empty);
            shipmentViewModel.ShipmentDirection = shipment.WorklistFromBioHubItem != null ? Shared.Enums.ShipmentDirection.FromBioHub : Shared.Enums.ShipmentDirection.ToBioHub;
            shipmentViewModel.CompletedOn = shipment.CompletedOn;

            shipmentViewModel.StatusOfRequest = shipment.StatusOfRequest;
            shipmentViewModel.BioHubFacilityId = shipment.BioHubFacilityId;
            shipmentViewModel.LaboratoryId = shipment.QELaboratoryId;

            list.Add(shipmentViewModel);
        }

        list = list.OrderByDescending(x => x.CompletedOn).ToList();

        return list;
    }
}