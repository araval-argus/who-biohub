using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ListDocuments;

public record struct ListDocumentsQueryResponse(IEnumerable<DocumentItemDto> Documents) { }