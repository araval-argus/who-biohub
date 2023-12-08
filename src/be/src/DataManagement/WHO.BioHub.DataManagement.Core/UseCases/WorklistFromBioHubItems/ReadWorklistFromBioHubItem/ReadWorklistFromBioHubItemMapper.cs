using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ReadWorklistFromBioHubItem;

public interface IReadWorklistFromBioHubItemMapper
{
    WorklistFromBioHubItemDto Map(WorklistFromBioHubItem entity, IEnumerable<string> userPermissions, IEnumerable<Annex2OfSMTA2Condition>? annex2OfSMTA2Conditions, IEnumerable<BiosafetyChecklistOfSMTA2>? biosafetyChecklistOfSMTA2);
    WorklistFromBioHubItemDto MapMinimal(WorklistFromBioHubItem entity, IEnumerable<string> userPermissions, IEnumerable<Annex2OfSMTA2Condition>? annex2OfSMTA2Conditions, IEnumerable<BiosafetyChecklistOfSMTA2>? biosafetyChecklistOfSMTA2);
}

public class ReadWorklistFromBioHubItemMapper : IReadWorklistFromBioHubItemMapper
{
    public WorklistFromBioHubItemDto MapMinimal(WorklistFromBioHubItem entity, IEnumerable<string> userPermissions, IEnumerable<Annex2OfSMTA2Condition>? annex2OfSMTA2Conditions, IEnumerable<BiosafetyChecklistOfSMTA2>? biosafetyChecklistOfSMTA2)
    {
        WorklistFromBioHubItemDto worklistfrombiohubitemDto = CreateDto(entity);

        SetAnnex2OfSMTA2DocumentInfo(worklistfrombiohubitemDto, entity, userPermissions);
        SetBiosafetyChecklistDocumentInfo(worklistfrombiohubitemDto, entity, userPermissions);
        SetBookingFormOfSMTA2DocumentInfo(worklistfrombiohubitemDto, entity, userPermissions);

        switch (entity.Status)
        {

            case WorklistFromBioHubStatus.WaitForPickUpCompleted:
            case WorklistFromBioHubStatus.WaitForDeliveryCompleted:
            case WorklistFromBioHubStatus.WaitForArrivalConditionCheck:
            case WorklistFromBioHubStatus.WaitForCommentQESendFeedback:
            case WorklistFromBioHubStatus.WaitForFinalApproval:
            case WorklistFromBioHubStatus.ShipmentCompleted:
                SetShipmentDocuments(entity, userPermissions, worklistfrombiohubitemDto);
                break;
        }

        return worklistfrombiohubitemDto;
    }



