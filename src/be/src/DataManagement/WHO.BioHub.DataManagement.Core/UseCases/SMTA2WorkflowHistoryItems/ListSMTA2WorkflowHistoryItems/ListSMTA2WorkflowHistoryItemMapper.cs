using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ReadSMTA2WorkflowHistoryItem;

public interface IListSMTA2WorkflowHistoryItemMapper
{
    List<SMTA2WorkflowItemDto> Map(List<SMTA2WorkflowHistoryItem> entities, IEnumerable<string> userPermissions);
}

public class ListSMTA2WorkflowHistoryItemMapper : IListSMTA2WorkflowHistoryItemMapper
{
    public List<SMTA2WorkflowItemDto> Map(List<SMTA2WorkflowHistoryItem> entities, IEnumerable<string> userPermissions)
    {        
        List<SMTA2WorkflowItemDto> list = new List<SMTA2WorkflowItemDto>();

        foreach (SMTA2WorkflowHistoryItem entity in entities)
        {

            SMTA2WorkflowItemDto SMTA2WorkflowItemDto = new()
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
            SetSMTA2DocumentInfo(SMTA2WorkflowItemDto, entity, userPermissions);

            list.Add(SMTA2WorkflowItemDto);
        }

        return list;
    }

    private string GetDocumentName(SMTA2WorkflowHistoryItem entity, DocumentFileType type)
    {
        string name = entity.SMTA2WorkflowHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile != false)?.Name;
        string extension = entity.SMTA2WorkflowHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile != false)?.Extension;

        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(extension))
        {
            return name + "." + extension.ToLower();
        }
        return string.Empty;
    }

    private void SetSMTA2DocumentInfo(SMTA2WorkflowItemDto SMTA2WorkflowItemDto, SMTA2WorkflowHistoryItem entity, IEnumerable<string> userPermissions)
    {
        switch (entity.Status)
        {
            case SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval:
                var requiredPermission = StatusPermissionMapper.GetSMTA2WorkflowStatusPermission(entity.Status, PermissionType.DownloadFile, entity.IsPast);

                if (userPermissions.Contains(requiredPermission))
                {
                    SMTA2WorkflowItemDto.SMTA2DocumentId = entity.SMTA2WorkflowHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.SMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.Id;
                    SMTA2WorkflowItemDto.OriginalDocumentTemplateSMTA2DocumentId = entity.SMTA2WorkflowHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.SMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;
                    SMTA2WorkflowItemDto.SMTA2DocumentName = GetDocumentName(entity, DocumentFileType.SMTA2);
                }
                break;

            default:
                break;
        }
    }

}