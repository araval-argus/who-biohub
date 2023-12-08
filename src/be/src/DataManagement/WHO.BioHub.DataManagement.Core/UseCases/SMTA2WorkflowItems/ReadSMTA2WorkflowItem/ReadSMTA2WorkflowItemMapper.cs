using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ReadSMTA2WorkflowItem;

public interface IReadSMTA2WorkflowItemMapper
{
    SMTA2WorkflowItemDto Map(SMTA2WorkflowItem entity, IEnumerable<string> userPermissions);
    SMTA2WorkflowItemDto MapMinimal(SMTA2WorkflowItem entity, IEnumerable<string> userPermissions);
}

public class ReadSMTA2WorkflowItemMapper : IReadSMTA2WorkflowItemMapper
{

    public SMTA2WorkflowItemDto MapMinimal(SMTA2WorkflowItem entity, IEnumerable<string> userPermissions)
    {

        SMTA2WorkflowItemDto SMTA2WorkflowItemDto = CreateDto(entity);

        SetSMTA2DocumentInfo(SMTA2WorkflowItemDto, entity, userPermissions);


        return SMTA2WorkflowItemDto;
    }

    public SMTA2WorkflowItemDto Map(SMTA2WorkflowItem entity, IEnumerable<string> userPermissions)
    {

        SMTA2WorkflowItemDto SMTA2WorkflowItemDto = CreateDto(entity);

        switch (entity.Status)
        {
            case SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval:

                SetSMTA2DocumentInfo(SMTA2WorkflowItemDto, entity, userPermissions);

                break;
        }


        return SMTA2WorkflowItemDto;
    }

    private SMTA2WorkflowItemDto CreateDto(SMTA2WorkflowItem entity)
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

    private string GetDocumentName(SMTA2WorkflowItem entity, DocumentFileType type)
    {
        string name = entity.SMTA2WorkflowItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile)?.Name;
        string extension = entity.SMTA2WorkflowItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile)?.Extension;

        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(extension))
        {
            return name + "." + extension.ToLower();
        }
        return string.Empty;
    }


    private void SetSMTA2DocumentInfo(SMTA2WorkflowItemDto SMTA2WorkflowItemDto, SMTA2WorkflowItem entity, IEnumerable<string> UserPermissions)
    {
        switch (entity.Status)
        {
            case SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval:
                var requiredPermission = StatusPermissionMapper.GetSMTA2WorkflowStatusPermission(entity.Status, PermissionType.DownloadFile, entity.IsPast);

                if (UserPermissions.Contains(requiredPermission))
                {
                    SMTA2WorkflowItemDto.OriginalDocumentTemplateSMTA2DocumentId = entity.SMTA2WorkflowItemDocuments.Where(x => x.Type == DocumentFileType.SMTA2).Select(x => x.Document).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;
                    SMTA2WorkflowItemDto.SMTA2DocumentId = entity.SMTA2WorkflowItemDocuments.Where(x => x.Type == DocumentFileType.SMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.DocumentId;
                    SMTA2WorkflowItemDto.SMTA2DocumentName = GetDocumentName(entity, DocumentFileType.SMTA2);
                }
                break;

            default:
                break;
        }
    }
}