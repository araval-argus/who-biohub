using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ReadSMTA1WorkflowItem;

public interface IReadSMTA1WorkflowItemMapper
{
    SMTA1WorkflowItemDto Map(SMTA1WorkflowItem entity, IEnumerable<string> userPermissions);
    SMTA1WorkflowItemDto MapMinimal(SMTA1WorkflowItem entity, IEnumerable<string> userPermissions);
}

public class ReadSMTA1WorkflowItemMapper : IReadSMTA1WorkflowItemMapper
{

    public SMTA1WorkflowItemDto MapMinimal(SMTA1WorkflowItem entity, IEnumerable<string> userPermissions)
    {        

        SMTA1WorkflowItemDto SMTA1WorkflowItemDto = CreateDto(entity);

        SetSMTA1DocumentInfo(SMTA1WorkflowItemDto, entity, userPermissions);


        return SMTA1WorkflowItemDto;
    }

    public SMTA1WorkflowItemDto Map(SMTA1WorkflowItem entity, IEnumerable<string> userPermissions)
    {        

        SMTA1WorkflowItemDto SMTA1WorkflowItemDto = CreateDto(entity);

        switch (entity.Status)
        {
            case SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval:

                SetSMTA1DocumentInfo(SMTA1WorkflowItemDto, entity, userPermissions);

                break;
        }


        return SMTA1WorkflowItemDto;
    }

    private SMTA1WorkflowItemDto CreateDto(SMTA1WorkflowItem entity)
    {
        return new()
        {
            Id = entity.Id,
            CurrentStatus = entity.Status,
            CurrentStatusName = entity.LastSubmissionApproved != false ? entity.Status.StatusName() : entity.Status.WorklistItemRejectedTitle(),
            PreviousStatus = entity.PreviousStatus,
            WorkflowItemTitle = entity.WorkflowItemTitle,
            OperationDate = entity.OperationDate,
            LastSubmissionApproved = entity.LastSubmissionApproved,
            UserName = entity.LastOperationUser.FirstName + " " + entity.LastOperationUser.LastName,
            Comment = entity.Comment,
            ReferenceId = entity.ReferenceId,
            LaboratoryId = entity.LaboratoryId,
            HistoryDto = false,
            UserRoleName = entity.LastOperationUser.Role.Name,
            UserRoleTypeName = entity.LastOperationUser.Role.RoleType.ToString(),
            UserRoleType = entity.LastOperationUser.Role.RoleType,
            LaboratoryName = entity.Laboratory.Name,
            IsPast = entity.IsPast,
        };
    }

    private string GetDocumentName(SMTA1WorkflowItem entity, DocumentFileType type)
    {
        string name = entity.SMTA1WorkflowItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile)?.Name;
        string extension = entity.SMTA1WorkflowItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile)?.Extension;

        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(extension))
        {
            return name + "." + extension.ToLower();
        }
        return string.Empty;
    }


    private void SetSMTA1DocumentInfo(SMTA1WorkflowItemDto SMTA1WorkflowItemDto, SMTA1WorkflowItem entity, IEnumerable<string> UserPermissions)
    {
        switch (entity.Status)
        {
            case SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval:
                var requiredPermission = StatusPermissionMapper.SMTA1WorkflowStatusDownloadFilePermissionMapper[entity.Status];

                if (UserPermissions.Contains(requiredPermission))
                {
                    SMTA1WorkflowItemDto.OriginalDocumentTemplateSMTA1DocumentId = entity.SMTA1WorkflowItemDocuments.Where(x => x.Type == DocumentFileType.SMTA1).Select(x => x.Document).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;
                    SMTA1WorkflowItemDto.SMTA1DocumentId = entity.SMTA1WorkflowItemDocuments.Where(x => x.Type == DocumentFileType.SMTA1).FirstOrDefault(x => x.IsDocumentFile != false)?.DocumentId;
                    SMTA1WorkflowItemDto.SMTA1DocumentName = GetDocumentName(entity, DocumentFileType.SMTA1);
                }
                break;

            default:
                break;
        }
    }
}