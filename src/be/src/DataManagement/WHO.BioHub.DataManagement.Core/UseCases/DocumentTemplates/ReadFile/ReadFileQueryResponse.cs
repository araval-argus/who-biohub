using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.ReadFile;

public record struct ReadFileQueryResponse(HttpResponseMessage DownloadedFile);