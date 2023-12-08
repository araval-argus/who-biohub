using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Worklists;

namespace WHO.BioHub.Shared.Enums
{

    /// <summary>
    /// Send BMEPP into the BioHub 
    /// </summary>
    public enum WorklistToBioHubStatus
    {
        [StatusDescription(
            WorklistToBioHubStatusNames.RequestInitiationTitle,
            WorklistToBioHubStatusNames.RequestInitiationApprovedInfo,
            WorklistToBioHubStatusNames.RequestInitiationRejectedInfo,
            WorklistToBioHubStatusNames.RequestInitiationRejectedTitle)]
        RequestInitiation = 0,

        [StatusDescription(
            WorklistToBioHubStatusNames.SubmitAnnex2OfSMTA1Title,
            WorklistToBioHubStatusNames.SubmitAnnex2OfSMTA1ApprovedInfo,
            WorklistToBioHubStatusNames.SubmitAnnex2OfSMTA1RejectedInfo,
            WorklistToBioHubStatusNames.SubmitAnnex2OfSMTA1RejectedTitle)]
        SubmitAnnex2OfSMTA1 = 4,

        [StatusDescription(
            WorklistToBioHubStatusNames.WaitingForAnnex2OfSMTA1SECsApprovalTitle,
            WorklistToBioHubStatusNames.WaitingForAnnex2OfSMTA1SECsApprovalApprovedInfo,
            WorklistToBioHubStatusNames.WaitingForAnnex2OfSMTA1SECsApprovalRejectedInfo,
            WorklistToBioHubStatusNames.WaitingForAnnex2OfSMTA1SECsApprovalRejectedTitle)]
        WaitingForAnnex2OfSMTA1SECsApproval = 5,

        [StatusDescription(
            WorklistToBioHubStatusNames.SubmitBookingFormOfSMTA1Title,
            WorklistToBioHubStatusNames.SubmitBookingFormOfSMTA1ApprovedInfo,
            WorklistToBioHubStatusNames.SubmitBookingFormOfSMTA1RejectedInfo,
            WorklistToBioHubStatusNames.SubmitBookingFormOfSMTA1RejectedTitle)]
        SubmitBookingFormOfSMTA1 = 6,

        [StatusDescription(
            WorklistToBioHubStatusNames.WaitForBookingFormSMTA1OPSApprovalTitle,
            WorklistToBioHubStatusNames.WaitForBookingFormSMTA1OPSApprovalApprovedInfo,
            WorklistToBioHubStatusNames.WaitForBookingFormSMTA1OPSApprovalRejectedInfo,
            WorklistToBioHubStatusNames.WaitForBookingFormSMTA1OPSApprovalRejectedTitle)]
        WaitForBookingFormSMTA1OPSApproval = 7,

        [StatusDescription(
            WorklistToBioHubStatusNames.SubmitSMTA1ShipmentDocumentsTitle,
            WorklistToBioHubStatusNames.SubmitSMTA1ShipmentDocumentsApprovedInfo,
            WorklistToBioHubStatusNames.SubmitSMTA1ShipmentDocumentsRejectedInfo,
            WorklistToBioHubStatusNames.SubmitSMTA1ShipmentDocumentsRejectedTitle)]
        SubmitSMTA1ShipmentDocuments = 8,

        [StatusDescription(
            WorklistToBioHubStatusNames.WaitForPickUpCompletedTitle,
            WorklistToBioHubStatusNames.WaitForPickUpCompletedApprovedInfo,
            WorklistToBioHubStatusNames.WaitForPickUpCompletedRejectedInfo,
            WorklistToBioHubStatusNames.WaitForPickUpCompletedRejectedTitle)]
        WaitForPickUpCompleted = 9,

        [StatusDescription(
            WorklistToBioHubStatusNames.WaitForDeliveryCompletedTitle,
            WorklistToBioHubStatusNames.WaitForDeliveryCompletedApprovedInfo,
            WorklistToBioHubStatusNames.WaitForDeliveryCompletedRejectedInfo,
            WorklistToBioHubStatusNames.WaitForDeliveryCompletedRejectedTitle)]
        WaitForDeliveryCompleted = 10,

        [StatusDescription(
            WorklistToBioHubStatusNames.WaitForArrivalConditionCheckTitle,
            WorklistToBioHubStatusNames.WaitForArrivalConditionCheckCompletedInfo,
            WorklistToBioHubStatusNames.WaitForArrivalConditionCheckAskForFeedbackInfo)]
        WaitForArrivalConditionCheck = 11,

        [StatusDescription(
            WorklistToBioHubStatusNames.WaitForCommentBHFSendFeedbackTitle,
            WorklistToBioHubStatusNames.WaitForCommentBHFSendFeedbackCommentSubmittedInfo,
            WorklistToBioHubStatusNames.WaitForCommentBHFSendFeedbackCommentRejectedInfo,
            WorklistToBioHubStatusNames.WaitForCommentBHFSendFeedbackCommentRejectedTitle)]
        WaitForCommentBHFSendFeedback = 12,

        [StatusDescription(
            WorklistToBioHubStatusNames.WaitForFinalApprovalTitle,
            WorklistToBioHubStatusNames.WaitForFinalApprovalCompletedInfo,
            WorklistToBioHubStatusNames.WaitForFinalApprovalAskForFeedbackInfo)]
        WaitForFinalApproval = 13,

        [StatusDescription(
            WorklistToBioHubStatusNames.ShipmentCompletedTitle,
            WorklistToBioHubStatusNames.ShipmentCompletedApprovedInfo,
            WorklistToBioHubStatusNames.ShipmentCompletedRejectedInfo,
            WorklistToBioHubStatusNames.ShipmentCompletedRejectedTitle)]
        ShipmentCompleted = 14
    }
}