    public WorklistFromBioHubItemDto Map(WorklistFromBioHubItem entity, IEnumerable<string> userPermissions, IEnumerable<Annex2OfSMTA2Condition>? annex2OfSMTA2Conditions, IEnumerable<BiosafetyChecklistOfSMTA2>? biosafetyChecklistOfSMTA2)
    {

        WorklistFromBioHubItemDto worklistfrombiohubitemDto = CreateDto(entity);

        switch (entity.Status)
        {

            case WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2:
            case WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval:

                SetAnnex2OfSMTA2DocumentInfo(worklistfrombiohubitemDto, entity, userPermissions);


                //#54317
                //worklistfrombiohubitemDto.Annex2OfSMTA2SignatureId = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA2).FirstOrDefault(x => x.IsDocumentFile == false)?.Id;
                //worklistfrombiohubitemDto.Annex2OfSMTA2SignatureString = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA2).FirstOrDefault(x => x.IsDocumentFile == false)?.Base64String;
                worklistfrombiohubitemDto.Annex2OfSMTA2SignatureText = entity.Annex2OfSMTA2SignatureText;
                ////////////


                worklistfrombiohubitemDto.Annex2FillingOption = entity.Annex2FillingOption ?? FillingOption.ElectronicallyFill;
                worklistfrombiohubitemDto.WorklistFromBioHubItemMaterials = GetMaterials(entity);
                worklistfrombiohubitemDto.WorklistFromBioHubItemAnnex2OfSMTA2Conditions = GetAnnex2OfSMTA2Conditions(entity, annex2OfSMTA2Conditions);
                worklistfrombiohubitemDto.LaboratoryFocalPoints = GetLaboratoryFocalPoints(entity);
                worklistfrombiohubitemDto.WHODocumentRegistrationNumber = entity.WHODocumentRegistrationNumber;
                worklistfrombiohubitemDto.BioHubFacilityAddress = entity.RequestInitiationFromBioHubFacility != null ? entity.RequestInitiationFromBioHubFacility.Address : String.Empty;
                worklistfrombiohubitemDto.BioHubFacilityCountry = entity.RequestInitiationFromBioHubFacility != null ? entity.RequestInitiationFromBioHubFacility.Country.Name : String.Empty;

                worklistfrombiohubitemDto.LaboratoryCountry = entity.RequestInitiationToLaboratory != null ? entity.RequestInitiationToLaboratory.Country.Name : string.Empty;
                worklistfrombiohubitemDto.LastOperationUserFirstName = entity.Status == WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval ? entity.LastOperationUser.FirstName : string.Empty;
                worklistfrombiohubitemDto.LastOperationUserLastName = entity.Status == WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval ? entity.LastOperationUser.LastName : string.Empty;
                worklistfrombiohubitemDto.LastOperationUserJobTitle = entity.Status == WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval ? entity.LastOperationUser.JobTitle : string.Empty;


                break;

            case WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2:
            case WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval:


                SetBiosafetyChecklistDocumentInfo(worklistfrombiohubitemDto, entity, userPermissions);

                //#54317
                //worklistfrombiohubitemDto.BiosafetyChecklistOfSMTA2SignatureId = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BiosafetyChecklist).FirstOrDefault(x => x.IsDocumentFile == false)?.Id;
                //worklistfrombiohubitemDto.BiosafetyChecklistOfSMTA2SignatureString = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BiosafetyChecklist).FirstOrDefault(x => x.IsDocumentFile == false)?.Base64String;
                worklistfrombiohubitemDto.BiosafetyChecklistOfSMTA2SignatureText = entity.BiosafetyChecklistOfSMTA2SignatureText;
                ////////////////

                worklistfrombiohubitemDto.BiosafetyChecklistFillingOption = entity.BiosafetyChecklistFillingOption ?? FillingOption.ElectronicallyFill;
                worklistfrombiohubitemDto.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s = GetBiosafetyChecklists(entity, biosafetyChecklistOfSMTA2);
                worklistfrombiohubitemDto.NewBiosafetyChecklistThreadComment = entity.SavedBiosafetyChecklistThreadComment;

                worklistfrombiohubitemDto.BiosafetyChecklistThreadComments = GetBiosafetyChecklistComments(entity);
                worklistfrombiohubitemDto.PreviousOperationDate = entity.OperationDate;
                worklistfrombiohubitemDto.PreviousUserId = entity.LastOperationUserId;

                worklistfrombiohubitemDto.LaboratoryCountry = entity.RequestInitiationToLaboratory != null ? entity.RequestInitiationToLaboratory.Country.Name : string.Empty;
                worklistfrombiohubitemDto.LastOperationUserFirstName = entity.Status == WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval ? entity.LastOperationUser.FirstName : string.Empty;
                worklistfrombiohubitemDto.LastOperationUserLastName = entity.Status == WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval ? entity.LastOperationUser.LastName : string.Empty;
                worklistfrombiohubitemDto.LastOperationUserJobTitle = entity.Status == WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval ? entity.LastOperationUser.JobTitle : string.Empty;

                break;

            case WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2:
            case WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval:

                SetBookingFormOfSMTA2DocumentInfo(worklistfrombiohubitemDto, entity, userPermissions);

                worklistfrombiohubitemDto.BookingFormFillingOption = entity.BookingFormFillingOption ?? FillingOption.ElectronicallyFill;
                worklistfrombiohubitemDto.BookingForms = GetBookingForms(entity);
                worklistfrombiohubitemDto.WorklistFromBioHubItemMaterials = GetMaterials(entity);

                //#54317
                //worklistfrombiohubitemDto.BookingFormOfSMTA2SignatureId = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA2).FirstOrDefault(x => x.IsDocumentFile == false)?.Id;
                //worklistfrombiohubitemDto.BookingFormOfSMTA2SignatureString = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA2).FirstOrDefault(x => x.IsDocumentFile == false)?.Base64String;
                worklistfrombiohubitemDto.BookingFormOfSMTA2SignatureText = entity.BookingFormOfSMTA2SignatureText;
                ////////////////


                worklistfrombiohubitemDto.LastOperationUserFirstName = entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval ? entity.LastOperationUser.FirstName : string.Empty;
                worklistfrombiohubitemDto.LastOperationUserLastName = entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval ? entity.LastOperationUser.LastName : string.Empty;
                worklistfrombiohubitemDto.LastOperationUserEmail = entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval ? entity.LastOperationUser.Email : string.Empty;
                worklistfrombiohubitemDto.LastOperationUserJobTitle = entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval ? entity.LastOperationUser.JobTitle : string.Empty;
                worklistfrombiohubitemDto.LastOperationUserBusinessPhone = entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval ? entity.LastOperationUser.BusinessPhone : string.Empty;
                worklistfrombiohubitemDto.LastOperationUserMobilePhone = entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval ? entity.LastOperationUser.MobilePhone : string.Empty;
                worklistfrombiohubitemDto.BioHubFacilityAddress = entity.RequestInitiationFromBioHubFacility != null ? entity.RequestInitiationFromBioHubFacility.Address : string.Empty;
                worklistfrombiohubitemDto.BioHubFacilityCountry = entity.RequestInitiationFromBioHubFacility != null ? entity.RequestInitiationFromBioHubFacility.Country.Name : string.Empty;
                worklistfrombiohubitemDto.LaboratoryAddress = entity.RequestInitiationToLaboratory.Address;
                worklistfrombiohubitemDto.LaboratoryCountry = entity.RequestInitiationToLaboratory.Country.Name;
                worklistfrombiohubitemDto.LaboratoryFocalPoints = GetLaboratoryFocalPoints(entity);
                break;

            //case WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments:
            //case WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments:
            //    worklistfrombiohubitemDto.BHFShipmentDocuments = GetBHFShipmentDocuments(entity);
            //    worklistfrombiohubitemDto.QEShipmentDocuments = GetQEShipmentDocuments(entity);
            //    break;
            case WorklistFromBioHubStatus.WaitForPickUpCompleted:
                SetShipmentDocuments(entity, userPermissions, worklistfrombiohubitemDto);
                worklistfrombiohubitemDto.BookingForms = GetBookingForms(entity);
                break;
            case WorklistFromBioHubStatus.WaitForDeliveryCompleted:
                SetShipmentDocuments(entity, userPermissions, worklistfrombiohubitemDto);
                worklistfrombiohubitemDto.BookingForms = GetBookingForms(entity);
                break;

            case WorklistFromBioHubStatus.WaitForArrivalConditionCheck:
                SetShipmentDocuments(entity, userPermissions, worklistfrombiohubitemDto);
                worklistfrombiohubitemDto.WorklistFromBioHubItemMaterials = GetMaterials(entity);
                worklistfrombiohubitemDto.Feedbacks = GetFeedbacks(entity);
                break;
            case WorklistFromBioHubStatus.WaitForCommentQESendFeedback:
                SetShipmentDocuments(entity, userPermissions, worklistfrombiohubitemDto);
                worklistfrombiohubitemDto.WorklistFromBioHubItemMaterials = GetMaterials(entity);
                worklistfrombiohubitemDto.Feedbacks = GetFeedbacks(entity);
                break;

            case WorklistFromBioHubStatus.WaitForFinalApproval:
                SetShipmentDocuments(entity, userPermissions, worklistfrombiohubitemDto);
                worklistfrombiohubitemDto.WorklistFromBioHubItemMaterials = GetMaterials(entity);
                worklistfrombiohubitemDto.Feedbacks = GetFeedbacks(entity);
                break;

            case WorklistFromBioHubStatus.ShipmentCompleted:
                SetShipmentDocuments(entity, userPermissions, worklistfrombiohubitemDto);
                worklistfrombiohubitemDto.WorklistFromBioHubItemMaterials = GetMaterials(entity);
                break;
        }


        return worklistfrombiohubitemDto;
    }

