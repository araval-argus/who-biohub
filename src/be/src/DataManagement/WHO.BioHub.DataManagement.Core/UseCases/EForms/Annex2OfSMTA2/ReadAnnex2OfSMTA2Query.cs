using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Data.Core.UseCases.EForms.Annex2OfSMTA2;

public record struct ReadAnnex2OfSMTA2Query(Guid WorklistId, RoleType? RoleType, Guid? BioHubFacilityId, Guid? LaboratoryId) { }