using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.ReadWorklistFromBioHubHistoryItem;

public interface IListWorklistFromBioHubHistoryItemMapper
{
    List<WorklistFromBioHubItemDto> Map(List<WorklistFromBioHubHistoryItem> entities, IEnumerable<string> userPermissions);
}

public class ListWorklistFromBioHubHistoryItemMapper : IListWorklistFromBioHubHistoryItemMapper
{
    public List<WorklistFromBioHubItemDto> Map(List<WorklistFromBioHubHistoryItem> entities, IEnumerable<string> userPermissions)
    {
        List<WorklistFromBioHubItemDto> list = new List<WorklistFromBioHubItemDto>();

        foreach (WorklistFromBioHubHistoryItem entity in entities)
        {
            WorklistFromBioHubItemDto worklistfrombiohubitemDto = new()
            {
                Id = entity.Id,
                CurrentStatus = entity.Status,
                CurrentStatusName = entity.LastSubmissionApproved != false ? entity.Status.StatusName() : entity.Status.WorklistItemRejectedTitle(), 
                PreviousStatus = entity.PreviousStatus,
                WorklistItemTitle = entity.WorklistItemTitle,
                OperationDate = entity.OperationDate,
                LastSubmissionApproved = entity.LastSubmissionApproved,
                LaboratoryName = entity.RequestInitiationToLaboratory.Name,
                BioHubFacilityName = entity.RequestInitiationFromBioHubFacility != null ? entity.RequestInitiationFromBioHubFacility.Name : string.Empty,
                UserName = entity.LastOperationUser.FirstName + " " + entity.LastOperationUser.LastName,
                Comment = entity.Comment,
                SMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.SMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.Id,
                OriginalDocumentTemplateSMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.SMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId,
                SMTA2DocumentName = GetDocumentName(entity, DocumentFileType.SMTA2),
                Annex2OfSMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.Id,
                OriginalDocumentTemplateAnnex2OfSMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId,
                Annex2OfSMTA2DocumentName = GetDocumentName(entity, DocumentFileType.Annex2OfSMTA2),
                Annex2OfSMTA2SignatureId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA2).FirstOrDefault(x => x.IsDocumentFile == false)?.Id,

                BiosafetyChecklistOfSMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BiosafetyChecklist).FirstOrDefault(x => x.IsDocumentFile != false)?.Id,
                OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BiosafetyChecklist).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId,
                BiosafetyChecklistOfSMTA2DocumentName = GetDocumentName(entity, DocumentFileType.BiosafetyChecklist),
                BiosafetyChecklistOfSMTA2SignatureId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BiosafetyChecklist).FirstOrDefault(x => x.IsDocumentFile == false)?.Id,


                BookingFormOfSMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.Id,
                OriginalDocumentTemplateBookingFormOfSMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId,
                BookingFormOfSMTA2DocumentName = GetDocumentName(entity, DocumentFileType.BookingFormOfSMTA2),
                BookingFormOfSMTA2SignatureId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA2).FirstOrDefault(x => x.IsDocumentFile == false)?.Id,

                BioHubFacilityId = entity.RequestInitiationFromBioHubFacilityId,
                LaboratoryId = entity.RequestInitiationToLaboratoryId,
                HistoryDto = true,
                UserRoleName = entity.LastOperationUser.Role.Name,
                UserRoleTypeName = entity.LastOperationUser.Role.RoleType.ToString(),
                UserRoleType = entity.LastOperationUser.Role.RoleType,

                IsPast = entity.IsPast,
            };

            SetAnnex2OfSMTA2DocumentInfo(worklistfrombiohubitemDto, entity, userPermissions);
            SetBiosafetyChecklistDocumentInfo(worklistfrombiohubitemDto, entity, userPermissions);
            SetBookingFormOfSMTA2DocumentInfo(worklistfrombiohubitemDto, entity, userPermissions);
            list.Add(worklistfrombiohubitemDto);
        }

        return list;
    }

    private string GetDocumentName(WorklistFromBioHubHistoryItem entity, DocumentFileType type)
    {
        string name = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile != false)?.Name;
        string extension = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile != false)?.Extension;

        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(extension))
        {
            return name + "." + extension.ToLower();
        }
        return string.Empty;
    }


    private void SetAnnex2OfSMTA2DocumentInfo(WorklistFromBioHubItemDto worklistfrombiohubitemDto, WorklistFromBioHubHistoryItem entity, IEnumerable<string> userPermissions)
    {
        switch (entity.Status)
        {
            case WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2:
            case WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval:
                var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(entity.Status, PermissionType.DownloadFile, entity.IsPast);

                if (userPermissions.Contains(requiredPermission))
                {
                    worklistfrombiohubitemDto.Annex2OfSMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.Id;
                    worklistfrombiohubitemDto.OriginalDocumentTemplateAnnex2OfSMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;
                    worklistfrombiohubitemDto.Annex2OfSMTA2DocumentName = GetDocumentName(entity, DocumentFileType.Annex2OfSMTA2);
                }
                break;

            default:
                break;
        }
    }

    private void SetBiosafetyChecklistDocumentInfo(WorklistFromBioHubItemDto worklistfrombiohubitemDto, WorklistFromBioHubHistoryItem entity, IEnumerable<string> userPermissions)
    {
        switch (entity.Status)
        {
            case WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2:
            case WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval:

                var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(entity.Status, PermissionType.DownloadFile, entity.IsPast);

                if (userPermissions.Contains(requiredPermission))
                {
                    worklistfrombiohubitemDto.BiosafetyChecklistOfSMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BiosafetyChecklist).FirstOrDefault(x => x.IsDocumentFile != false)?.Id;
                    worklistfrombiohubitemDto.BiosafetyChecklistOfSMTA2DocumentName = GetDocumentName(entity, DocumentFileType.BiosafetyChecklist);
                    worklistfrombiohubitemDto.OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BiosafetyChecklist).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;
                }
                break;
            default:
                break;
        }
    }


    private void SetBookingFormOfSMTA2DocumentInfo(WorklistFromBioHubItemDto worklistfrombiohubitemDto, WorklistFromBioHubHistoryItem entity, IEnumerable<string> userPermissions)
    {
        switch (entity.Status)
        {
            case WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2:
            case WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval:

                var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(entity.Status, PermissionType.DownloadFile, entity.IsPast);

                if (userPermissions.Contains(requiredPermission))
                {
                    worklistfrombiohubitemDto.BookingFormOfSMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.Id;
                    worklistfrombiohubitemDto.OriginalDocumentTemplateBookingFormOfSMTA2DocumentId = entity.WorklistFromBioHubHistoryItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;
                    worklistfrombiohubitemDto.BookingFormOfSMTA2DocumentName = GetDocumentName(entity, DocumentFileType.BookingFormOfSMTA2);
                }
                break;
            default:
                break;
        }
    }
}