using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.EForms.ListEForms;

public record struct ListEFormsQuery(RoleType? RoleType, Guid? LaboratoryId, Guid? BioHubFacilityId) { }