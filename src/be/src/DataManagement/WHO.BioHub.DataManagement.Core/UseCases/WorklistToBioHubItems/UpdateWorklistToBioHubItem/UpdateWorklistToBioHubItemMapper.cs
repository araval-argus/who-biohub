using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItem;

public interface IUpdateWorklistToBioHubItemMapper
{
    WorklistToBioHubItem Map(WorklistToBioHubItem worklisttobiohubitem, UpdateWorklistToBioHubItemCommand command);
}

public class UpdateWorklistToBioHubItemMapper : IUpdateWorklistToBioHubItemMapper
{
    public WorklistToBioHubItem Map(
        WorklistToBioHubItem worklisttobiohubitem,
        UpdateWorklistToBioHubItemCommand command
        )
    {
        worklisttobiohubitem.LastOperationUserId = command.IsSaveDraft != true ? command.UserId : worklisttobiohubitem.LastOperationUserId;
        worklisttobiohubitem.OperationDate = command.IsSaveDraft != true ? (worklisttobiohubitem.IsPast == true ? command.AssignedOperationDate : DateTime.UtcNow) : worklisttobiohubitem.OperationDate;
        worklisttobiohubitem.LastSubmissionApproved = command.IsSaveDraft != true ? command.LastSubmissionApproved : worklisttobiohubitem.LastSubmissionApproved;
        worklisttobiohubitem.PreviousStatus = command.IsSaveDraft != true ? worklisttobiohubitem.Status : worklisttobiohubitem.PreviousStatus;
        worklisttobiohubitem.WorklistItemTitle = command.IsSaveDraft != true ? (command.LastSubmissionApproved == true ? worklisttobiohubitem.Status.WorklistItemApprovedInfo() : worklisttobiohubitem.Status.WorklistItemRejectedInfo()) : worklisttobiohubitem.WorklistItemTitle;
        worklisttobiohubitem.Comment = command.IsSaveDraft != true ? (command.LastSubmissionApproved == true ? string.Empty : command.Comment) : worklisttobiohubitem.Comment;
        worklisttobiohubitem.ReferenceId = Guid.NewGuid();

        switch (worklisttobiohubitem.Status)
        {
            case WorklistToBioHubStatus.SubmitAnnex2OfSMTA1:
            
                worklisttobiohubitem.Annex2Comment = command.Annex2Comment;
                worklisttobiohubitem.Annex2FillingOption = command.Annex2FillingOption;
                worklisttobiohubitem.Annex2TermsAndConditions = command.Annex2TermsAndConditions;
                worklisttobiohubitem.Annex2ApprovalFlag = command.LastSubmissionApproved == true ? command.Annex2ApprovalFlag : false;
                worklisttobiohubitem.Annex2ApprovalComment = command.LastSubmissionApproved == true ? command.Annex2ApprovalComment : String.Empty;
                worklisttobiohubitem.WHODocumentRegistrationNumber = command.LastSubmissionApproved == true ? command.WHODocumentRegistrationNumber : worklisttobiohubitem.WHODocumentRegistrationNumber;

                worklisttobiohubitem.RequestInitiationToBioHubFacilityId = command.Annex2FillingOption == FillingOption.ElectronicallyFill ? command.BioHubFacilityId : null;


                //# 54317
                worklisttobiohubitem.Annex2OfSMTA1SignatureText = command.Annex2FillingOption == FillingOption.ElectronicallyFill ? (command.LastSubmissionApproved == true ? command.Annex2OfSMTA1SignatureText : worklisttobiohubitem.Annex2OfSMTA1SignatureText) : string.Empty;
                /////////

                worklisttobiohubitem.OriginalAnnex2OfSMTA1DocumentTemplateId = command.OriginalDocumentTemplateAnnex2OfSMTA1DocumentId;

                break;

            case WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval:
                worklisttobiohubitem.Annex2Comment = command.Annex2Comment;
                worklisttobiohubitem.Annex2FillingOption = command.Annex2FillingOption;
                worklisttobiohubitem.Annex2TermsAndConditions = command.Annex2TermsAndConditions;
                worklisttobiohubitem.Annex2ApprovalFlag = command.LastSubmissionApproved == true ? command.Annex2ApprovalFlag : false;
                worklisttobiohubitem.Annex2ApprovalComment = command.LastSubmissionApproved == true ? command.Annex2ApprovalComment : String.Empty;
                worklisttobiohubitem.WHODocumentRegistrationNumber = command.LastSubmissionApproved == true ? command.WHODocumentRegistrationNumber : worklisttobiohubitem.WHODocumentRegistrationNumber;

                worklisttobiohubitem.RequestInitiationToBioHubFacilityId = command.BioHubFacilityId;
                

                //# 54317
                worklisttobiohubitem.Annex2OfSMTA1SignatureText = command.Annex2FillingOption == FillingOption.ElectronicallyFill ? (command.LastSubmissionApproved == true ? command.Annex2OfSMTA1SignatureText : worklisttobiohubitem.Annex2OfSMTA1SignatureText) : string.Empty;
                /////////
                break;

            case WorklistToBioHubStatus.SubmitBookingFormOfSMTA1:
          
                worklisttobiohubitem.BookingFormFillingOption = command.BookingFormFillingOption;
                worklisttobiohubitem.BookingFormApprovalFlag = command.LastSubmissionApproved == true ? command.BookingFormApprovalFlag : false;
                worklisttobiohubitem.BookingFormApprovalComment = command.LastSubmissionApproved == true ? command.BookingFormApprovalComment : String.Empty;
                //# 54317
                worklisttobiohubitem.BookingFormOfSMTA1SignatureText = command.BookingFormFillingOption == FillingOption.ElectronicallyFill ? (command.LastSubmissionApproved == true ? command.BookingFormOfSMTA1SignatureText : worklisttobiohubitem.BookingFormOfSMTA1SignatureText) : string.Empty;
                /////////

                worklisttobiohubitem.OriginalBookingFormOfSMTA1DocumentTemplateId = command.OriginalDocumentTemplateBookingFormOfSMTA1DocumentId;

                break;

            case WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval:
                worklisttobiohubitem.BookingFormFillingOption = command.BookingFormFillingOption;
                worklisttobiohubitem.BookingFormApprovalFlag = command.LastSubmissionApproved == true ? command.BookingFormApprovalFlag : false;
                worklisttobiohubitem.BookingFormApprovalComment = command.LastSubmissionApproved == true ? command.BookingFormApprovalComment : String.Empty;
                //# 54317
                worklisttobiohubitem.BookingFormOfSMTA1SignatureText = command.BookingFormFillingOption == FillingOption.ElectronicallyFill ? (command.LastSubmissionApproved == true ? command.BookingFormOfSMTA1SignatureText : worklisttobiohubitem.BookingFormOfSMTA1SignatureText) : string.Empty;
                /////////


                break;

            case WorklistToBioHubStatus.WaitForArrivalConditionCheck:
                worklisttobiohubitem.WaitForArrivalConditionCheckApprovalFlag = command.LastSubmissionApproved == true ? command.WaitForArrivalConditionCheckApprovalFlag : false;
                worklisttobiohubitem.WaitForArrivalConditionCheckApprovalComment = command.LastSubmissionApproved == true ? command.WaitForArrivalConditionCheckApprovalComment : String.Empty;
                break;
        }


        return worklisttobiohubitem;
    }
}