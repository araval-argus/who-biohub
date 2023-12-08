using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.UpdateSMTA1WorkflowItem;

public record struct UpdateSMTA1WorkflowItemCommand(
    Guid Id,
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    SMTA1WorkflowStatus CurrentStatus,
    Guid? UserId,
    bool? LastSubmissionApproved,
    string Comment,
    Guid? OriginalDocumentTemplateSMTA1DocumentId,
    DocumentFileType? DocumentTemplateFileType,
    IFormFile? File,
    IEnumerable<string> UserPermissions,
    bool IsSaveDraft,
    Guid? ReferenceId,
    bool? IsPast,
    DateTime? AssignedOperationDate
);

