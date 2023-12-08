using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.DownloadSMTA1WorkflowHistoryItemFile;

public record struct DownloadSMTA1WorkflowHistoryItemFileQueryResponse(HttpResponseMessage DownloadedFile);