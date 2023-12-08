using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListBioHubFacilities;

public record struct ListBioHubFacilitiesQuery(
    RoleType RoleType);