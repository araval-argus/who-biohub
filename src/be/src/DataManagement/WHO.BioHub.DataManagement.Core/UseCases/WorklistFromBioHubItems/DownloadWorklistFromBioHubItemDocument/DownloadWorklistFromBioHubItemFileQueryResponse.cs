using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.DownloadWorklistFromBioHubItemFile;

public record struct DownloadWorklistFromBioHubItemFileQueryResponse(HttpResponseMessage DownloadedFile);