    private static WorklistFromBioHubItemDto CreateDto(WorklistFromBioHubItem entity)
    {
        return new()
        {
            Id = entity.Id,
            CurrentStatus = entity.Status,
            CurrentStatusName = entity.LastSubmissionApproved != false ? entity.Status.StatusName() : entity.Status.WorklistItemRejectedTitle(),
            PreviousStatus = entity.PreviousStatus,
            WorklistItemTitle = entity.WorklistItemTitle,
            OperationDate = entity.OperationDate,
            LastSubmissionApproved = entity.LastSubmissionApproved,
            UserName = entity.LastOperationUser.FirstName + " " + entity.LastOperationUser.LastName,
            Comment = entity.Comment,
            ReferenceId = entity.ReferenceId,
            BioHubFacilityId = entity.RequestInitiationFromBioHubFacilityId,
            LaboratoryId = entity.RequestInitiationToLaboratoryId,
            HistoryDto = false,
            UserRoleName = entity.LastOperationUser.Role.Name,
            UserRoleTypeName = entity.LastOperationUser.Role.RoleType.ToString(),
            UserRoleType = entity.LastOperationUser.Role.RoleType,
            LaboratoryName = entity.RequestInitiationToLaboratory.Name,
            BioHubFacilityName = entity.RequestInitiationFromBioHubFacility != null ? entity.RequestInitiationFromBioHubFacility.Name : string.Empty,
            IsPast = entity.IsPast,
        };
    }

