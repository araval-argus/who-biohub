using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ReadWorklistToBioHubHistoryItem;

public interface IListWorklistToBioHubHistoryItemMapper
{
    List<WorklistToBioHubItemDto> Map(List<WorklistToBioHubHistoryItem> entities, IEnumerable<string> userPermissions);
}

public class ListWorklistToBioHubHistoryItemMapper : IListWorklistToBioHubHistoryItemMapper
{
    public List<WorklistToBioHubItemDto> Map(List<WorklistToBioHubHistoryItem> entities, IEnumerable<string> userPermissions)
    {
        List<WorklistToBioHubItemDto> list = new List<WorklistToBioHubItemDto>();

        foreach (WorklistToBioHubHistoryItem entity in entities)
        {

            WorklistToBioHubItemDto worklisttobiohubitemDto = new()
            {
                Id = entity.Id,
                CurrentStatus = entity.Status,
                CurrentStatusName = entity.LastSubmissionApproved != false ? entity.Status.StatusName() : entity.Status.WorklistItemRejectedTitle(), 
                PreviousStatus = entity.PreviousStatus,
                WorklistItemTitle = entity.WorklistItemTitle,
                OperationDate = entity.OperationDate,
                LastSubmissionApproved = entity.LastSubmissionApproved,
                LaboratoryName = entity.RequestInitiationFromLaboratory.Name,
                BioHubFacilityName = entity.RequestInitiationToBioHubFacility != null ? entity.RequestInitiationToBioHubFacility.Name : string.Empty,
                UserName = entity.LastOperationUser.FirstName + " " + entity.LastOperationUser.LastName,
                Comment = entity.Comment,

                BioHubFacilityId = entity.RequestInitiationToBioHubFacilityId,
                LaboratoryId = entity.RequestInitiationFromLaboratoryId,
                HistoryDto = true,
                UserRoleName = entity.LastOperationUser.Role.Name,
                UserRoleTypeName = entity.LastOperationUser.Role.RoleType.ToString(),
                UserRoleType = entity.LastOperationUser.Role.RoleType,
                IsPast = entity.IsPast,

            };
            SetAnnex2OfSMTA1DocumentInfo(worklisttobiohubitemDto, entity, userPermissions);
            SetBookingFormOfSMTA1DocumentInfo(worklisttobiohubitemDto, entity, userPermissions);

            list.Add(worklisttobiohubitemDto);
        }

        return list;
    }

    private string GetDocumentName(WorklistToBioHubHistoryItem entity, DocumentFileType type)
    {
        string name = entity.WorklistToBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile != false)?.Name;
        string extension = entity.WorklistToBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile != false)?.Extension;

        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(extension))
        {
            return name + "." + extension.ToLower();
        }
        return string.Empty;
    }

    private void SetAnnex2OfSMTA1DocumentInfo(WorklistToBioHubItemDto worklisttobiohubitemDto, WorklistToBioHubHistoryItem entity, IEnumerable<string> userPermissions)
    {
        switch (entity.Status)
        {
            case WorklistToBioHubStatus.SubmitAnnex2OfSMTA1:
            case WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval:
                var requiredPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(entity.Status, PermissionType.DownloadFile, entity.IsPast);

                if (userPermissions.Contains(requiredPermission))
                {
                    worklisttobiohubitemDto.Annex2OfSMTA1DocumentId = entity.WorklistToBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA1).FirstOrDefault(x => x.IsDocumentFile != false)?.Id;
                    worklisttobiohubitemDto.OriginalDocumentTemplateAnnex2OfSMTA1DocumentId = entity.WorklistToBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA1).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;
                    worklisttobiohubitemDto.Annex2OfSMTA1DocumentName = GetDocumentName(entity, DocumentFileType.Annex2OfSMTA1);
                }
                break;

            default:
                break;
        }
    }


    private void SetBookingFormOfSMTA1DocumentInfo(WorklistToBioHubItemDto worklisttobiohubitemDto, WorklistToBioHubHistoryItem entity, IEnumerable<string> userPermissions)
    {
        switch (entity.Status)
        {
            case WorklistToBioHubStatus.SubmitBookingFormOfSMTA1:
            case WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval:

                var requiredPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(entity.Status, PermissionType.DownloadFile, entity.IsPast);

                if (userPermissions.Contains(requiredPermission))
                {
                    worklisttobiohubitemDto.BookingFormOfSMTA1DocumentId = entity.WorklistToBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA1).FirstOrDefault(x => x.IsDocumentFile != false)?.Id;
                    worklisttobiohubitemDto.OriginalDocumentTemplateBookingFormOfSMTA1DocumentId = entity.WorklistToBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA1).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;
                    worklisttobiohubitemDto.BookingFormOfSMTA1DocumentName = GetDocumentName(entity, DocumentFileType.BookingFormOfSMTA1);
                }
                break;
            default:
                break;
        }
    }
}