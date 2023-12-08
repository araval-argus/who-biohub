namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.UpdateShipment;

public record struct UpdateShipmentCommand(Guid Id, string ReferenceNumber) { }