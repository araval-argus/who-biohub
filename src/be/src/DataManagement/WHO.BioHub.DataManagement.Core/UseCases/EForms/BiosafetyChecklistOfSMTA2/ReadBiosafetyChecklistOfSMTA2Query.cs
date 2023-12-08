using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Data.Core.UseCases.EForms.BiosafetyChecklistOfSMTA2;

public record struct ReadBiosafetyChecklistOfSMTA2Query(Guid WorklistId, RoleType? RoleType, Guid? BioHubFacilityId, Guid? LaboratoryId) { }