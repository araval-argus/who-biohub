using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.CheckDocumentSigned;

public record struct CheckDocumentSignedQuery(Guid? LaboratoryId, DocumentFileType Type) { }