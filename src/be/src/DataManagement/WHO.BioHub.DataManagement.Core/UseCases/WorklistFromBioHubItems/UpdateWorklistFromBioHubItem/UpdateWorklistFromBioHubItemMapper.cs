using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItem;

public interface IUpdateWorklistFromBioHubItemMapper
{
    WorklistFromBioHubItem Map(WorklistFromBioHubItem worklistfrombiohubitem, UpdateWorklistFromBioHubItemCommand command);
}

public class UpdateWorklistFromBioHubItemMapper : IUpdateWorklistFromBioHubItemMapper
{
    public WorklistFromBioHubItem Map(
        WorklistFromBioHubItem worklistfrombiohubitem,
        UpdateWorklistFromBioHubItemCommand command
        )
    {
        worklistfrombiohubitem.LastOperationUserId = command.UserId;
        worklistfrombiohubitem.OperationDate = command.IsSaveDraft != true ? (worklistfrombiohubitem.IsPast == true ? command.AssignedOperationDate : DateTime.UtcNow) : worklistfrombiohubitem.OperationDate;
        worklistfrombiohubitem.LastSubmissionApproved = command.IsSaveDraft != true ? command.LastSubmissionApproved : worklistfrombiohubitem.LastSubmissionApproved;
        worklistfrombiohubitem.PreviousStatus = command.IsSaveDraft != true ? worklistfrombiohubitem.Status : worklistfrombiohubitem.PreviousStatus;
        worklistfrombiohubitem.WorklistItemTitle = command.IsSaveDraft != true ? (command.LastSubmissionApproved == true ? worklistfrombiohubitem.Status.WorklistItemApprovedInfo() : worklistfrombiohubitem.Status.WorklistItemRejectedInfo()) : worklistfrombiohubitem.WorklistItemTitle;
        worklistfrombiohubitem.Comment = command.IsSaveDraft != true ? (command.LastSubmissionApproved == true ? string.Empty : command.Comment) : worklistfrombiohubitem.Comment;
        worklistfrombiohubitem.ReferenceId = Guid.NewGuid();

        switch (worklistfrombiohubitem.Status)
        {
            case WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2:
                worklistfrombiohubitem.Annex2FillingOption = command.Annex2FillingOption;
                worklistfrombiohubitem.Annex2ApprovalFlag = command.LastSubmissionApproved == true ? command.Annex2ApprovalFlag : false;
                worklistfrombiohubitem.Annex2ApprovalComment = command.LastSubmissionApproved == true ? command.Annex2ApprovalComment : String.Empty;
                worklistfrombiohubitem.WHODocumentRegistrationNumber = command.LastSubmissionApproved == true ? command.WHODocumentRegistrationNumber : worklistfrombiohubitem.WHODocumentRegistrationNumber;
                worklistfrombiohubitem.RequestInitiationFromBioHubFacilityId = command.Annex2FillingOption == FillingOption.ElectronicallyFill ? command.BioHubFacilityId : null;

                //# 54317
                worklistfrombiohubitem.Annex2OfSMTA2SignatureText = command.Annex2FillingOption == FillingOption.ElectronicallyFill ? (command.LastSubmissionApproved == true ? command.Annex2OfSMTA2SignatureText : worklistfrombiohubitem.Annex2OfSMTA2SignatureText) : string.Empty;
                /////////
                ///

                worklistfrombiohubitem.OriginalAnnex2OfSMTA2DocumentTemplateId = command.OriginalDocumentTemplateAnnex2OfSMTA2DocumentId;
                break;

            case WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval:
                worklistfrombiohubitem.Annex2FillingOption = command.Annex2FillingOption;
                worklistfrombiohubitem.Annex2ApprovalFlag = command.LastSubmissionApproved == true ? command.Annex2ApprovalFlag : false;
                worklistfrombiohubitem.Annex2ApprovalComment = command.LastSubmissionApproved == true ? command.Annex2ApprovalComment : String.Empty;
                worklistfrombiohubitem.WHODocumentRegistrationNumber = command.LastSubmissionApproved == true ? command.WHODocumentRegistrationNumber : worklistfrombiohubitem.WHODocumentRegistrationNumber;
                worklistfrombiohubitem.RequestInitiationFromBioHubFacilityId = command.BioHubFacilityId;

                //# 54317
                worklistfrombiohubitem.Annex2OfSMTA2SignatureText = command.Annex2FillingOption == FillingOption.ElectronicallyFill ? (command.LastSubmissionApproved == true ? command.Annex2OfSMTA2SignatureText : worklistfrombiohubitem.Annex2OfSMTA2SignatureText) : string.Empty;
                /////////

                break;

            case WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2:
                worklistfrombiohubitem.BiosafetyChecklistFillingOption = command.BiosafetyChecklistFillingOption;
                worklistfrombiohubitem.BiosafetyChecklistApprovalFlag = command.LastSubmissionApproved == true ? command.BiosafetyChecklistOfSMTA2ApprovalFlag : false;
                worklistfrombiohubitem.BiosafetyChecklistApprovalComment = command.LastSubmissionApproved == true ? command.BiosafetyChecklistOfSMTA2ApprovalComment : String.Empty;

                //# 54317
                worklistfrombiohubitem.BiosafetyChecklistOfSMTA2SignatureText = command.BiosafetyChecklistFillingOption == FillingOption.ElectronicallyFill ? (command.LastSubmissionApproved == true ? command.BiosafetyChecklistOfSMTA2SignatureText : worklistfrombiohubitem.BiosafetyChecklistOfSMTA2SignatureText) : string.Empty;
                /////////

                worklistfrombiohubitem.OriginalBiosafetyChecklistDocumentTemplateId = command.OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentId;
                break;


            case WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval:
                worklistfrombiohubitem.BiosafetyChecklistFillingOption = command.BiosafetyChecklistFillingOption;
                worklistfrombiohubitem.BiosafetyChecklistApprovalFlag = command.LastSubmissionApproved == true ? command.BiosafetyChecklistOfSMTA2ApprovalFlag : false;
                worklistfrombiohubitem.BiosafetyChecklistApprovalComment = command.LastSubmissionApproved == true ? command.BiosafetyChecklistOfSMTA2ApprovalComment : String.Empty;

                //# 54317
                worklistfrombiohubitem.BiosafetyChecklistOfSMTA2SignatureText = command.BiosafetyChecklistFillingOption == FillingOption.ElectronicallyFill ? (command.LastSubmissionApproved == true ? command.BiosafetyChecklistOfSMTA2SignatureText : worklistfrombiohubitem.BiosafetyChecklistOfSMTA2SignatureText) : string.Empty;
                /////////


                break;

            case WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2:
                worklistfrombiohubitem.OriginalBookingFormOfSMTA2DocumentTemplateId = command.OriginalDocumentTemplateBookingFormOfSMTA2DocumentId;
                worklistfrombiohubitem.BookingFormFillingOption = command.BookingFormFillingOption;
                worklistfrombiohubitem.BookingFormApprovalFlag = command.LastSubmissionApproved == true ? command.BookingFormApprovalFlag : false;
                worklistfrombiohubitem.BookingFormApprovalComment = command.LastSubmissionApproved == true ? command.BookingFormApprovalComment : String.Empty;

                //# 54317
                worklistfrombiohubitem.BookingFormOfSMTA2SignatureText = command.BookingFormFillingOption == FillingOption.ElectronicallyFill ? (command.LastSubmissionApproved == true ? command.BookingFormOfSMTA2SignatureText : worklistfrombiohubitem.BookingFormOfSMTA2SignatureText) : string.Empty;
                /////////
                ///

                worklistfrombiohubitem.OriginalBookingFormOfSMTA2DocumentTemplateId = command.OriginalDocumentTemplateBookingFormOfSMTA2DocumentId;
                break;

            case WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval:
                worklistfrombiohubitem.BookingFormFillingOption = command.BookingFormFillingOption;
                worklistfrombiohubitem.BookingFormApprovalFlag = command.LastSubmissionApproved == true ? command.BookingFormApprovalFlag : false;
                worklistfrombiohubitem.BookingFormApprovalComment = command.LastSubmissionApproved == true ? command.BookingFormApprovalComment : String.Empty;

                //# 54317
                worklistfrombiohubitem.BookingFormOfSMTA2SignatureText = command.BookingFormFillingOption == FillingOption.ElectronicallyFill ? (command.LastSubmissionApproved == true ? command.BookingFormOfSMTA2SignatureText : worklistfrombiohubitem.BookingFormOfSMTA2SignatureText) : string.Empty;
                /////////

                break;

            case WorklistFromBioHubStatus.WaitForArrivalConditionCheck:
                worklistfrombiohubitem.WaitForArrivalConditionCheckApprovalFlag = command.LastSubmissionApproved == true ? command.WaitForArrivalConditionCheckApprovalFlag : false;
                worklistfrombiohubitem.WaitForArrivalConditionCheckApprovalComment = command.LastSubmissionApproved == true ? command.WaitForArrivalConditionCheckApprovalComment : String.Empty;
                break;
        }


        return worklistfrombiohubitem;
    }
}