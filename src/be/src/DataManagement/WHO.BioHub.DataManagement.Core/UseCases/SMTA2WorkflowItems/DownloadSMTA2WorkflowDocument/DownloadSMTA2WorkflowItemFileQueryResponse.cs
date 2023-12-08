using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.DownloadSMTA2WorkflowItemFile;

public record struct DownloadSMTA2WorkflowItemFileQueryResponse(HttpResponseMessage DownloadedFile);