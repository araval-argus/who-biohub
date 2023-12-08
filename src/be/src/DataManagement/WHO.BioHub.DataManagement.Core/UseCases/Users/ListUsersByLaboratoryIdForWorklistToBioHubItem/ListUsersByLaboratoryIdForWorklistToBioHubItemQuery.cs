namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByLaboratoryId;

public record struct ListUsersByLaboratoryIdForWorklistToBioHubItemQuery(Guid LaboratoryId, Guid WorklistToBioHubItemId, IEnumerable<string> UserPermissions) { }