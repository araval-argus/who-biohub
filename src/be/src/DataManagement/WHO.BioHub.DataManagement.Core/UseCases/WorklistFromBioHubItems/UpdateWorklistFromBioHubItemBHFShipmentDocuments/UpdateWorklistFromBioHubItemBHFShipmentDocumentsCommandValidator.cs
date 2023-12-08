using FluentValidation;
using Microsoft.AspNetCore.Http;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItemBHFShipmentDocuments;

public class UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommandValidator : AbstractValidator<UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommand>
{
    private readonly IDocumentReadRepository _documentReadRepository;
    public UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommandValidator(IDocumentReadRepository documentReadRepository)
    {
        _documentReadRepository = documentReadRepository;

        RuleFor(cmd => cmd.CurrentStatus)
            .NotNull()
            .WithMessage("Current Status is required");

     
         RuleFor((cmd) => cmd.ShipmentDocumentOperationType)
           .NotNull()
           .WithMessage("Missing ShipmentDocumentOperationType");


         When(cmd => cmd.ShipmentDocumentOperationType == ShipmentDocumentOperationType.Add, () =>
         {
            RuleFor(cmd => cmd.File)
           .NotNull()
           .WithMessage("File is required")
           .Must((f) => IsExtensionValid(f))
           .WithMessage("Invalid File format");
        });

        When(cmd => cmd.ShipmentDocumentOperationType == ShipmentDocumentOperationType.Update, () =>
        {
            RuleFor(cmd => cmd.ShipmentDocumentId)
           .NotNull()
           .WithMessage("Shipment DocumentId is required");

           RuleFor(cmd => cmd.ShipmentDocumentNewName)
           .NotNull()
           .WithMessage("Shipment Document New Name is required");
        });

        When(cmd => cmd.ShipmentDocumentOperationType == ShipmentDocumentOperationType.Delete, () =>
        {
            RuleFor(cmd => cmd.ShipmentDocumentId)
           .NotNull()
           .WithMessage("Shipment DocumentId is required");
        });    
      
    }

    protected bool IsExtensionValid(IFormFile file)
    {
        string extension = Path.GetExtension(file.FileName);

        if (string.IsNullOrEmpty(extension))
        {
            return false;
        }

        extension = extension.Replace(".", "").ToLower();

        switch (extension)
        {
            case "doc":
            case "docx":
            case "xls":
            case "xlsx":
            case "ppt":
            case "pptx":
            case "pdf":
            case "jpg":
            case "jpeg":
            case "mp4":
                return true;
            default:
                return false;

        }
    }

    protected bool IsSignatureExtensionValid(IFormFile file)
    {
        string extension = Path.GetExtension(file.FileName);

        if (string.IsNullOrEmpty(extension))
        {
            return false;
        }

        extension = extension.Replace(".", "").ToLower();

        switch (extension)
        {
            case "jpg":
            case "jpeg":
                return true;
            default:
                return false;

        }
    }

    protected bool IsLaboratoryFocalPointsValid(IEnumerable<WorklistItemUserDto> laboratoryFocalPoints)
    {
        if (laboratoryFocalPoints == null || !laboratoryFocalPoints.Any())
        {
            return false;
        }

        return true;
    }

    protected bool IsMaterialsNotEmpty(IEnumerable<WorklistFromBioHubItemMaterialDto> materials)
    {
        if (materials == null || !materials.Any())
        {
            return false;
        }
        return true;
    }

