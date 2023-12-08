using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ReadDocument;

public record struct ReadDocumentQueryResponse(HttpResponseMessage DownloadedFile) { }