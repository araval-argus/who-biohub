using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ReadWorklistToBioHubItem;

public interface IReadWorklistToBioHubItemMapper
{
    WorklistToBioHubItemDto Map(WorklistToBioHubItem entity, IEnumerable<string> userPermissions);
    WorklistToBioHubItemDto MapMinimal(WorklistToBioHubItem entity, IEnumerable<string> userPermissions);
}

public class ReadWorklistToBioHubItemMapper : IReadWorklistToBioHubItemMapper
{

    public WorklistToBioHubItemDto MapMinimal(WorklistToBioHubItem entity, IEnumerable<string> userPermissions)
    {

        WorklistToBioHubItemDto worklisttobiohubitemDto = CreateDto(entity);

        SetAnnex2OfSMTA1DocumentInfo(worklisttobiohubitemDto, entity, userPermissions);
        SetBookingFormOfSMTA1DocumentInfo(worklisttobiohubitemDto, entity, userPermissions);

        switch (entity.Status)
        {

            case WorklistToBioHubStatus.WaitForPickUpCompleted:
            case WorklistToBioHubStatus.WaitForDeliveryCompleted:
            case WorklistToBioHubStatus.WaitForArrivalConditionCheck:
            case WorklistToBioHubStatus.WaitForCommentBHFSendFeedback:
            case WorklistToBioHubStatus.WaitForFinalApproval:
            case WorklistToBioHubStatus.ShipmentCompleted:
                SetShipmentDocuments(entity, userPermissions, worklisttobiohubitemDto);
                break;
        }

        return worklisttobiohubitemDto;
    }

