using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Shipments.ReadShipment;

public record struct ReadShipmentQuery(Guid Id,
    RoleType? RoleType,
    Guid? LaboratoryId,
    Guid? BioHubFacilityId,
    IEnumerable<string> UserPermissions
   )
{ }