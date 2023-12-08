namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByLaboratoryId;

public record struct ListUsersByLaboratoryIdForWorklistFromBioHubItemQuery(Guid LaboratoryId, Guid WorklistFromBioHubItemId, IEnumerable<string> UserPermissions) { }