    public WorklistToBioHubItemDto Map(WorklistToBioHubItem entity, IEnumerable<string> userPermissions)
    {

        WorklistToBioHubItemDto worklisttobiohubitemDto = CreateDto(entity);

        switch (entity.Status)
        {

            case WorklistToBioHubStatus.SubmitAnnex2OfSMTA1:
            case WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval:

                SetAnnex2OfSMTA1DocumentInfo(worklisttobiohubitemDto, entity, userPermissions);

                //#54317
                //worklisttobiohubitemDto.Annex2OfSMTA1SignatureId = entity.WorklistToBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA1).FirstOrDefault(x => x.IsDocumentFile == false)?.Id;
                //worklisttobiohubitemDto.Annex2OfSMTA1SignatureString = entity.WorklistToBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA1).FirstOrDefault(x => x.IsDocumentFile == false)?.Base64String;
                worklisttobiohubitemDto.Annex2OfSMTA1SignatureText = entity.Annex2OfSMTA1SignatureText;
                ////////////

                worklisttobiohubitemDto.Annex2FillingOption = entity.Annex2FillingOption ?? FillingOption.ElectronicallyFill;
                worklisttobiohubitemDto.Annex2TermsAndConditions = entity.Annex2TermsAndConditions ?? true;
                worklisttobiohubitemDto.Annex2Comment = entity.Annex2Comment;
                worklisttobiohubitemDto.MaterialShippingInformations = GetMaterialShippingInformations(entity);
                worklisttobiohubitemDto.LaboratoryFocalPoints = GetLaboratoryFocalPoints(entity);
                worklisttobiohubitemDto.WHODocumentRegistrationNumber = entity.WHODocumentRegistrationNumber;
                worklisttobiohubitemDto.BioHubFacilityAddress = entity.RequestInitiationToBioHubFacility != null ? entity.RequestInitiationToBioHubFacility.Address : string.Empty;
                worklisttobiohubitemDto.BioHubFacilityCountry = entity.RequestInitiationToBioHubFacility != null ? entity.RequestInitiationToBioHubFacility.Country.Name : string.Empty;
                worklisttobiohubitemDto.LaboratoryCountry = entity.RequestInitiationFromLaboratory != null ? entity.RequestInitiationFromLaboratory.Country.Name : string.Empty;
                worklisttobiohubitemDto.LastOperationUserFirstName = entity.Status == WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval ? entity.LastOperationUser.FirstName : string.Empty;
                worklisttobiohubitemDto.LastOperationUserLastName = entity.Status == WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval ? entity.LastOperationUser.LastName : string.Empty;
                worklisttobiohubitemDto.LastOperationUserJobTitle = entity.Status == WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval ? entity.LastOperationUser.JobTitle : string.Empty;

                break;

            case WorklistToBioHubStatus.SubmitBookingFormOfSMTA1:
            case WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval:

                SetBookingFormOfSMTA1DocumentInfo(worklisttobiohubitemDto, entity, userPermissions);

                //#54317
                //worklisttobiohubitemDto.BookingFormOfSMTA1SignatureId = entity.WorklistToBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA1).FirstOrDefault(x => x.IsDocumentFile == false)?.Id;
                //worklisttobiohubitemDto.BookingFormOfSMTA1SignatureString = entity.WorklistToBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA1).FirstOrDefault(x => x.IsDocumentFile == false)?.Base64String;
                worklisttobiohubitemDto.BookingFormOfSMTA1SignatureText = entity.BookingFormOfSMTA1SignatureText;
                ////////////
                ///

                worklisttobiohubitemDto.BookingFormFillingOption = entity.BookingFormFillingOption ?? FillingOption.ElectronicallyFill;
                worklisttobiohubitemDto.BookingForms = GetBookingForms(entity);
                worklisttobiohubitemDto.MaterialShippingInformations = GetMaterialShippingInformations(entity);

                worklisttobiohubitemDto.LastOperationUserFirstName = entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval ? entity.LastOperationUser.FirstName : string.Empty;
                worklisttobiohubitemDto.LastOperationUserLastName = entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval ? entity.LastOperationUser.LastName : string.Empty;
                worklisttobiohubitemDto.LastOperationUserEmail = entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval ? entity.LastOperationUser.Email : string.Empty;
                worklisttobiohubitemDto.LastOperationUserJobTitle = entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval ? entity.LastOperationUser.JobTitle : string.Empty;
                worklisttobiohubitemDto.LastOperationUserBusinessPhone = entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval ? entity.LastOperationUser.BusinessPhone : string.Empty;
                worklisttobiohubitemDto.LastOperationUserMobilePhone = entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval ? entity.LastOperationUser.MobilePhone : string.Empty;
                worklisttobiohubitemDto.BioHubFacilityAddress = entity.RequestInitiationToBioHubFacility.Address;
                worklisttobiohubitemDto.BioHubFacilityCountry = entity.RequestInitiationToBioHubFacility.Country.Name;
                worklisttobiohubitemDto.LaboratoryAddress = entity.RequestInitiationFromLaboratory.Address;
                worklisttobiohubitemDto.LaboratoryCountry = entity.RequestInitiationFromLaboratory.Country.Name;
                worklisttobiohubitemDto.WorklistToBioHubItemBioHubFacilityFocalPoints = GetBioHubFacilityFocalPoints(entity);
                break;

            //case WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments:
            //    worklisttobiohubitemDto.ShipmentDocuments = GetShipmentDocuments(entity);
            //    break;
            case WorklistToBioHubStatus.WaitForPickUpCompleted:
            case WorklistToBioHubStatus.WaitForDeliveryCompleted:
                SetShipmentDocuments(entity, userPermissions, worklisttobiohubitemDto);
                worklisttobiohubitemDto.BookingForms = GetBookingForms(entity);
                break;

            case WorklistToBioHubStatus.WaitForArrivalConditionCheck:
                SetShipmentDocuments(entity, userPermissions, worklisttobiohubitemDto);
                worklisttobiohubitemDto.MaterialShippingInformations = GetMaterialShippingInformations(entity);
                worklisttobiohubitemDto.Feedbacks = GetFeedbacks(entity);
                break;
            case WorklistToBioHubStatus.WaitForCommentBHFSendFeedback:
                SetShipmentDocuments(entity, userPermissions, worklisttobiohubitemDto);
                worklisttobiohubitemDto.MaterialShippingInformations = GetMaterialShippingInformations(entity);
                worklisttobiohubitemDto.Feedbacks = GetFeedbacks(entity);
                break;

            case WorklistToBioHubStatus.WaitForFinalApproval:
                SetShipmentDocuments(entity, userPermissions, worklisttobiohubitemDto);
                worklisttobiohubitemDto.MaterialShippingInformations = GetMaterialShippingInformations(entity);
                worklisttobiohubitemDto.Feedbacks = GetFeedbacks(entity);
                break;

            case WorklistToBioHubStatus.ShipmentCompleted:
                SetShipmentDocuments(entity, userPermissions, worklisttobiohubitemDto);
                worklisttobiohubitemDto.MaterialShippingInformations = GetMaterialShippingInformations(entity, true);
                worklisttobiohubitemDto.WorklistToBioHubItemMaterials = GetMaterials(entity);
                break;
        }


        return worklisttobiohubitemDto;
    }