    protected bool IsMaterialsValid(IEnumerable<WorklistFromBioHubItemMaterialDto> materials)
    {
        if (materials != null && materials.Any())
        {

            foreach (var material in materials)
            {
                if (material.MaterialProductId == null)
                {
                    return false;
                }

                if (material.Quantity == null || material.Quantity <= 0 || material.Quantity > material.AvailableQuantity)
                {
                    return false;
                }

                if (material.Amount == null || material.Amount <= 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    protected bool IsAnnex2OfSMTA2ConditionsValid(IEnumerable<WorklistFromBioHubItemAnnex2OfSMTA2ConditionDto> annex2OfSMTA2Conditions)
    {

        foreach (var annex2OfSMTA2Condition in annex2OfSMTA2Conditions)
        {
            if (annex2OfSMTA2Condition.Mandatory == true && annex2OfSMTA2Condition.Flag != true)
            {
                return false;
            }

        }

        return true;
    }


    protected bool IsBiosafetyChecklistOfSMTA2ConditionsValid(IEnumerable<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto> bioHubItemBiosafetyChecklists)
    {

        foreach (var bioHubItemBiosafetyChecklist in bioHubItemBiosafetyChecklists)
        {
            if (bioHubItemBiosafetyChecklist.ParentConditionId == null)
            {
                if (bioHubItemBiosafetyChecklist.Mandatory == true && bioHubItemBiosafetyChecklist.Flag != true)
                {
                    return false;
                }
            }
            else
            {
                var parentCondition = bioHubItemBiosafetyChecklists.Where(x => x.BiosafetyChecklistId == bioHubItemBiosafetyChecklist.ParentConditionId).FirstOrDefault();

                if (parentCondition == null)
                {
                    return false;
                }

                if (bioHubItemBiosafetyChecklist.ShowOnParentValue == parentCondition.Flag && bioHubItemBiosafetyChecklist.Mandatory == true && bioHubItemBiosafetyChecklist.Flag != true)
                {
                    return false;
                }
            }
        }

        return true;
    }

    protected bool IsBookingFormsNotEmpty(IEnumerable<BookingFormOfSMTADto> bookingForms)
    {
        if (bookingForms == null || !bookingForms.Any())
        {
            return false;
        }
        return true;
    }

    protected bool IsBookingFormsValid(IEnumerable<BookingFormOfSMTADto> bookingForms)
    {

        foreach (var bookingForm in bookingForms)
        {
            if (bookingForm.TransportCategoryId == null)
            {
                return false;
            }

            if (bookingForm.Date == null)
            {
                return false;
            }

            if (bookingForm.RequestDateOfPickup == null)
            {
                return false;
            }

            if (bookingForm.TotalAmount <= 0)
            {
                return false;
            }

            if (bookingForm.TotalNumberOfVials <= 0)
            {
                return false;
            }

            if (bookingForm.TemperatureTransportCondition == null)
            {
                return false;
            }

            if (bookingForm.BookingFormPickupUsers == null || !bookingForm.BookingFormPickupUsers.Any())
            {
                return false;
            }

        }

        return true;
    }

    protected bool IsBookingFormCouriersValid(IEnumerable<BookingFormOfSMTADto> bookingForms)
    {


        foreach (var bookingForm in bookingForms)
        {
            if (bookingForm.CourierId == null)
            {
                return false;
            }

            if (bookingForm.BookingFormCourierUsers == null || !bookingForm.BookingFormCourierUsers.Any())
            {
                return false;
            }

            if (bookingForm.EstimateDateOfPickup == null)
            {
                return false;
            }

        }

        return true;
    }

    protected bool ShipmentDocumentsValid(List<ShipmentDocumentDto> shipmentDocuments)
    {
        if (shipmentDocuments == null || !shipmentDocuments.Any())
        {
            return false;
        }

        //if (!shipmentDocuments.Any(x => x.FileType == DocumentFileType.PackagingList))
        //{
        //    return false;
        //}

        //if (!shipmentDocuments.Any(x => x.FileType == DocumentFileType.NonCommercialInvoiceCatA))
        //{
        //    return false;
        //}

        //if (!shipmentDocuments.Any(x => x.FileType == DocumentFileType.NonCommercialInvoiceCatB))
        //{
        //    return false;
        //}

        //if (!shipmentDocuments.Any(x => x.FileType == DocumentFileType.DangerousGoodsDeclaration))
        //{
        //    return false;
        //}

        //if (!shipmentDocuments.Any(x => x.FileType == DocumentFileType.ExportPermit))
        //{
        //    return false;
        //}

        //if (!shipmentDocuments.Any(x => x.FileType == DocumentFileType.ImportPermit))
        //{
        //    return false;
        //}
        return true;
    }

    protected bool IsBookingFormPickupCompletedValid(IEnumerable<BookingFormOfSMTADto> bookingForms)
    {

        foreach (var bookingForm in bookingForms)
        {
            if (string.IsNullOrEmpty(bookingForm.ShipmentReferenceNumber))
            {
                return false;
            }

            if (bookingForm.DateOfPickup == null)
            {
                return false;
            }

        }

        return true;
    }

    protected bool IsBookingFormDeliveryCompletedValid(IEnumerable<BookingFormOfSMTADto> bookingForms)
    {

        foreach (var bookingForm in bookingForms)
        {
            if (string.IsNullOrEmpty(bookingForm.ShipmentReferenceNumber))
            {
                return false;
            }

            if (bookingForm.DateOfDelivery == null)
            {
                return false;
            }

        }

        return true;
    }


    protected bool IsShipmentMaterialsValid(IEnumerable<WorklistFromBioHubItemMaterialDto> materials)
    {

        foreach (var material in materials)
        {

            if (material.Condition == null)
            {
                return false;
            }
        }

        return true;
    }


    protected async Task<bool> IsSMTA2DocumentSigned(UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommand cmd, CancellationToken cancellationToken)
    {
        var laboratoryId = cmd.LaboratoryId.GetValueOrDefault();
        DocumentFileType documentType = DocumentFileType.SMTA2;
        var res = await _documentReadRepository.IsDocumentSignedByLaboratoryId(laboratoryId, documentType, cancellationToken);
        return res;
    }



}