using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.ListShipments;

public record struct ListShipmentsQuery(
    RoleType? RoleType,
    Guid? LaboratoryId,
    Guid? BioHubFacilityId);