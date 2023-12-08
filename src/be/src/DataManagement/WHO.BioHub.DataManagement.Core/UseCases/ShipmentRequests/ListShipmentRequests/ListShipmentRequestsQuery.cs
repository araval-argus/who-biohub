using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.ShipmentRequests.ListShipmentRequests;

public record struct ListShipmentRequestsQuery(
    RoleType? RoleType,
    Guid? LaboratoryId,
    Guid? BioHubFacilityId,
    IEnumerable<string> UserPermissions);