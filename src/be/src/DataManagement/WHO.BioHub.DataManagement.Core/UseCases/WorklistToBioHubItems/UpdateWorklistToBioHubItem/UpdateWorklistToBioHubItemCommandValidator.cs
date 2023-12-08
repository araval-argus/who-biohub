using FluentValidation;
using Microsoft.AspNetCore.Http;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.SeedDataConstants;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItem;

public class UpdateWorklistToBioHubItemCommandValidator : AbstractValidator<UpdateWorklistToBioHubItemCommand>
{
    private readonly IDocumentReadRepository _documentReadRepository;
    private readonly IWorklistToBioHubItemReadRepository _worklistToBioHubItemReadRepository;
    public UpdateWorklistToBioHubItemCommandValidator(IDocumentReadRepository documentReadRepository, IWorklistToBioHubItemReadRepository worklistToBioHubItemReadRepository)
    {
        _documentReadRepository = documentReadRepository;
        _worklistToBioHubItemReadRepository = worklistToBioHubItemReadRepository;

        RuleFor(cmd => cmd.CurrentStatus)
            .NotNull()
            .WithMessage("Current Status is required");

        RuleFor(cmd => cmd.LastSubmissionApproved)
            .NotNull()
            .WithMessage("Last Submission Approved is required");

        When(cmd => cmd.IsPast == true && cmd.IsSaveDraft != true, () =>
        {
            RuleFor(cmd => cmd.AssignedOperationDate)
                .NotNull()
                .WithMessage("'Operation Date' is required")
                .NotEmpty()
                .WithMessage("'Operation Date' is required");
                
        });

        When(cmd => cmd.LastSubmissionApproved == true, () =>
        {

            When(cmd => cmd.CurrentStatus == WorklistToBioHubStatus.SubmitAnnex2OfSMTA1, () =>
            {
                RuleFor(cmd => cmd.Annex2FillingOption)
                .NotNull()
                .WithMessage("Specify Filling Option");

                When(cmd => cmd.Annex2FillingOption == FillingOption.DocumentUpload, () =>
                {
                    RuleFor(cmd => cmd.File)
                    .NotNull()
                    .WithMessage("File is required")
                    .Must((f) => IsExtensionValid(f))
                    .WithMessage("Invalid File format");
                });

                When(cmd => cmd.Annex2FillingOption == FillingOption.ElectronicallyFill, () =>
                {
                    When(cmd => cmd.IsSaveDraft != true, () =>
                    {
                        RuleFor(cmd => cmd.Annex2TermsAndConditions)
                       .NotNull()
                       .WithMessage("Please select a choice");

                        RuleFor(cmd => cmd.LaboratoryFocalPoints)
                       .Must((lfp) => IsLaboratoryFocalPointsValid(lfp))
                       .WithMessage("Please specify at least a Laboratory Focal Point");

                        RuleFor((cmd) => cmd.MaterialShippingInformations)
                       .Must((msinfo) => IsMaterialShippingInformationNotEmpty(msinfo))
                       .WithMessage("Shipping Informations empty")
                       .Must((msinfo) => IsMaterialShippingInformationValid(msinfo))
                       .WithMessage("Something wrong with Shipping Information or Clinical Details Info");

                        RuleFor(cmd => cmd.BioHubFacilityId)
                       .NotNull()
                       .WithMessage("'BioHub Facility' is required")
                       .NotEmpty()
                       .WithMessage("'BioHub Facility' is required")
                       .MustAsync(async (command, x, token) => await IsSMTA1DocumentSigned(command, token))
                       .WithMessage("SMTA 1 Document not signed yet");

                        When(cmd => cmd.IsPast != true, () =>
                        {
                            RuleFor(cmd => cmd.CurrentDownloadSMTA1DocumentId)
                            .Must((x) => x != null)
                            .WithMessage("Please Select a signed SMTA 1 document");
                        });

                        //# 54317
                        //When(cmd => cmd.Annex2OfSMTA1SignatureId == null, () =>
                        //{
                        //    RuleFor(cmd => cmd.File)
                        //    .NotNull()
                        //    .WithMessage("File is required")
                        //    .Must((f) => IsExtensionValid(f))
                        //    .WithMessage("Invalid File format");
                        //});
                        RuleFor(cmd => cmd.Annex2OfSMTA1SignatureText)
                       .NotNull()
                       .WithMessage("'Signature' is required")
                       .NotEmpty()
                       .WithMessage("'Signature' is required");


                        /////////////////
                    });

                    When(cmd => cmd.IsSaveDraft == true, () =>
                    {

                        RuleFor((cmd) => cmd.MaterialShippingInformations)
                       .Must((msinfo) => IsMaterialShippingInformationValid(msinfo))
                       .WithMessage("Something wrong with Shipping Information or Clinical Details Info");

                        When(cmd => cmd.Annex2OfSMTA1SignatureId == null && cmd.File != null, () =>
                        {
                            RuleFor(cmd => cmd.File)
                            .Must((f) => IsSignatureExtensionValid(f))
                            .WithMessage("Invalid File format");
                        });
                    });
                });
            });

            When(cmd => cmd.CurrentStatus == WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval, () =>
            {

                RuleFor(cmd => cmd.Annex2TermsAndConditions)
                .NotNull()
                .WithMessage("Please select a choice");

                RuleFor(cmd => cmd.Annex2ApprovalFlag)
                .NotNull()
                .WithMessage("Please Confirm");

                RuleFor(cmd => cmd.LaboratoryFocalPoints)
                .Must((lfp) => IsLaboratoryFocalPointsValid(lfp))
                .WithMessage("Please specify at least a Laboratory Focal Point");


                RuleFor((cmd) => cmd.MaterialShippingInformations)
                .Must((msinfo) => IsMaterialShippingInformationNotEmpty(msinfo))
                 .WithMessage("Shipping Informations empty")
                .Must((msinfo) => IsMaterialShippingInformationValid(msinfo))
                .WithMessage("Something wrong with Shipping Information or Clinical Details Info");

                RuleFor(cmd => cmd.BioHubFacilityId)
                       .NotNull()
                       .WithMessage("'BioHub Facility' is required")
                       .NotEmpty()
                       .WithMessage("'BioHub Facility' is required")
                       .MustAsync(async (command, x, token) => await IsSMTA1DocumentSigned(command, token))
                       .WithMessage("SMTA 1 Document not signed yet");

                When(cmd => cmd.IsPast != true, () =>
                {
                    RuleFor(cmd => cmd.CurrentDownloadSMTA1DocumentId)
                    .Must((x) => x != null)
                    .WithMessage("Please Select a signed SMTA 1 document");
                });

            });

            When(cmd => cmd.CurrentStatus == WorklistToBioHubStatus.SubmitBookingFormOfSMTA1, () =>
            {
                RuleFor(cmd => cmd.BookingFormFillingOption)
                .NotNull()
                .WithMessage("Specify Filling Option");

                When(cmd => cmd.BookingFormFillingOption == FillingOption.DocumentUpload, () =>
                {
                    RuleFor(cmd => cmd.File)
                    .NotNull()
                    .WithMessage("File is required")
                    .Must((f) => IsExtensionValid(f))
                    .WithMessage("Invalid File format");
                });

                When(cmd => cmd.BookingFormFillingOption == FillingOption.ElectronicallyFill, () =>
                {
                    When(cmd => cmd.IsSaveDraft != true, () =>
                    {
                        RuleFor((cmd) => cmd.BookingForms)
                       .Must((bf) => IsBookingFormsNotEmpty(bf))
                       .WithMessage("BookingForms empty")
                       .Must((bf) => IsBookingFormsValid(bf))
                       .WithMessage("Something wrong with Booking Forms Info");

                        //# 54317
                        //When(cmd => cmd.BookingFormOfSMTA1SignatureId == null, () =>
                        //{
                        //    RuleFor(cmd => cmd.File)
                        //    .NotNull()
                        //    .WithMessage("File is required")
                        //    .Must((f) => IsExtensionValid(f))
                        //    .WithMessage("Invalid File format");
                        //});
                        RuleFor(cmd => cmd.BookingFormOfSMTA1SignatureText)
                       .NotNull()
                       .WithMessage("'Signature' is required")
                       .NotEmpty()
                       .WithMessage("'Signature' is required");
                        /////////////////
                    });

                    When(cmd => cmd.IsSaveDraft == true, () =>
                    {
                        When(cmd => cmd.BookingFormOfSMTA1SignatureId == null && cmd.File != null, () =>
                        {
                            RuleFor(cmd => cmd.File)
                            .Must((f) => IsSignatureExtensionValid(f))
                            .WithMessage("Invalid File format");
                        });
                    });
                });
            });



            When(cmd => cmd.CurrentStatus == WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval, () =>
            {

                RuleFor(cmd => cmd.BookingFormApprovalFlag)
                .NotNull()
                .WithMessage("Please Confirm");

                RuleFor((cmd) => cmd.BookingForms)
               .Must((bf) => IsBookingFormsNotEmpty(bf))
               .WithMessage("BookingForms empty")
               .Must((bf) => IsBookingFormsValid(bf))
               .WithMessage("Something wrong with Booking Forms Info")
               .Must((bf) => IsBookingFormCouriersValid(bf))
               .WithMessage("Something wrong with Couriers Info");

            });



            When(cmd => cmd.CurrentStatus == WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments, () =>
            {
                When(cmd => cmd.IsSaveDraft == true, () =>
                {
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

                });

            });

            When(cmd => cmd.CurrentStatus == WorklistToBioHubStatus.WaitForPickUpCompleted, () =>
            {

                When(cmd => cmd.IsSaveDraft != true, () =>
                {
                    RuleFor((cmd) => cmd.BookingForms)
                    .Must((bf) => IsBookingFormsNotEmpty(bf))
                    .WithMessage("BookingForms empty")
                    .Must((bf) => IsBookingFormPickupCompletedValid(bf))
                    .WithMessage("Something wrong with Booking Forms Info");

                });

            });

            When(cmd => cmd.CurrentStatus == WorklistToBioHubStatus.WaitForDeliveryCompleted, () =>
            {

                When(cmd => cmd.IsSaveDraft != true, () =>
                {
                    RuleFor((cmd) => cmd.BookingForms)
                    .Must((bf) => IsBookingFormsNotEmpty(bf))
                    .WithMessage("BookingForms empty")
                    .Must((bf) => IsBookingFormDeliveryCompletedValid(bf))
                    .WithMessage("Something wrong with Booking Forms Info");

                });

            });

            When(cmd => cmd.CurrentStatus == WorklistToBioHubStatus.WaitForArrivalConditionCheck, () =>
            {

                When(cmd => cmd.IsSaveDraft != true, () =>
                {
                    RuleFor((cmd) => cmd.MaterialShippingInformations)
                    .Must((bf) => IsShipmentMaterialsValid(bf))
                    .WithMessage("Something Wrong with Shipment Material Information");

                });

            });

            When(cmd => cmd.CurrentStatus == WorklistToBioHubStatus.WaitForCommentBHFSendFeedback, () =>
            {
                RuleFor((cmd) => cmd.NewFeedback)
                .NotEmpty()
                .WithMessage("Feedback is required");

            });

            //When(cmd => cmd.CurrentStatus == WorklistToBioHubStatus.WaitForFinalApproval, () =>
            //{
            //    RuleFor((cmd) => cmd.NewFeedback)
            //    .NotEmpty()
            //    .WithMessage("Feedback is required");

            //});
        });

        When(cmd => cmd.LastSubmissionApproved == false, () =>
        {
            RuleFor(cmd => cmd.Comment)
            .NotNull()
            .WithMessage("Reject Reason is required");
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

    protected bool IsMaterialShippingInformationNotEmpty(IEnumerable<MaterialShippingInformationDto> materialShippingInformations)
    {
        if (materialShippingInformations == null || !materialShippingInformations.Any())
        {
            return false;
        }
        return true;
    }

    protected bool IsMaterialShippingInformationValid(IEnumerable<MaterialShippingInformationDto> materialShippingInformations)
    {

        foreach (var materialShippingInformation in materialShippingInformations)
        {
            if (materialShippingInformation.MaterialProductId == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(materialShippingInformation.Condition))
            {
                return false;
            }

            if (materialShippingInformation.Amount == null || materialShippingInformation.Amount <= 0)
            {
                return false;
            }

            if (materialShippingInformation.Quantity == null || materialShippingInformation.Quantity <= 0)
            {
                return false;
            }


            if (materialShippingInformation.MaterialClinicalDetails == null || !materialShippingInformation.MaterialClinicalDetails.Any())
            {
                return false;
            }

            foreach (var materialClinicalDetail in materialShippingInformation.MaterialClinicalDetails)
            {
                if (materialClinicalDetail.IsolationHostTypeId == null)
                {
                    return false;
                }
                if (materialClinicalDetail.Age == null || materialClinicalDetail.Age < 0)
                {
                    return false;
                }
                if (materialClinicalDetail.CollectionDate == null)
                {
                    return false;
                }
                if (materialClinicalDetail.Gender == null)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(materialClinicalDetail.Location))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(materialClinicalDetail.MaterialNumber))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(materialClinicalDetail.PatientStatus))
                {
                    return false;
                }
            }

            foreach (var materialLaboratoryAnalysisInformation in materialShippingInformation.MaterialLaboratoryAnalysisInformation)
            {
                if (materialLaboratoryAnalysisInformation.FreezingDate == null)
                {
                    return false;
                }
                if (materialLaboratoryAnalysisInformation.Temperature == null)
                {
                    return false;
                }
                if (materialLaboratoryAnalysisInformation.UnitOfMeasureId == null)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(materialLaboratoryAnalysisInformation.VirusConcentration))
                {
                    return false;
                }

                if (materialShippingInformation.MaterialProductId == Guid.Parse(SeedDataConstants.CulturedIsolateProductId))
                {
                    if (string.IsNullOrEmpty(materialLaboratoryAnalysisInformation.CulturingCellLine))
                    {
                        return false;
                    }

                    if (materialLaboratoryAnalysisInformation.CulturingPassagesNumber == null || materialLaboratoryAnalysisInformation.CulturingPassagesNumber < 0)
                    {
                        return false;
                    }
                }

                else if (materialShippingInformation.MaterialProductId == Guid.Parse(SeedDataConstants.ClinicalSpecimenProductId))
                {
                    if (materialLaboratoryAnalysisInformation.CollectedSpecimenTypes == null || !materialLaboratoryAnalysisInformation.CollectedSpecimenTypes.Any())
                    {
                        return false;
                    }

                    if (string.IsNullOrEmpty(materialLaboratoryAnalysisInformation.TypeOfTransportMedium))
                    {
                        return false;
                    }

                    if (string.IsNullOrEmpty(materialLaboratoryAnalysisInformation.BrandOfTransportMedium))
                    {
                        return false;
                    }
                }

                if (materialLaboratoryAnalysisInformation.GSDUploadedToDatabase == null)
                {
                    return false;
                }

                if (materialLaboratoryAnalysisInformation.GSDUploadedToDatabase == YesNoOption.Yes)
                {
                    if (materialLaboratoryAnalysisInformation.DatabaseUsedForGSDUploadingId == null)
                    {
                        return false;
                    }

                    if (string.IsNullOrEmpty(materialLaboratoryAnalysisInformation.AccessionNumberInGSDDatabase))
                    {
                        return false;
                    }
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

            if (bookingForm.TotalNumberOfVials <= 0)
            {
                return false;
            }

            if (bookingForm.TotalAmount <= 0)
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

        if (!shipmentDocuments.Any(x => x.FileType == DocumentFileType.PackagingList))
        {
            return false;
        }

        if (!shipmentDocuments.Any(x => x.FileType == DocumentFileType.NonCommercialInvoiceCatA))
        {
            return false;
        }

        if (!shipmentDocuments.Any(x => x.FileType == DocumentFileType.NonCommercialInvoiceCatB))
        {
            return false;
        }

        if (!shipmentDocuments.Any(x => x.FileType == DocumentFileType.DangerousGoodsDeclaration))
        {
            return false;
        }

        if (!shipmentDocuments.Any(x => x.FileType == DocumentFileType.ExportPermit))
        {
            return false;
        }

        if (!shipmentDocuments.Any(x => x.FileType == DocumentFileType.ImportPermit))
        {
            return false;
        }
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


    protected bool IsShipmentMaterialsValid(IEnumerable<MaterialShippingInformationDto> materialShippingInformations)
    {

        foreach (var materialShippingInformation in materialShippingInformations)
        {

            foreach (var materialClinicalDetail in materialShippingInformation.MaterialClinicalDetails)
            {
                if (materialClinicalDetail.Condition == null)
                {
                    return false;
                }
            }

        }

        return true;
    }

    protected async Task<bool> IsSMTA1DocumentSigned(UpdateWorklistToBioHubItemCommand cmd, CancellationToken cancellationToken)
    {
        if (cmd.IsPast == true)
        {
            return true;
        }

        var laboratoryId = cmd.LaboratoryId.GetValueOrDefault();
        DocumentFileType documentType = DocumentFileType.SMTA1;
        var res = await _documentReadRepository.IsDocumentSignedByLaboratoryId(laboratoryId, documentType, cancellationToken);
        return res;

    }

}