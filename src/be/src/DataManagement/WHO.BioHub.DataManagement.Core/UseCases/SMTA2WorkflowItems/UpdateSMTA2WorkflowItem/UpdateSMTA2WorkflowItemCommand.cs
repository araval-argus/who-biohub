using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.UpdateSMTA2WorkflowItem;

public record struct UpdateSMTA2WorkflowItemCommand(
    Guid Id,
    RoleType? RoleType,
    Guid? UserLaboratoryId,
    SMTA2WorkflowStatus CurrentStatus,
    Guid? UserId,
    bool? LastSubmissionApproved,
    string Comment,
    Guid? OriginalDocumentTemplateSMTA2DocumentId,
    DocumentFileType? DocumentTemplateFileType,
    IFormFile? File,
    IEnumerable<string> UserPermissions,
    bool IsSaveDraft,
    Guid? ReferenceId,
    bool? IsPast,
    DateTime? AssignedOperationDate
);

