using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.UpdateShipment;

public record struct UpdateShipmentCommandResponse(Guid Shipment) { }