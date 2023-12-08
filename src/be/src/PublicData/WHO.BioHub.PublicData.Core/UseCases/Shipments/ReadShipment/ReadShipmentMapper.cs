using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.PublicData.Core.UseCases.Shipments.ReadShipment;

public interface IReadShipmentMapper
{
    ShipmentPublicViewModel Map(Shipment shipment);
}

public class ReadShipmentMapper : IReadShipmentMapper
{
    public ShipmentPublicViewModel Map(Shipment shipment)
    {
        ShipmentPublicViewModel shipmentPublicViewModel = new ShipmentPublicViewModel();

        shipmentPublicViewModel.Id = shipment.Id;
        shipmentPublicViewModel.ReferenceNumber = shipment.ReferenceNumber;
        shipmentPublicViewModel.From = shipment.WorklistFromBioHubItem != null ? shipment.WorklistFromBioHubItem.RequestInitiationFromBioHubFacility.Name : (shipment.WorklistToBioHubItem != null ? shipment.WorklistToBioHubItem.RequestInitiationFromLaboratory.Name : String.Empty);
        shipmentPublicViewModel.To = shipment.WorklistFromBioHubItem != null ? shipment.WorklistFromBioHubItem.RequestInitiationToLaboratory.Name : (shipment.WorklistToBioHubItem != null ? shipment.WorklistToBioHubItem.RequestInitiationToBioHubFacility.Name : String.Empty);
        shipmentPublicViewModel.CompletedOn = shipment.CreationDate;
        shipmentPublicViewModel.StatusOfRequest = shipment.StatusOfRequest;
        shipmentPublicViewModel.BioHubFacilityId = shipment.BioHubFacilityId;
        shipmentPublicViewModel.LaboratoryId = shipment.QELaboratoryId;


        shipmentPublicViewModel.ShipmentMaterials = new List<ShipmentPublicMaterialViewModel>();

        if (shipment.WorklistFromBioHubItem != null &&
            shipment.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials != null &&
            shipment.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials.Any()
            )
        {
            var worklistFromBioHubItemMaterials = shipment.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials.Where(x => x.Material.PublicShare == YesNoOption.Yes && x.Material.Status == MaterialStatus.Completed);

            foreach (var worklistFromBioHubItemMaterial in worklistFromBioHubItemMaterials)
            {
                ShipmentPublicMaterialViewModel shipmentPublicMaterialViewModel = new ShipmentPublicMaterialViewModel();
                shipmentPublicMaterialViewModel.Id = worklistFromBioHubItemMaterial.Id;
                shipmentPublicMaterialViewModel.MaterialProductId = worklistFromBioHubItemMaterial.Material.OriginalProductTypeId;
                shipmentPublicMaterialViewModel.MaterialNumber = worklistFromBioHubItemMaterial.Material.ReferenceNumber;
                shipmentPublicMaterialViewModel.MaterialId = worklistFromBioHubItemMaterial.Material.Id;
                shipmentPublicMaterialViewModel.MaterialName = worklistFromBioHubItemMaterial.Material.Name;
                shipmentPublicViewModel.ShipmentMaterials.Add(shipmentPublicMaterialViewModel);
            }
        }

        if (shipment.WorklistToBioHubItem != null &&
            shipment.WorklistToBioHubItem.WorklistToBioHubItemMaterials != null &&
            shipment.WorklistToBioHubItem.WorklistToBioHubItemMaterials.Any()
            )
        {
            var worklistToBioHubItemMaterials = shipment.WorklistToBioHubItem.WorklistToBioHubItemMaterials.Where(x => x.Material.PublicShare == YesNoOption.Yes && x.Material.Status == MaterialStatus.Completed);

            foreach (var worklistToBioHubItemMaterial in worklistToBioHubItemMaterials)
            {
                ShipmentPublicMaterialViewModel shipmentPublicMaterialViewModel = new ShipmentPublicMaterialViewModel();
                shipmentPublicMaterialViewModel.Id = worklistToBioHubItemMaterial.Id;
                shipmentPublicMaterialViewModel.MaterialProductId = worklistToBioHubItemMaterial.Material.OriginalProductTypeId;
                shipmentPublicMaterialViewModel.MaterialNumber = worklistToBioHubItemMaterial.Material.ReferenceNumber;
                shipmentPublicMaterialViewModel.MaterialId = worklistToBioHubItemMaterial.Material.Id;
                shipmentPublicMaterialViewModel.MaterialName = worklistToBioHubItemMaterial.Material.Name;
                shipmentPublicMaterialViewModel.ProviderId = worklistToBioHubItemMaterial.Material.ProviderLaboratoryId != null ? worklistToBioHubItemMaterial.Material.ProviderLaboratoryId : worklistToBioHubItemMaterial.Material.ProviderBioHubFacilityId;
                shipmentPublicViewModel.ShipmentMaterials.Add(shipmentPublicMaterialViewModel);
            }
        }

        return shipmentPublicViewModel;
    }
}