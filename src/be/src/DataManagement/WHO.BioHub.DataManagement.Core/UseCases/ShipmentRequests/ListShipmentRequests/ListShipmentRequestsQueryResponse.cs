using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.ShipmentRequests.ListShipmentRequests;

public record struct ListShipmentRequestsQueryResponse(IEnumerable<ShipmentRequestViewModel> ShipmentRequests) { }