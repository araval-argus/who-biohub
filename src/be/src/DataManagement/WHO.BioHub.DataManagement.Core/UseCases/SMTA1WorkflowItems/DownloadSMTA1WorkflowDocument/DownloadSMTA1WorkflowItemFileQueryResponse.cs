using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.DownloadSMTA1WorkflowItemFile;

public record struct DownloadSMTA1WorkflowItemFileQueryResponse(HttpResponseMessage DownloadedFile);