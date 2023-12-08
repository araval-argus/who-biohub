using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTARequests.ListSMTARequests;

public record struct ListSMTARequestsQuery(
    RoleType? RoleType,
    Guid? LaboratoryId,
    Guid? BioHubFacilityId,
    IEnumerable<string> UserPermissions);