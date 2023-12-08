namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;

public record struct ListMaterialsForWorklistFromBioHubItemQuery(Guid WorklistFromBioHubItemId, Guid BioHubFacilityId) { }