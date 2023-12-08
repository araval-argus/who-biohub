using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Data.Core.UseCases.EForms.Annex2OfSMTA1;

public record struct ReadAnnex2OfSMTA1Query(Guid WorklistId, RoleType? RoleType, Guid? BioHubFacilityId, Guid? LaboratoryId) { }