    private WorklistToBioHubItemDto CreateDto(WorklistToBioHubItem entity)
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
            BioHubFacilityId = entity.RequestInitiationToBioHubFacilityId,
            LaboratoryId = entity.RequestInitiationFromLaboratoryId,
            HistoryDto = false,
            UserRoleName = entity.LastOperationUser.Role.Name,
            UserRoleTypeName = entity.LastOperationUser.Role.RoleType.ToString(),
            UserRoleType = entity.LastOperationUser.Role.RoleType,
            LaboratoryName = entity.RequestInitiationFromLaboratory.Name,
            BioHubFacilityName = entity.RequestInitiationToBioHubFacility != null ? entity.RequestInitiationToBioHubFacility.Name : string.Empty,
            IsPast = entity.IsPast,
        };
    }

    private string GetDocumentName(WorklistToBioHubItem entity, DocumentFileType type)
    {
        string name = entity.WorklistToBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile)?.Name;
        string extension = entity.WorklistToBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == type).FirstOrDefault(x => x.IsDocumentFile)?.Extension;

        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(extension))
        {
            return name + "." + extension.ToLower();
        }
        return string.Empty;
    }




    private void SetAnnex2OfSMTA1DocumentInfo(WorklistToBioHubItemDto worklisttobiohubitemDto, WorklistToBioHubItem entity, IEnumerable<string> userPermissions)
    {
        switch (entity.Status)
        {
            case WorklistToBioHubStatus.SubmitAnnex2OfSMTA1:
            case WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval:
                var requiredPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(entity.Status, PermissionType.DownloadFile, entity.IsPast);

                if (userPermissions.Contains(requiredPermission))
                {
                    worklisttobiohubitemDto.Annex2OfSMTA1DocumentId = entity.WorklistToBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA1).FirstOrDefault(x => x.IsDocumentFile != false)?.Id;
                    worklisttobiohubitemDto.OriginalDocumentTemplateAnnex2OfSMTA1DocumentId = entity.WorklistToBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.Annex2OfSMTA1).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;
                    worklisttobiohubitemDto.Annex2OfSMTA1DocumentName = GetDocumentName(entity, DocumentFileType.Annex2OfSMTA1);
                }
                break;

            default:
                break;
        }
    }


    private void SetBookingFormOfSMTA1DocumentInfo(WorklistToBioHubItemDto worklisttobiohubitemDto, WorklistToBioHubItem entity, IEnumerable<string> userPermissions)
    {
        switch (entity.Status)
        {
            case WorklistToBioHubStatus.SubmitBookingFormOfSMTA1:
            case WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval:

                var requiredPermission = StatusPermissionMapper.GetWorklistToBioHubStatusPermission(entity.Status, PermissionType.DownloadFile, entity.IsPast);

                if (userPermissions.Contains(requiredPermission))
                {
                    worklisttobiohubitemDto.BookingFormOfSMTA1DocumentId = entity.WorklistToBioHubItemDocuments.Where(x => x.Type == DocumentFileType.BookingFormOfSMTA1).FirstOrDefault(x => x.IsDocumentFile != false)?.DocumentId;
                    worklisttobiohubitemDto.BookingFormOfSMTA1DocumentName = GetDocumentName(entity, DocumentFileType.BookingFormOfSMTA1);
                    worklisttobiohubitemDto.OriginalDocumentTemplateBookingFormOfSMTA1DocumentId = entity.WorklistToBioHubItemDocuments.Select(x => x.Document).Where(x => x.Type == DocumentFileType.BookingFormOfSMTA1).FirstOrDefault(x => x.IsDocumentFile != false)?.OriginalDocumentTemplateId;

                }
                break;
            default:
                break;
        }
    }


    private IEnumerable<MaterialShippingInformationDto> GetMaterialShippingInformations(WorklistToBioHubItem entity, bool onlyDamaged = false)
    {
        List<MaterialShippingInformationDto> materialShippingInformations = new List<MaterialShippingInformationDto>();
        foreach (var materialShippingInformation in entity.MaterialShippingInformations)
        {

            MaterialShippingInformationDto materialShippingInformationDto = new MaterialShippingInformationDto();
            materialShippingInformationDto.Id = materialShippingInformation.Id;
            materialShippingInformationDto.MaterialNumber = materialShippingInformation.MaterialNumber;
            materialShippingInformationDto.AdditionalInformation = materialShippingInformation.AdditionalInformation;
            materialShippingInformationDto.Condition = materialShippingInformation.Condition;
            materialShippingInformationDto.Quantity = materialShippingInformation.Quantity;
            materialShippingInformationDto.MaterialProductId = materialShippingInformation.MaterialProductId;
            materialShippingInformationDto.TransportCategoryId = materialShippingInformation.TransportCategoryId;
            materialShippingInformationDto.Amount = materialShippingInformation.Amount;
            materialShippingInformationDto.MaterialClinicalDetails = new List<MaterialClinicalDetailDto>();
            if (materialShippingInformation.MaterialClinicalDetails != null)
            {
                var materialClinicalDetails = materialShippingInformation.MaterialClinicalDetails;

                if (onlyDamaged)
                {
                    materialClinicalDetails = materialClinicalDetails.Where(x => x.Condition == ShipmentMaterialCondition.Damaged).ToList();
                }

                foreach (var materialClinicalDetail in materialClinicalDetails)
                {
                    MaterialClinicalDetailDto materialClinicalDetailDto = new MaterialClinicalDetailDto();
                    materialClinicalDetailDto.Id = materialClinicalDetail.Id;
                    materialClinicalDetailDto.MaterialShippingInformationId = materialShippingInformationDto.Id;
                    materialClinicalDetailDto.MaterialNumber = materialClinicalDetail.MaterialNumber;
                    materialClinicalDetailDto.MaterialProductId = materialShippingInformation.MaterialProductId;
                    materialClinicalDetailDto.TransportCategoryId = materialShippingInformation.TransportCategoryId;
                    materialClinicalDetailDto.Gender = materialClinicalDetail.Gender;
                    materialClinicalDetailDto.PatientStatus = materialClinicalDetail.PatientStatus;
                    materialClinicalDetailDto.CollectionDate = materialClinicalDetail.CollectionDate;
                    materialClinicalDetailDto.Age = materialClinicalDetail.Age;
                    materialClinicalDetailDto.IsolationHostTypeId = materialClinicalDetail.IsolationHostTypeId;
                    materialClinicalDetailDto.Location = materialClinicalDetail.Location;
                    materialClinicalDetailDto.Note = materialClinicalDetail.Note;
                    materialClinicalDetailDto.Condition = materialClinicalDetail.Condition;
                    materialShippingInformationDto.MaterialClinicalDetails.Add(materialClinicalDetailDto);
                }
            }
            materialShippingInformationDto.MaterialLaboratoryAnalysisInformation = new List<MaterialLaboratoryAnalysisInformationDto>();
            if (materialShippingInformation.MaterialLaboratoryAnalysisInformation != null)
            {
                var materialLaboratoryAnalysisInformationElements = materialShippingInformation.MaterialLaboratoryAnalysisInformation;

                foreach (var materialLaboratoryAnalysisInformationElement in materialLaboratoryAnalysisInformationElements)
                {
                    MaterialLaboratoryAnalysisInformationDto materialLaboratoryAnalysisInformationDto = new MaterialLaboratoryAnalysisInformationDto();
                    materialLaboratoryAnalysisInformationDto.Id = materialLaboratoryAnalysisInformationElement.Id;
                    materialLaboratoryAnalysisInformationDto.MaterialShippingInformationId = materialShippingInformationDto.Id;
                    materialLaboratoryAnalysisInformationDto.MaterialNumber = materialLaboratoryAnalysisInformationElement.MaterialNumber;
                    materialLaboratoryAnalysisInformationDto.UnitOfMeasureId = materialLaboratoryAnalysisInformationElement.UnitOfMeasureId;
                    materialLaboratoryAnalysisInformationDto.Temperature = materialLaboratoryAnalysisInformationElement.Temperature;
                    materialLaboratoryAnalysisInformationDto.VirusConcentration = materialLaboratoryAnalysisInformationElement.VirusConcentration;
                    materialLaboratoryAnalysisInformationDto.MaterialNumber = materialLaboratoryAnalysisInformationElement.MaterialNumber;
                    materialLaboratoryAnalysisInformationDto.CulturingPassagesNumber = materialLaboratoryAnalysisInformationElement.CulturingPassagesNumber;
                    materialLaboratoryAnalysisInformationDto.BrandOfTransportMedium = materialLaboratoryAnalysisInformationElement.BrandOfTransportMedium;
                    materialLaboratoryAnalysisInformationDto.TypeOfTransportMedium = materialLaboratoryAnalysisInformationElement.TypeOfTransportMedium;
                    materialLaboratoryAnalysisInformationDto.CulturingCellLine = materialLaboratoryAnalysisInformationElement.CulturingCellLine;
                    materialLaboratoryAnalysisInformationDto.FreezingDate = materialLaboratoryAnalysisInformationElement.FreezingDate;
                    materialLaboratoryAnalysisInformationDto.GSDUploadedToDatabase = materialLaboratoryAnalysisInformationElement.GSDUploadedToDatabase;
                    materialLaboratoryAnalysisInformationDto.AccessionNumberInGSDDatabase = materialLaboratoryAnalysisInformationElement.AccessionNumberInGSDDatabase;
                    materialLaboratoryAnalysisInformationDto.DatabaseUsedForGSDUploadingId = materialLaboratoryAnalysisInformationElement.DatabaseUsedForGSDUploadingId;
                    materialLaboratoryAnalysisInformationDto.CollectedSpecimenTypes = new List<Guid?>();
                    foreach (var elem in materialLaboratoryAnalysisInformationElement.CollectedSpecimenTypes)
                    {
                        materialLaboratoryAnalysisInformationDto.CollectedSpecimenTypes.Add(elem.SpecimenTypeId);
                    }

                    materialShippingInformationDto.MaterialLaboratoryAnalysisInformation.Add(materialLaboratoryAnalysisInformationDto);
                }
            }


            materialShippingInformations.Add(materialShippingInformationDto);

        }
        return materialShippingInformations;
    }

    private IEnumerable<WorklistItemUserDto> GetLaboratoryFocalPoints(WorklistToBioHubItem entity)
    {
        List<WorklistItemUserDto> laboratoryFocalPoints = new List<WorklistItemUserDto>();
        foreach (var laboratoryFocalPoint in entity.WorklistToBioHubItemLaboratoryFocalPoints)
        {

            WorklistItemUserDto laboratoryFocalPointDto = new WorklistItemUserDto();
            laboratoryFocalPointDto.Id = laboratoryFocalPoint.Id;
            laboratoryFocalPointDto.UserId = laboratoryFocalPoint.UserId;
            laboratoryFocalPointDto.UserName = laboratoryFocalPoint.User.FirstName + " " + laboratoryFocalPoint.User.LastName;
            laboratoryFocalPointDto.Country = entity.RequestInitiationFromLaboratory.Country.Name;
            laboratoryFocalPointDto.Email = laboratoryFocalPoint.User.Email;
            laboratoryFocalPointDto.LaboratoryId = entity.RequestInitiationFromLaboratoryId;
            laboratoryFocalPointDto.JobTitle = laboratoryFocalPoint.User.JobTitle;
            laboratoryFocalPointDto.MobilePhone = laboratoryFocalPoint.User.MobilePhone;
            laboratoryFocalPointDto.BusinessPhone = laboratoryFocalPoint.User.BusinessPhone;
            laboratoryFocalPointDto.Laboratory = entity.RequestInitiationFromLaboratory.Name;
            laboratoryFocalPointDto.Other = laboratoryFocalPoint.Other;
            laboratoryFocalPointDto.WorklistItemId = laboratoryFocalPoint.WorklistToBioHubItemId;

            laboratoryFocalPoints.Add(laboratoryFocalPointDto);

        }
        return laboratoryFocalPoints;
    }

    private IEnumerable<WorklistItemUserDto> GetBioHubFacilityFocalPoints(WorklistToBioHubItem entity)
    {
        List<WorklistItemUserDto> bioHubFacilityFocalPointsDto = new List<WorklistItemUserDto>();
        List<User>? bioHubFacilityFocalPoints;
        if (entity.IsPast == true)
        {
            bioHubFacilityFocalPoints = entity.RequestInitiationToBioHubFacility.Users
                .Where(x => x.DeletedOn == null)
                .Where(x => x.OperationalFocalPoint == true)
                .Where(x => x.Role.OnBehalfOf == true)
                .ToList();
        }

        else
        {
            bioHubFacilityFocalPoints = entity.RequestInitiationToBioHubFacility.Users
               .Where(x => x.DeletedOn == null)
               .Where(x => x.OperationalFocalPoint == true)
               .Where(x => x.Role.OnBehalfOf != true)
               .ToList();
        }

        foreach (var bioHubFacilityFocalPoint in bioHubFacilityFocalPoints)
        {

            WorklistItemUserDto bioHubFacilityFocalPointDto = new WorklistItemUserDto();
            bioHubFacilityFocalPointDto.Id = Guid.NewGuid();
            bioHubFacilityFocalPointDto.UserId = bioHubFacilityFocalPoint.Id;
            bioHubFacilityFocalPointDto.UserName = bioHubFacilityFocalPoint.FirstName + " " + bioHubFacilityFocalPoint.LastName;
            bioHubFacilityFocalPointDto.BioHubFacilityId = entity.RequestInitiationToBioHubFacilityId;
            bioHubFacilityFocalPointDto.JobTitle = bioHubFacilityFocalPoint.JobTitle;
            bioHubFacilityFocalPointDto.MobilePhone = bioHubFacilityFocalPoint.MobilePhone;
            bioHubFacilityFocalPointDto.BusinessPhone = bioHubFacilityFocalPoint.BusinessPhone;
            bioHubFacilityFocalPointDto.WorklistItemId = entity.Id;

            bioHubFacilityFocalPointsDto.Add(bioHubFacilityFocalPointDto);

        }
        return bioHubFacilityFocalPointsDto;
    }

    private IEnumerable<BookingFormOfSMTADto> GetBookingForms(WorklistToBioHubItem entity)
    {
        List<BookingFormOfSMTADto> bookingForms = new List<BookingFormOfSMTADto>();
        var orderedList = entity.BookingForms.OrderBy(x => x.TransportCategory.Description);
        foreach (var bookingForm in orderedList)
        {

            BookingFormOfSMTADto bookingFormDto = new BookingFormOfSMTADto();
            bookingFormDto.Id = bookingForm.Id;
            bookingFormDto.Date = bookingForm.Date; //bookingForm.Date == null ? DateTime.UtcNow : bookingForm.Date;
            bookingFormDto.RequestDateOfPickup = bookingForm.RequestDateOfPickup; //bookingForm.RequestDateOfPickup == null ? DateTime.UtcNow : bookingForm.RequestDateOfPickup;
            bookingFormDto.TotalAmount = bookingForm.TotalAmount;
            bookingFormDto.TotalNumberOfVials = bookingForm.TotalNumberOfVials;
            bookingFormDto.NumberOfInnerPackagingAndSize = bookingForm.NumberOfInnerPackagingAndSize;
            bookingFormDto.TemperatureTransportCondition = bookingForm.TemperatureTransportCondition;
            bookingFormDto.TransportCategoryId = bookingForm.TransportCategoryId;
            bookingFormDto.TransportCategoryName = bookingForm.TransportCategory.Name;
            bookingFormDto.TransportCategoryDescription = bookingForm.TransportCategory.Description;
            bookingFormDto.TransportModeId = bookingForm.TransportModeId;
            bookingFormDto.TransportModeName = bookingForm.TransportMode?.Name ?? string.Empty;
            bookingFormDto.TransportModeDescription = bookingForm.TransportMode?.Description ?? string.Empty;
            bookingFormDto.WorklistItemId = bookingForm.WorklistToBioHubItemId;
            bookingFormDto.CourierId = bookingForm.CourierId;


            if (entity.Status == WorklistToBioHubStatus.SubmitBookingFormOfSMTA1 || entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval)
            {
                bookingFormDto.BookingFormPickupUsers = new List<WorklistItemUserDto>();
                foreach (var bookingFormPickupUser in bookingForm.BookingFormPickupUsers)
                {
                    WorklistItemUserDto bookingFormPickupUserDto = new WorklistItemUserDto();
                    bookingFormPickupUserDto.Id = bookingFormPickupUser.Id;
                    bookingFormPickupUserDto.UserId = bookingFormPickupUser.UserId;
                    bookingFormPickupUserDto.BookingFormId = bookingFormPickupUser.BookingFormId;
                    bookingFormPickupUserDto.Other = bookingFormPickupUser.Other;
                    bookingFormPickupUserDto.Country = bookingFormPickupUser.User.Laboratory.Country.Name;
                    bookingFormPickupUserDto.Laboratory = bookingFormPickupUser.User.Laboratory.Name;
                    bookingFormPickupUserDto.UserName = bookingFormPickupUser.User.FirstName + " " + bookingFormPickupUser.User.LastName;
                    bookingFormPickupUserDto.Email = bookingFormPickupUser.User.Email;
                    bookingFormPickupUserDto.JobTitle = bookingFormPickupUser.User.JobTitle;
                    bookingFormPickupUserDto.MobilePhone = bookingFormPickupUser.User.MobilePhone;
                    bookingFormPickupUserDto.BusinessPhone = bookingFormPickupUser.User.BusinessPhone;

                    bookingFormDto.BookingFormPickupUsers.Add(bookingFormPickupUserDto);
                }
            }
            if (entity.Status == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval)
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

            if (entity.Status == WorklistToBioHubStatus.WaitForPickUpCompleted || entity.Status == WorklistToBioHubStatus.WaitForDeliveryCompleted)
            {
                bookingFormDto.DateOfPickup = bookingForm.DateOfPickup;
                bookingFormDto.DateOfDelivery = bookingForm.DateOfDelivery;
                bookingFormDto.ShipmentReferenceNumber = bookingForm.ShipmentReferenceNumber;
            }
            bookingForms.Add(bookingFormDto);

        }
        return bookingForms;
    }

    private IEnumerable<ShipmentDocumentDto> GetShipmentDocuments(WorklistToBioHubItem entity)
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

        var documents = entity.WorklistToBioHubItemDocuments.Select(x => x.Document).Where(x => shipmentDocumentTypes.Contains(x.Type) && x.IsDocumentFile).ToList();

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
                shipmentDocuments.Add(shipmentDocument);
            }
        }

        return shipmentDocuments;
    }

    private IEnumerable<FeedbackDto> GetFeedbacks(WorklistToBioHubItem entity)
    {
        List<FeedbackDto> feedbacks = new List<FeedbackDto>();

        if (entity.WorklistToBioHubItemFeedbacks != null && entity.WorklistToBioHubItemFeedbacks.Any())
        {
            foreach (var feedback in entity.WorklistToBioHubItemFeedbacks)
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

    private IEnumerable<WorklistToBioHubItemMaterialDto> GetMaterials(WorklistToBioHubItem entity)
    {
        List<WorklistToBioHubItemMaterialDto> materials = new List<WorklistToBioHubItemMaterialDto>();
        foreach (var worklistToBioHubItemMaterial in entity.WorklistToBioHubItemMaterials)
        {
            WorklistToBioHubItemMaterialDto materialDto = new WorklistToBioHubItemMaterialDto();
            materialDto.Id = worklistToBioHubItemMaterial.Id;
            materialDto.MaterialProductId = worklistToBioHubItemMaterial.Material.OriginalProductTypeId;
            materialDto.TransportCategoryId = worklistToBioHubItemMaterial.Material.TransportCategoryId;
            materialDto.MaterialNumber = worklistToBioHubItemMaterial.Material.ReferenceNumber;
            materialDto.MaterialId = worklistToBioHubItemMaterial.Material.Id;
            materialDto.MaterialName = worklistToBioHubItemMaterial.Material.Name;
            materialDto.Age = worklistToBioHubItemMaterial.Material.Age;
            materialDto.IsolationHostTypeId = worklistToBioHubItemMaterial.Material.IsolationHostTypeId;
            materialDto.CollectionDate = worklistToBioHubItemMaterial.Material.CollectionDate;
            materialDto.Location = worklistToBioHubItemMaterial.Material.Location;
            materialDto.Gender = worklistToBioHubItemMaterial.Material.Gender;
            materialDto.Status = worklistToBioHubItemMaterial.Material.Status;
            materials.Add(materialDto);

        }
        return materials;
    }
    private void SetShipmentDocuments(WorklistToBioHubItem entity, IEnumerable<string> userPermissions, WorklistToBioHubItemDto worklisttobiohubitemDto)
    {
        if (
            (entity.IsPast != true && userPermissions.Contains(PermissionNames.CanReadSMTA1ShipmentDocuments))
            || ((entity.IsPast == true && userPermissions.Contains(PermissionNames.CanReadSMTA1ShipmentDocumentsPast)))
        )
        {
            worklisttobiohubitemDto.ShipmentDocuments = GetShipmentDocuments(entity);
        }
    }
}