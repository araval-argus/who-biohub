using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.DownloadWorklistToBioHubHistoryItemFile;

public record struct DownloadWorklistToBioHubHistoryItemFileQueryResponse(HttpResponseMessage DownloadedFile);