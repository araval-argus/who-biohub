using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.DownloadWorklistToBioHubItemFile;

public record struct DownloadWorklistToBioHubItemFileQueryResponse(HttpResponseMessage DownloadedFile);