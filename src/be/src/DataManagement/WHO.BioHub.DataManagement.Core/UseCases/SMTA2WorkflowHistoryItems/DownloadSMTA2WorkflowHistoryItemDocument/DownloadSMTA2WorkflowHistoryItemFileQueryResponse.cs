using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.DownloadSMTA2WorkflowHistoryItemFile;

public record struct DownloadSMTA2WorkflowHistoryItemFileQueryResponse(HttpResponseMessage DownloadedFile);