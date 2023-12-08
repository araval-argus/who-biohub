using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ListMapLaboratories;

public record struct ListMapLaboratoriesQuery(
    RoleType? RoleType,
    Guid? LaboratoryId,
    Guid? BioHubFacilityId);