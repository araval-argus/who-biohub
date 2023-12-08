namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ListSignedSMTADocuments;

public record struct ListSignedSMTADocumentsQuery(Guid? LaboratoryId, Guid? BioHubFacilityId) { }