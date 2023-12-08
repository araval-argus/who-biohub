using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.ReadSMTA1WorkflowHistoryItem;

public interface IListSMTA1WorkflowHistoryItemMapper
{
    List<SMTA1WorkflowItemDto> Map(List<SMTA1WorkflowHistoryItem> entities, IEnumerable<string> userPermissions);
}

public class ListSMTA1WorkflowHistoryItemMapper : IListSMTA1WorkflowHistoryItemMapper
{
    public List<SMTA1WorkflowItemDto> Map(List<SMTA1WorkflowHistoryItem> entities, IEnumerable<string> userPermissions)
    {        
        List<SMTA1WorkflowItemDto> list = new List<SMTA1WorkflowItemDto>();

        foreach (SMTA1WorkflowHistoryItem entity in entities)
        {

            SMTA1WorkflowItemDto SMTA1WorkflowItemDto = new()
            {
                Id = entity.Id,
                CurrentStatus = entity.Status,
                CurrentStatusName = entity.LastSubmissionApproved != false ? entity.Status.StatusName() : entity.Status.WorklistItemRejectedTitle(),
                PreviousStatus = entity.PreviousStatus,
                WorkflowItemTitle = entity.WorkflowItemTitle,
                OperationDate = entity.OperationDate,
                LastSubmissionApproved = entity.LastSubmissionApproved,
                LaboratoryName = entity.Laboratory.Name,
                UserName = entity.LastOperationUser.FirstName + " " + entity.LastOperationUser.LastName,
                Comment = entity.Comment,
                LaboratoryId = entity.LaboratoryId,
                HistoryDto = true,
                UserRoleName = entity.LastOperationUser.Role.Name,
                UserRoleTypeName = entity.LastOperationUser.Role.RoleType.ToString(),
                UserRoleType = entity.LastOperationUser.Role.RoleType,
                IsPast = entity.IsPast,

            };
            SetSMTA1DocumentInfo(SMTA1WorkflowItemDto, entity, userPermissions);

            list.Add(SMTA1WorkflowItemDto);
        }

        return list;
    }

    private string GetDocumentName(SMTA1WorkflowHistoryItem entity, DocumentFileType type)
    {
        string name = entity.SMTA1WorkflowHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile != false)?.Name;
        string extension = entity.SMTA1WorkflowHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile != false)?.Extension;

        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(extension))
        {
            return name + "." + extension.ToLower();
        }
        return string.Empty;
    }

    private void SetSMTA1DocumentInfo(SMTA1WorkflowItemDto SMTA1WorkflowItemDto, SMTA1WorkflowHistoryItem entity, IEnumerable<string> userPermissions)
    {
        switch (entity.Status)
        {
            case SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval:
                var requiredPermission = StatusPermissionMapper.GetSMTA1WorkflowStatusPermission(entity.Status, PermissionType.DownloadFile, entity.IsPast);

                if (userPermissions.Contains(requiredPermission))
                {
                    SMTA1WorkflowItemDto.SMTA1DocumentId = entity.SMTA1WorkflowHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.SMTA1).FirstOrDefault(x => x.IsDocumentFile != false)?.Id;
                    SMTA1WorkflowItemDto.OriginalDocumentTemplateSMTA1DocumentId = entity.SMTA1WorkflowHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.SMTA1).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;
                    SMTA1WorkflowItemDto.SMTA1DocumentName = GetDocumentName(entity, DocumentFileType.SMTA1);
                }
                break;

            default:
                break;
        }
    }

}