    private string GetDocumentName(WorklistFromBioHubItem entity, DocumentFileType type)
    {
        string name = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile)?.Name;
        string extension = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile)?.Extension;

        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(extension))
        {
            return name + "." + extension.ToLower();
        }
        return string.Empty;
    }



    private void SetAnnex2OfSMTA2DocumentInfo(WorklistFromBioHubItemDto worklistfrombiohubitemDto, WorklistFromBioHubItem entity, IEnumerable<string> userPermissions)
    {
        switch (entity.Status)
        {
            case WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2:
            case WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval:
                var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(entity.Status, PermissionType.DownloadFile, entity.IsPast);

                if (userPermissions.Contains(requiredPermission))
                {
                    worklistfrombiohubitemDto.Annex2OfSMTA2DocumentId = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.Id;
                    worklistfrombiohubitemDto.OriginalDocumentTemplateAnnex2OfSMTA2DocumentId = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;
                    worklistfrombiohubitemDto.Annex2OfSMTA2DocumentName = GetDocumentName(entity, DocumentFileType.Annex2OfSMTA2);
                }
                break;

            default:
                break;
        }
    }

    private void SetBiosafetyChecklistDocumentInfo(WorklistFromBioHubItemDto worklistfrombiohubitemDto, WorklistFromBioHubItem entity, IEnumerable<string> userPermissions)
    {
        switch (entity.Status)
        {
            case WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2:
            case WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval:

                var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(entity.Status, PermissionType.DownloadFile, entity.IsPast);

                if (userPermissions.Contains(requiredPermission))
                {
                    worklistfrombiohubitemDto.BiosafetyChecklistOfSMTA2DocumentId = entity.WorklistFromBioHubItemDocuments.Where(x => x.Type == DocumentFileType.BiosafetyChecklist).FirstOrDefault(x => x.IsDocumentFile != false)?.DocumentId;
                    worklistfrombiohubitemDto.BiosafetyChecklistOfSMTA2DocumentName = GetDocumentName(entity, DocumentFileType.BiosafetyChecklist);
                    worklistfrombiohubitemDto.OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentId = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BiosafetyChecklist).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;
                }
                break;
            default:
                break;
        }
    }


    private void SetBookingFormOfSMTA2DocumentInfo(WorklistFromBioHubItemDto worklistfrombiohubitemDto, WorklistFromBioHubItem entity, IEnumerable<string> userPermissions)
    {
        switch (entity.Status)
        {
            case WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2:
            case WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval:

                var requiredPermission = StatusPermissionMapper.GetWorklistFromBioHubStatusPermission(entity.Status, PermissionType.DownloadFile, entity.IsPast);

                if (userPermissions.Contains(requiredPermission))
                {
                    worklistfrombiohubitemDto.BookingFormOfSMTA2DocumentId = entity.WorklistFromBioHubItemDocuments.Where(x => x.Type == DocumentFileType.BookingFormOfSMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.DocumentId;
                    worklistfrombiohubitemDto.BookingFormOfSMTA2DocumentName = GetDocumentName(entity, DocumentFileType.BookingFormOfSMTA2);
                    worklistfrombiohubitemDto.OriginalDocumentTemplateBookingFormOfSMTA2DocumentId = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA2).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;

                }
                break;
            default:
                break;
        }
    }

    private IEnumerable<WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto> GetAnnex2OfSMTA2Conditions(WorklistFromBioHubItem entity, IEnumerable<Annex2OfSMTA2Condition> annex2OfSMTA2Conditions)
    {
        List<WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto> conditions = new List<WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto>();

        var worklistFromBioHubItemAnnex2OfSMTA2Conditions = entity.WorklistFromBioHubItemAnnex2OfSMTA2Conditions.ToList();


        foreach (var condition in annex2OfSMTA2Conditions)
        {
            var worklistFromBioHubItemAnnex2OfSMTA2Condition = worklistFromBioHubItemAnnex2OfSMTA2Conditions.Where(x => x.Annex2OfSMTA2ConditionId == condition.Id).FirstOrDefault();

            WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto annex2OfSMTA2ConditionDto = new WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto();
            annex2OfSMTA2ConditionDto.Id = worklistFromBioHubItemAnnex2OfSMTA2Condition != null ? worklistFromBioHubItemAnnex2OfSMTA2Condition.Id : Guid.NewGuid();
            annex2OfSMTA2ConditionDto.Annex2OfSMTA2ConditionId = condition.Id;
            annex2OfSMTA2ConditionDto.Comment = worklistFromBioHubItemAnnex2OfSMTA2Condition != null ? worklistFromBioHubItemAnnex2OfSMTA2Condition.Comment : String.Empty;
            annex2OfSMTA2ConditionDto.Flag = worklistFromBioHubItemAnnex2OfSMTA2Condition != null ? worklistFromBioHubItemAnnex2OfSMTA2Condition.Flag : (condition.FlagPresetValue != null ? condition.FlagPresetValue : false);
            annex2OfSMTA2ConditionDto.Order = condition.Order;
            annex2OfSMTA2ConditionDto.PointNumber = condition.PointNumber;
            annex2OfSMTA2ConditionDto.Condition = condition.Condition;
            annex2OfSMTA2ConditionDto.Mandatory = condition.Mandatory;
            annex2OfSMTA2ConditionDto.Selectable = condition.Selectable;


            conditions.Add(annex2OfSMTA2ConditionDto);

        }

        conditions = conditions.OrderBy(x => x.Order).ToList();

        return conditions;
    }


    private IEnumerable<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto> GetBiosafetyChecklists(WorklistFromBioHubItem entity, IEnumerable<BiosafetyChecklistOfSMTA2> biosafetyChecklistOfSMTA2)
    {
        List<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto> conditions = new List<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto>();

        var worklistFromBioHubItemBiosafetyChecklistOfSMTA2Conditions = entity.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s.Where(x => x.BiosafetyChecklistOfSMTA2.Current == true).ToList();


        foreach (var condition in biosafetyChecklistOfSMTA2)
        {
            var worklistFromBioHubItemBiosafetyChecklistOfSMTA2Condition = worklistFromBioHubItemBiosafetyChecklistOfSMTA2Conditions.Where(x => x.BiosafetyChecklistOfSMTA2Id == condition.Id).FirstOrDefault();

            WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto biosafetyChecklistOfSMTA2ConditionDto = new WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto();
            biosafetyChecklistOfSMTA2ConditionDto.Id = worklistFromBioHubItemBiosafetyChecklistOfSMTA2Condition != null ? worklistFromBioHubItemBiosafetyChecklistOfSMTA2Condition.Id : Guid.NewGuid();
            biosafetyChecklistOfSMTA2ConditionDto.BiosafetyChecklistId = condition.Id;
            biosafetyChecklistOfSMTA2ConditionDto.Order = condition.Order;
            biosafetyChecklistOfSMTA2ConditionDto.Condition = condition.Condition;
            biosafetyChecklistOfSMTA2ConditionDto.Mandatory = condition.Mandatory;
            biosafetyChecklistOfSMTA2ConditionDto.Selectable = condition.Selectable;
            biosafetyChecklistOfSMTA2ConditionDto.ParentConditionId = condition.ParentConditionId;
            biosafetyChecklistOfSMTA2ConditionDto.ShowOnParentValue = condition.ShowOnParentValue;
            biosafetyChecklistOfSMTA2ConditionDto.IsParentCondition = condition.IsParentCondition;
            SetFlag(biosafetyChecklistOfSMTA2ConditionDto, worklistFromBioHubItemBiosafetyChecklistOfSMTA2Condition, condition);


            conditions.Add(biosafetyChecklistOfSMTA2ConditionDto);

        }

        foreach (var condition in conditions)
        {
            if (condition.IsParentCondition == true || condition.ParentConditionId == null)
            {
                condition.IsVisible = true;
            }
            else
            {
                var parent = conditions.Where(x => x.BiosafetyChecklistId == condition.ParentConditionId).FirstOrDefault();
                if (parent == null)
                {
                    condition.IsVisible = false;
                }
                else
                {
                    condition.IsVisible = parent.Flag == condition.ShowOnParentValue;
                }
            }
        }

        conditions = conditions.OrderBy(x => x.Order).ToList();

        return conditions;
    }

    private void SetFlag(WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto biosafetyChecklistOfSMTA2ConditionDto, WorklistFromBioHubItemBiosafetyChecklistOfSMTA2? worklistFromBioHubItemBiosafetyChecklistOfSMTA2Condition, BiosafetyChecklistOfSMTA2 condition)
    {
        if (worklistFromBioHubItemBiosafetyChecklistOfSMTA2Condition != null)
        {
            if (worklistFromBioHubItemBiosafetyChecklistOfSMTA2Condition.Flag != null)
            {
                biosafetyChecklistOfSMTA2ConditionDto.Flag = worklistFromBioHubItemBiosafetyChecklistOfSMTA2Condition.Flag;
            }
            else
            {
                if (condition.IsParentCondition == true)
                {
                    biosafetyChecklistOfSMTA2ConditionDto.Flag = null;
                }
                else
                {
                    biosafetyChecklistOfSMTA2ConditionDto.Flag = false;
                }
            }
        }
        else
        {
            if (condition.FlagPresetValue != null)
            {
                biosafetyChecklistOfSMTA2ConditionDto.Flag = biosafetyChecklistOfSMTA2ConditionDto.Flag;
            }
            else
            {
                if (condition.IsParentCondition == true)
                {
                    biosafetyChecklistOfSMTA2ConditionDto.Flag = null;
                }
                else
                {
                    biosafetyChecklistOfSMTA2ConditionDto.Flag = false;
                }
            }
        }
    }



    private IEnumerable<WorklistFromBioHubItemMaterialDto> GetMaterials(WorklistFromBioHubItem entity)
    {
        List<WorklistFromBioHubItemMaterialDto> materials = new List<WorklistFromBioHubItemMaterialDto>();
        foreach (var worklistFromBioHubItemMaterial in entity.WorklistFromBioHubItemMaterials)
        {

            WorklistFromBioHubItemMaterialDto materialDto = new WorklistFromBioHubItemMaterialDto();
            materialDto.Id = worklistFromBioHubItemMaterial.Id;
            materialDto.Condition = worklistFromBioHubItemMaterial.Condition;
            materialDto.Quantity = worklistFromBioHubItemMaterial.Quantity;
            materialDto.MaterialProductId = worklistFromBioHubItemMaterial.Material.OriginalProductTypeId;
            materialDto.TransportCategoryId = worklistFromBioHubItemMaterial.Material.TransportCategoryId;
            materialDto.Amount = worklistFromBioHubItemMaterial.Amount;
            materialDto.AvailableQuantity = worklistFromBioHubItemMaterial.Material.CurrentNumberOfVials ?? 0;
            materialDto.MaterialNumber = worklistFromBioHubItemMaterial.Material.ReferenceNumber;
            materialDto.MaterialId = worklistFromBioHubItemMaterial.Material.Id;
            materialDto.MaterialName = worklistFromBioHubItemMaterial.Material.Name;
            materialDto.Gender = worklistFromBioHubItemMaterial.Material.Gender;
            //materialDto.PatientStatus = materialClinicalDetail.PatientStatus;
            materialDto.CollectionDate = worklistFromBioHubItemMaterial.Material.CollectionDate;
            materialDto.Age = worklistFromBioHubItemMaterial.Material.Age;
            materialDto.IsolationHostTypeId = worklistFromBioHubItemMaterial.Material.IsolationHostTypeId;
            materialDto.Location = worklistFromBioHubItemMaterial.Material.Location;
            materialDto.Note = worklistFromBioHubItemMaterial.Note;
            materialDto.Condition = worklistFromBioHubItemMaterial.Condition;
            materialDto.Status = worklistFromBioHubItemMaterial.Material.Status;

            materials.Add(materialDto);

        }
        return materials;
    }

    private IEnumerable<WorklistItemUserDto> GetLaboratoryFocalPoints(WorklistFromBioHubItem entity)
    {
        List<WorklistItemUserDto> laboratoryFocalPoints = new List<WorklistItemUserDto>();
        foreach (var laboratoryFocalPoint in entity.WorklistFromBioHubItemLaboratoryFocalPoints)
        {

            WorklistItemUserDto laboratoryFocalPointDto = new WorklistItemUserDto();
            laboratoryFocalPointDto.Id = laboratoryFocalPoint.Id;
            laboratoryFocalPointDto.UserId = laboratoryFocalPoint.UserId;
            laboratoryFocalPointDto.UserName = laboratoryFocalPoint.User.FirstName + " " + laboratoryFocalPoint.User.LastName;
            laboratoryFocalPointDto.Country = entity.RequestInitiationToLaboratory.Country.Name;
            laboratoryFocalPointDto.Email = laboratoryFocalPoint.User.Email;
            laboratoryFocalPointDto.LaboratoryId = entity.RequestInitiationToLaboratoryId;
            laboratoryFocalPointDto.JobTitle = laboratoryFocalPoint.User.JobTitle;
            laboratoryFocalPointDto.MobilePhone = laboratoryFocalPoint.User.MobilePhone;
            laboratoryFocalPointDto.BusinessPhone = laboratoryFocalPoint.User.BusinessPhone;
            laboratoryFocalPointDto.Laboratory = entity.RequestInitiationToLaboratory.Name;
            laboratoryFocalPointDto.Other = laboratoryFocalPoint.Other;
            laboratoryFocalPointDto.WorklistItemId = laboratoryFocalPoint.WorklistFromBioHubItemId;

            laboratoryFocalPoints.Add(laboratoryFocalPointDto);

        }
        return laboratoryFocalPoints;
    }

    private IEnumerable<BookingFormOfSMTADto> GetBookingForms(WorklistFromBioHubItem entity)
    {
        List<BookingFormOfSMTADto> bookingForms = new List<BookingFormOfSMTADto>();
        var orderedList = entity.BookingForms.OrderBy(x => x.TransportCategory.Description);
        foreach (var bookingForm in orderedList)
        {

            BookingFormOfSMTADto bookingFormDto = new BookingFormOfSMTADto();
            bookingFormDto.Id = bookingForm.Id;
            bookingFormDto.Date = bookingForm.Date; //bookingForm.Date == null ? DateTime.UtcNow : bookingForm.Date;
            bookingFormDto.RequestDateOfPickup = bookingForm.RequestDateOfPickup; //bookingForm.RequestDateOfPickup == null ? DateTime.UtcNow : bookingForm.RequestDateOfPickup;
            bookingFormDto.TotalNumberOfVials = bookingForm.TotalNumberOfVials;
            bookingFormDto.TotalAmount = bookingForm.TotalAmount;
            bookingFormDto.NumberOfInnerPackagingAndSize = bookingForm.NumberOfInnerPackagingAndSize;
            bookingFormDto.TemperatureTransportCondition = bookingForm.TemperatureTransportCondition;
            bookingFormDto.TransportCategoryId = bookingForm.TransportCategoryId;
            bookingFormDto.TransportCategoryName = bookingForm.TransportCategory.Name;
            bookingFormDto.TransportCategoryDescription = bookingForm.TransportCategory.Description;
            bookingFormDto.TransportModeId = bookingForm.TransportModeId;
            bookingFormDto.TransportModeName = bookingForm.TransportMode?.Name ?? string.Empty;
            bookingFormDto.TransportModeDescription = bookingForm.TransportMode?.Description ?? string.Empty;
            bookingFormDto.WorklistItemId = bookingForm.WorklistFromBioHubItemId;


            if (entity.Status == WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2 || entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval)
            {
                bookingFormDto.BookingFormPickupUsers = new List<WorklistItemUserDto>();
                foreach (var bookingFormPickupUser in bookingForm.BookingFormPickupUsers)
                {
                    WorklistItemUserDto bookingFormPickupUserDto = new WorklistItemUserDto();
                    bookingFormPickupUserDto.Id = bookingFormPickupUser.Id;
                    bookingFormPickupUserDto.UserId = bookingFormPickupUser.UserId;
                    bookingFormPickupUserDto.BookingFormId = bookingFormPickupUser.BookingFormId;
                    bookingFormPickupUserDto.Other = bookingFormPickupUser.Other;
                    bookingFormPickupUserDto.Country = entity.RequestInitiationFromBioHubFacility.Country.Name;
                    bookingFormPickupUserDto.BioHubFacility = entity.RequestInitiationFromBioHubFacility.Name;
                    bookingFormPickupUserDto.UserName = bookingFormPickupUser.User.FirstName + " " + bookingFormPickupUser.User.LastName;
                    bookingFormPickupUserDto.Email = bookingFormPickupUser.User.Email;
                    bookingFormPickupUserDto.JobTitle = bookingFormPickupUser.User.JobTitle;
                    bookingFormPickupUserDto.MobilePhone = bookingFormPickupUser.User.MobilePhone;
                    bookingFormPickupUserDto.BusinessPhone = bookingFormPickupUser.User.BusinessPhone;

                    bookingFormDto.BookingFormPickupUsers.Add(bookingFormPickupUserDto);
                }
            }
            if (entity.Status == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval)
            {
                bookingFormDto.EstimateDateOfPickup = bookingForm.EstimateDateOfPickup;
                bookingFormDto.BookingFormCourierUsers = new List<WorklistItemUserDto>();
                foreach (var bookingFormCourierUser in bookingForm.BookingFormCourierUsers)
                {
                    WorklistItemUserDto bookingFormCourierUserDto = new WorklistItemUserDto();
                    bookingFormCourierUserDto.Id = bookingFormCourierUser.Id;
                    bookingFormCourierUserDto.UserId = bookingFormCourierUser.UserId;
                    bookingFormCourierUserDto.BookingFormId = bookingFormCourierUser.BookingFormId;
                    bookingFormCourierUserDto.CourierId = bookingFormCourierUser.User.CourierId;
                    bookingFormCourierUserDto.Other = bookingFormCourierUser.Other;
                    bookingFormCourierUserDto.Country = bookingFormCourierUser.User.Courier.Country.Name;
                    bookingFormCourierUserDto.UserName = bookingFormCourierUser.User.FirstName + " " + bookingFormCourierUser.User.LastName;
                    bookingFormCourierUserDto.Email = bookingFormCourierUser.User.Email;
                    bookingFormCourierUserDto.JobTitle = bookingFormCourierUser.User.JobTitle;
                    bookingFormCourierUserDto.MobilePhone = bookingFormCourierUser.User.MobilePhone;
                    bookingFormCourierUserDto.BusinessPhone = bookingFormCourierUser.User.BusinessPhone;

                    bookingFormDto.BookingFormCourierUsers.Add(bookingFormCourierUserDto);
                }
            }

            if (entity.Status == WorklistFromBioHubStatus.WaitForPickUpCompleted || entity.Status == WorklistFromBioHubStatus.WaitForDeliveryCompleted)
            {
                bookingFormDto.DateOfPickup = bookingForm.DateOfPickup;
                bookingFormDto.DateOfDelivery = bookingForm.DateOfDelivery;
                bookingFormDto.ShipmentReferenceNumber = bookingForm.ShipmentReferenceNumber;
            }
            bookingForms.Add(bookingFormDto);

        }
        return bookingForms;
    }

    private IEnumerable<ShipmentDocumentDto> GetBHFShipmentDocuments(WorklistFromBioHubItem entity)
    {
        var shipmentDocuments = GetShipmentDocuments(entity);

        return shipmentDocuments.Where(x => x.UploaderRoleType == RoleType.BioHubFacility);
    }

    private IEnumerable<ShipmentDocumentDto> GetQEShipmentDocuments(WorklistFromBioHubItem entity)
    {
        var shipmentDocuments = GetShipmentDocuments(entity);

        return shipmentDocuments.Where(x => x.UploaderRoleType == RoleType.Laboratory);
    }

    private IEnumerable<ShipmentDocumentDto> GetShipmentDocuments(WorklistFromBioHubItem entity)
    {
        List<ShipmentDocumentDto> shipmentDocuments = new List<ShipmentDocumentDto>();
        List<DocumentFileType?> shipmentDocumentTypes = new List<DocumentFileType?>();
        shipmentDocumentTypes.Add(DocumentFileType.PackagingList);
        shipmentDocumentTypes.Add(DocumentFileType.NonCommercialInvoiceCatA);
        shipmentDocumentTypes.Add(DocumentFileType.NonCommercialInvoiceCatB);
        shipmentDocumentTypes.Add(DocumentFileType.DangerousGoodsDeclaration);
        shipmentDocumentTypes.Add(DocumentFileType.ExportPermit);
        shipmentDocumentTypes.Add(DocumentFileType.ImportPermit);
        shipmentDocumentTypes.Add(DocumentFileType.Other);

        var documents = entity.WorklistFromBioHubItemDocuments.Select(x => x.Document).Where(x => shipmentDocumentTypes.Contains(x.Type) && x.IsDocumentFile).ToList();

        if (documents != null && documents.Any())
        {
            foreach (var document in documents)
            {
                ShipmentDocumentDto shipmentDocument = new ShipmentDocumentDto();
                shipmentDocument.Id = document.Id;
                shipmentDocument.UploadedById = document.UploadedById;
                shipmentDocument.UploadedBy = document.UploadedBy.FirstName + " " + document.UploadedBy.LastName;
                shipmentDocument.Extension = document.Extension;
                shipmentDocument.Name = document.Name;
                shipmentDocument.UploadTime = document.CreationDate;
                shipmentDocument.FileType = document.Type;
                shipmentDocument.UploaderRoleType = document.UploadedBy.Role.RoleType;
                shipmentDocuments.Add(shipmentDocument);
            }
        }

        return shipmentDocuments;
    }

    private IEnumerable<FeedbackDto> GetFeedbacks(WorklistFromBioHubItem entity)
    {
        List<FeedbackDto> feedbacks = new List<FeedbackDto>();

        if (entity.WorklistFromBioHubItemFeedbacks != null && entity.WorklistFromBioHubItemFeedbacks.Any())
        {
            foreach (var feedback in entity.WorklistFromBioHubItemFeedbacks)
            {
                var newFeedback = new FeedbackDto();
                newFeedback.Date = feedback.Date;
                newFeedback.Text = feedback.Text;
                newFeedback.PostedBy = feedback.PostedBy.FirstName + " " + feedback.PostedBy.LastName;
                feedbacks.Add(newFeedback);
            }
        }
        feedbacks = feedbacks.OrderBy(x => x.Date).ToList();
        return feedbacks;
    }

    private IEnumerable<BiosafetyChecklistThreadCommentDto> GetBiosafetyChecklistComments(WorklistFromBioHubItem entity)
    {
        List<BiosafetyChecklistThreadCommentDto> comments = new List<BiosafetyChecklistThreadCommentDto>();

        if (entity.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments != null && entity.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments.Any())
        {
            foreach (var comment in entity.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments)
            {
                var newComment = new BiosafetyChecklistThreadCommentDto();
                newComment.Date = comment.Date;
                newComment.Text = comment.Text;
                newComment.PostedBy = comment.PostedBy != null ? comment.PostedBy.FirstName + " " + comment.PostedBy.LastName : string.Empty;
                comments.Add(newComment);
            }
        }
        comments = comments.OrderBy(x => x.Date).ToList();
        return comments;
    }

    private void SetShipmentDocuments(WorklistFromBioHubItem entity, IEnumerable<string> userPermissions, WorklistFromBioHubItemDto worklistfrombiohubitemDto)
    {
        if (
            (entity.IsPast != true && userPermissions.Contains(PermissionNames.CanReadBHFSMTA2ShipmentDocuments))
            || ((entity.IsPast == true && userPermissions.Contains(PermissionNames.CanReadBHFSMTA2ShipmentDocumentsPast)))
        )
        {
            worklistfrombiohubitemDto.BHFShipmentDocuments = GetBHFShipmentDocuments(entity);
        }


        if (
            (entity.IsPast != true && userPermissions.Contains(PermissionNames.CanReadQESMTA2ShipmentDocuments))
            || ((entity.IsPast == true && userPermissions.Contains(PermissionNames.CanReadQESMTA2ShipmentDocumentsPast)))
        )
        {
            worklistfrombiohubitemDto.QEShipmentDocuments = GetQEShipmentDocuments(entity);
        }
    }
}