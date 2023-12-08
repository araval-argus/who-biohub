using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.ListShipments;

public record struct ListShipmentsQueryResponse(IEnumerable<ShipmentViewModel> Shipments) { }