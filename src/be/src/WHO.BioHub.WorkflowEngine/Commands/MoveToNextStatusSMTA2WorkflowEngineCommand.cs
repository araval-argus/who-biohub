using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.WorkflowEngine.Commands;

public record struct MoveToNextStatusSMTA2WorkflowEngineCommand(
    Guid Id,
    Guid? UserId,
    Guid? OriginalDocumentTemplateSMTA2DocumentId,
    DocumentFileType? DocumentTemplateFileType,
    IFormFile? File,
    bool IsSaveDraft
);

