using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Shipments.ListShipments;

public record struct ListShipmentsQueryResponse(IEnumerable<ShipmentPublicViewModel> Shipments) { }