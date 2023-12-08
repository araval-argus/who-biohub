using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.WorkflowEngine.Commands;

public record struct MoveToNextStatusSMTA1WorkflowEngineCommand(
    Guid Id,
    Guid? UserId,
    Guid? OriginalDocumentTemplateSMTA1DocumentId,
    DocumentFileType? DocumentTemplateFileType,
    IFormFile? File,
    bool IsSaveDraft
);

