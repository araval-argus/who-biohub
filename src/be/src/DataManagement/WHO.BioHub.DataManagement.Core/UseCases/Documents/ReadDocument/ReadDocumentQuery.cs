namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ReadDocument;

public record struct ReadDocumentQuery(Guid Id, Guid? LaboratoryId, Guid? BioHubFacilityId) { }