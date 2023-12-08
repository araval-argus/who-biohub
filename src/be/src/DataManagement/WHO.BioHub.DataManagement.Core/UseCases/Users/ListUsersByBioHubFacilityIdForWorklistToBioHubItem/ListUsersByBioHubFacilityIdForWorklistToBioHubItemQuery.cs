namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByBioHubFacilityId;

public record struct ListUsersByBioHubFacilityIdForWorklistToBioHubItemQuery(Guid BioHubFacilityId, Guid WorklistToBioHubItemId, IEnumerable<string> UserPermissions) { }