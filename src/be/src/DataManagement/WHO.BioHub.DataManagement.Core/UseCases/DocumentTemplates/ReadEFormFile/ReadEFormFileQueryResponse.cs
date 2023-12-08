using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ReadEFormFile;

public record struct ReadEFormFileQueryResponse(HttpResponseMessage DownloadedFile);