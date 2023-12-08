using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Shipments.ReadShipment;

public record struct ReadShipmentQueryResponse(ShipmentPublicViewModel Shipment) { }