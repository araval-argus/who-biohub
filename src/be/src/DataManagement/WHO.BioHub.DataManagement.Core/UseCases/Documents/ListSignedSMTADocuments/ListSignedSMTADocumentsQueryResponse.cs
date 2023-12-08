using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ListSignedSMTADocuments;

public record struct ListSignedSMTADocumentsQueryResponse(IEnumerable<DocumentItemDto> Documents) { }