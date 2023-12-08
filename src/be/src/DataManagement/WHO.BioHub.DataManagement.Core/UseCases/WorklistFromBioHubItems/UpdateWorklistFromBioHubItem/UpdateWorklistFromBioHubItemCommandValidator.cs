using FluentValidation;
using Microsoft.AspNetCore.Http;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItem;

public class UpdateWorklistFromBioHubItemCommandValidator : AbstractValidator<UpdateWorklistFromBioHubItemCommand>
{
    private readonly IDocumentReadRepository _documentReadRepository;
    public UpdateWorklistFromBioHubItemCommandValidator(IDocumentReadRepository documentReadRepository)
    {
        _documentReadRepository = documentReadRepository;

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

            When(cmd => cmd.CurrentStatus == WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2, () =>
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
                        RuleFor(cmd => cmd.WorklistFromBioHubItemAnnex2OfSMTA2Conditions)
                       .Must((lfp) => IsAnnex2OfSMTA2ConditionsValid(lfp))
                       .WithMessage("Please flag all the required conditions");

                        RuleFor(cmd => cmd.LaboratoryFocalPoints)
                       .Must((lfp) => IsLaboratoryFocalPointsValid(lfp))
                       .WithMessage("Please specify at least a Laboratory Focal Point");

                        RuleFor((cmd) => cmd.WorklistFromBioHubItemMaterials)
                       .Must((msinfo) => IsMaterialsNotEmpty(msinfo))
                       .WithMessage("Material Informations empty")
                       .Must((msinfo) => IsMaterialsValid(msinfo, false))
                       .WithMessage("Something wrong with Materials Info");

                        RuleFor(cmd => cmd.BioHubFacilityId)
                       .NotNull()
                       .WithMessage("'BioHub Facility' is required")
                       .NotEmpty()
                       .WithMessage("'BioHub Facility' is required")
                       .MustAsync(async (command, x, token) => await IsSMTA2DocumentSigned(command, token))
                       .WithMessage("SMTA 2 Document not signed yet");

                        When(cmd => cmd.IsPast != true, () =>
                        {
                            RuleFor(cmd => cmd.CurrentDownloadSMTA2DocumentId)
                            .Must((x) => x != null)
                            .WithMessage("Please Select a signed SMTA 2 document");
                        });

                        //# 54317
                        //When(cmd => cmd.Annex2OfSMTA2SignatureId == null, () =>
                        //{
                        //    RuleFor(cmd => cmd.File)
                        //    .NotNull()
                        //    .WithMessage("File is required")
                        //    .Must((f) => IsExtensionValid(f))
                        //    .WithMessage("Invalid File format");
                        //});
                        RuleFor(cmd => cmd.Annex2OfSMTA2SignatureText)
                       .NotNull()
                       .WithMessage("'Signature' is required")
                       .NotEmpty()
                       .WithMessage("'Signature' is required");
                        //////////////////
                        ///
                    });

