using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.ReadShipment;

public record struct ReadShipmentQueryResponse(ShipmentViewModel Shipment) { }