using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHO.BioHub.Shared.Worklists
{
    public static class WorklistToBioHubStatusNames
    {
        public const string RequestInitiationTitle = "Request Initiation";
        public const string RequestInitiationApprovedInfo = "Initiated Request";
        public const string RequestInitiationRejectedInfo = "";
        public const string RequestInitiationRejectedTitle = "";

        public const string SubmitAnnex2OfSMTA1Title = "Submit Annex 2 of SMTA 1";
        public const string SubmitAnnex2OfSMTA1ApprovedInfo = "Submitted Annex 2 of SMTA 1";
        public const string SubmitAnnex2OfSMTA1RejectedInfo = "";
        public const string SubmitAnnex2OfSMTA1RejectedTitle = "Resubmit Annex 2 of SMTA 1";

        public const string WaitingForAnnex2OfSMTA1SECsApprovalTitle = "Approve submitted Annex 2 of SMTA 1";
        public const string WaitingForAnnex2OfSMTA1SECsApprovalApprovedInfo = "Approved submitted Annex 2 of SMTA 1";
        public const string WaitingForAnnex2OfSMTA1SECsApprovalRejectedInfo = "Returned submitted Annex 2 of SMTA 1";
        public const string WaitingForAnnex2OfSMTA1SECsApprovalRejectedTitle = "";

        public const string SubmitBookingFormOfSMTA1Title = "Submit Booking Form";
        public const string SubmitBookingFormOfSMTA1ApprovedInfo = "Submitted Booking Form";
        public const string SubmitBookingFormOfSMTA1RejectedInfo = "";
        public const string SubmitBookingFormOfSMTA1RejectedTitle = "Resubmit Booking Form";

        public const string WaitForBookingFormSMTA1OPSApprovalTitle = "Approve submitted Booking Form";
        public const string WaitForBookingFormSMTA1OPSApprovalApprovedInfo = "Approved submitted Booking Form";
        public const string WaitForBookingFormSMTA1OPSApprovalRejectedInfo = "Returned submitted Booking Form";
        public const string WaitForBookingFormSMTA1OPSApprovalRejectedTitle = "";

        public const string SubmitSMTA1ShipmentDocumentsTitle = "Submit other shipment documents";
        public const string SubmitSMTA1ShipmentDocumentsApprovedInfo = "Submitted other shipment documents";
        public const string SubmitSMTA1ShipmentDocumentsRejectedInfo = "";
        public const string SubmitSMTA1ShipmentDocumentsRejectedTitle = "";

        public const string WaitForPickUpCompletedTitle = "Confirm BMEPP pick-up completion";
        public const string WaitForPickUpCompletedApprovedInfo = "Confirmed BMEPP pick-up completion";
        public const string WaitForPickUpCompletedRejectedInfo = "";
        public const string WaitForPickUpCompletedRejectedTitle = "";

        public const string WaitForDeliveryCompletedTitle = "Confirm BMEPP delivery completion";
        public const string WaitForDeliveryCompletedApprovedInfo = "Confirmed BMEPP delivery completion";
        public const string WaitForDeliveryCompletedRejectedInfo = "";
        public const string WaitForDeliveryCompletedRejectedTitle = "";


        public const string WaitForArrivalConditionCheckTitle = "Confirm BMEPP arrival condition";
        public const string WaitForArrivalConditionCheckCompletedInfo = "Completed shipment process";
        public const string WaitForArrivalConditionCheckAskForFeedbackInfo = "Sent comment about BMEPP arrival condition";


        public const string WaitForCommentBHFSendFeedbackTitle = "Respond to BioHub Facility's comment";
        public const string WaitForCommentBHFSendFeedbackCommentSubmittedInfo = "Responded to BioHub Facility's comment";
        public const string WaitForCommentBHFSendFeedbackCommentRejectedInfo = "";
        public const string WaitForCommentBHFSendFeedbackCommentRejectedTitle = "";

        public const string WaitForFinalApprovalTitle = "Check BMEPP Provider's response";
        public const string WaitForFinalApprovalCompletedInfo = "Completed shipment process";
        public const string WaitForFinalApprovalAskForFeedbackInfo = "Sent comment about BMEPP arrival condition";


        public const string ShipmentCompletedTitle = "No more actions (all processes completed)";
        public const string ShipmentCompletedApprovedInfo = "";
        public const string ShipmentCompletedRejectedInfo = "";
        public const string ShipmentCompletedRejectedTitle = "";

    }
}