                    When(cmd => cmd.IsSaveDraft == true, () =>
                    {


                        When(cmd => cmd.Annex2OfSMTA2SignatureId == null && cmd.File != null, () =>
                        {
                            RuleFor(cmd => cmd.File)
                            .Must((f) => IsSignatureExtensionValid(f))
                            .WithMessage("Invalid File format");
                        });
                    });
                });
            });

            When(cmd => cmd.CurrentStatus == WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval, () =>
            {

                RuleFor(cmd => cmd.WorklistFromBioHubItemAnnex2OfSMTA2Conditions)
                .Must((lfp) => IsAnnex2OfSMTA2ConditionsValid(lfp))
                .WithMessage("Please flag all the required conditions");

                RuleFor(cmd => cmd.Annex2ApprovalFlag)
                .NotNull()
                .WithMessage("Please Confirm");

                RuleFor(cmd => cmd.LaboratoryFocalPoints)
                .Must((lfp) => IsLaboratoryFocalPointsValid(lfp))
                .WithMessage("Please specify at least a Laboratory Focal Point");


                RuleFor(cmd => cmd.BioHubFacilityId)
                      .NotNull()
                      .WithMessage("'BioHub Facility' is required")
                      .NotEmpty()
                      .WithMessage("'BioHub Facility' is required")
                      .MustAsync(async (command, x, token) => await IsSMTA2DocumentSigned(command, token))
                      .WithMessage("SMTA 2 Document not signed yet");

                When(cmd => cmd.IsPast != true, () =>
                {
                    RuleFor(cmd => cmd.CurrentDownloadSMTA2DocumentId)
                    .Must((x) => x != null)
                    .WithMessage("Please Select a signed SMTA 2 document");
                });

                RuleFor((cmd) => cmd.WorklistFromBioHubItemMaterials)
                .Must((msinfo) => IsMaterialsNotEmpty(msinfo))
                 .WithMessage("Material Informations empty")
                .Must((msinfo) => IsMaterialsValid(msinfo, false, true))
                .WithMessage("Something wrong with Materials Info");

                RuleFor(cmd => cmd.BioHubFacilityId)
                       .NotNull()
                       .WithMessage("'BioHub Facility' is required")
                       .NotEmpty()
                       .WithMessage("'BioHub Facility' is required");



            });



            When(cmd => cmd.CurrentStatus == WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2, () =>
            {
                RuleFor(cmd => cmd.BiosafetyChecklistFillingOption)
                .NotNull()
                .WithMessage("Specify Filling Option");

                When(cmd => cmd.BiosafetyChecklistFillingOption == FillingOption.DocumentUpload, () =>
                {
                    RuleFor(cmd => cmd.File)
                    .NotNull()
                    .WithMessage("File is required")
                    .Must((f) => IsExtensionValid(f))
                    .WithMessage("Invalid File format");
                });

                When(cmd => cmd.BiosafetyChecklistFillingOption == FillingOption.ElectronicallyFill, () =>
                {
                    When(cmd => cmd.IsSaveDraft != true, () =>
                    {
                        //RuleFor(cmd => cmd.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s)
                        //.Must((bsf) => IsBiosafetyChecklistOfSMTA2ConditionsValid(bsf))
                        //.WithMessage("Please flag all the required conditions");

                        //# 54317
                        //When(cmd => cmd.BiosafetyChecklistOfSMTA2SignatureId == null, () =>
                        //{
                        //    RuleFor(cmd => cmd.File)
                        //    .NotNull()
                        //    .WithMessage("File is required")
                        //    .Must((f) => IsExtensionValid(f))
                        //    .WithMessage("Invalid File format");
                        //});
                        RuleFor(cmd => cmd.BiosafetyChecklistOfSMTA2SignatureText)
                       .NotNull()
                       .WithMessage("'Signature' is required")
                       .NotEmpty()
                       .WithMessage("'Signature' is required");
                        /////////////////
                    });

                    When(cmd => cmd.IsSaveDraft == true, () =>
                    {
                        When(cmd => cmd.BiosafetyChecklistOfSMTA2SignatureId == null && cmd.File != null, () =>
                        {
                            RuleFor(cmd => cmd.File)
                            .Must((f) => IsSignatureExtensionValid(f))
                            .WithMessage("Invalid File format");
                        });
                    });
                });
            });


            When(cmd => cmd.CurrentStatus == WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval, () =>
            {

                RuleFor(cmd => cmd.BiosafetyChecklistOfSMTA2ApprovalFlag)
                .NotNull()
                .WithMessage("Please Confirm");

                //RuleFor(cmd => cmd.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s)
                //        .Must((bsf) => IsBiosafetyChecklistOfSMTA2ConditionsValid(bsf))
                //        .WithMessage("Please flag all the required conditions");

            });



            When(cmd => cmd.CurrentStatus == WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2, () =>
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
                        //When(cmd => cmd.BookingFormOfSMTA2SignatureId == null, () =>
                        //{
                        //    RuleFor(cmd => cmd.File)
                        //    .NotNull()
                        //    .WithMessage("File is required")
                        //    .Must((f) => IsExtensionValid(f))
                        //    .WithMessage("Invalid File format");
                        //});
                        RuleFor(cmd => cmd.BookingFormOfSMTA2SignatureText)
                       .NotNull()
                       .WithMessage("'Signature' is required")
                       .NotEmpty()
                       .WithMessage("'Signature' is required");

                        RuleFor((cmd) => cmd.WorklistFromBioHubItemMaterials)
                        .Must((msinfo) => IsMaterialsNotEmpty(msinfo))
                         .WithMessage("Material Informations empty")
                        .Must((msinfo) => IsMaterialsValid(msinfo, true, true))
                        .WithMessage("Something wrong with Materials Info");
                        //////////////////////
                    });

                    When(cmd => cmd.IsSaveDraft == true, () =>
                    {
                        When(cmd => cmd.BookingFormOfSMTA2SignatureId == null && cmd.File != null, () =>
                        {
                            RuleFor(cmd => cmd.File)
                            .Must((f) => IsSignatureExtensionValid(f))
                            .WithMessage("Invalid File format");
                        });
                    });
                });
            });



            When(cmd => cmd.CurrentStatus == WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval, () =>
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



            When(cmd => cmd.CurrentStatus == WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments, () =>
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


            When(cmd => cmd.CurrentStatus == WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments, () =>
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

                //When(cmd => cmd.IsSaveDraft != true, () =>
                //{
                //    RuleFor(cmd => cmd.QEShipmentDocuments)
                //       .Must((f) => ShipmentDocumentsValid(f))
                //       .WithMessage("Please specify at least a document");

                //});
            });

            When(cmd => cmd.CurrentStatus == WorklistFromBioHubStatus.WaitForPickUpCompleted, () =>
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

            When(cmd => cmd.CurrentStatus == WorklistFromBioHubStatus.WaitForDeliveryCompleted, () =>
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

            When(cmd => cmd.CurrentStatus == WorklistFromBioHubStatus.WaitForArrivalConditionCheck, () =>
            {

                When(cmd => cmd.IsSaveDraft != true, () =>
                {
                    RuleFor((cmd) => cmd.WorklistFromBioHubItemMaterials)
                    .Must((bf) => IsShipmentMaterialsValid(bf))
                    .WithMessage("Something Wrong with Material Information");

                });

            });

            When(cmd => cmd.CurrentStatus == WorklistFromBioHubStatus.WaitForCommentQESendFeedback, () =>
            {
                RuleFor((cmd) => cmd.NewFeedback)
                .NotEmpty()
                .WithMessage("Feedback is required");

            });

            //When(cmd => cmd.CurrentStatus == WorklistFromBioHubStatus.WaitForFinalApproval, () =>
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

    protected bool IsMaterialsNotEmpty(IEnumerable<WorklistFromBioHubItemMaterialDto> materials)
    {
        if (materials == null || !materials.Any())
        {
            return false;
        }
        return true;
    }

    protected bool IsMaterialsValid(IEnumerable<WorklistFromBioHubItemMaterialDto> materials, bool checkQuantity, bool checkTransportCategories = false)
    {
        if (materials != null && materials.Any())
        {

            foreach (var material in materials)
            {
                if (material.MaterialProductId == null)
                {
                    return false;
                }

                if (material.Quantity == null || material.Quantity <= 0) // || material.Quantity > material.AvailableQuantity)
                {
                    return false;
                }

                if (checkQuantity && material.Quantity > material.AvailableQuantity)
                {
                    return false;
                }

                if (material.Amount == null || material.Amount <= 0)
                {
                    return false;
                }

                if (checkTransportCategories && material.TransportCategoryId == null)
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


    protected async Task<bool> IsSMTA2DocumentSigned(UpdateWorklistFromBioHubItemCommand cmd, CancellationToken cancellationToken)
    {
        if (cmd.IsPast == true)
        {
            return true;
        }

        var laboratoryId = cmd.LaboratoryId.GetValueOrDefault();
        DocumentFileType documentType = DocumentFileType.SMTA2;
        var res = await _documentReadRepository.IsDocumentSignedByLaboratoryId(laboratoryId, documentType, cancellationToken);
        return res;
    }



}