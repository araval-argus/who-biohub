namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ListDocuments;

public record struct ListDocumentsQuery(Guid? LaboratoryId, Guid? BioHubFacilityId) { }