namespace WHO.BioHub.DataManagement.Core.UseCases.Users.ListUsersByBioHubFacilityId;

public record struct ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQuery(Guid BioHubFacilityId, Guid WorklistFromBioHubItemId, IEnumerable<string> UserPermissions) { }