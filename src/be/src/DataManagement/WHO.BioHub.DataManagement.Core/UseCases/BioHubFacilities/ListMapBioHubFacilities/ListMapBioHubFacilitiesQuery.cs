using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListMapBioHubFacilities;

public record struct ListMapBioHubFacilitiesQuery(
    RoleType RoleType,
    Guid? LaboratoryId);