using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Shared.Worklists;

namespace WHO.BioHub.Shared.Enums
{

    /// <summary>
    /// Send BMEPP into the BioHub 
    /// </summary>
    public enum WorklistFromBioHubStatus
    {
        [StatusDescription(
            WorklistFromBioHubStatusNames.RequestInitiationTitle,
            WorklistFromBioHubStatusNames.RequestInitiationApprovedInfo,
            WorklistFromBioHubStatusNames.RequestInitiationRejectedInfo)]
        RequestInitiation = 0,


        [StatusDescription(
            WorklistFromBioHubStatusNames.SubmitAnnex2OfSMTA2Title,
            WorklistFromBioHubStatusNames.SubmitAnnex2OfSMTA2ApprovedInfo,
            WorklistFromBioHubStatusNames.SubmitAnnex2OfSMTA2RejectedInfo,
            WorklistFromBioHubStatusNames.SubmitAnnex2OfSMTA2RejectedTitle)]
        SubmitAnnex2OfSMTA2 = 3,

        [StatusDescription(
            WorklistFromBioHubStatusNames.WaitingForAnnex2OfSMTA2SECsApprovalTitle,
            WorklistFromBioHubStatusNames.WaitingForAnnex2OfSMTA2SECsApprovalApprovedInfo,
            WorklistFromBioHubStatusNames.WaitingForAnnex2OfSMTA2SECsApprovalRejectedInfo,
            WorklistFromBioHubStatusNames.WaitingForAnnex2OfSMTA2SECsApprovalRejectedTitle)]
        WaitingForAnnex2OfSMTA2SECsApproval = 4,

        [StatusDescription(
            WorklistFromBioHubStatusNames.SubmitBiosafetyChecklistFormOfSMTA2Title,
            WorklistFromBioHubStatusNames.SubmitBiosafetyChecklistFormOfSMTA2ApprovedInfo,
            WorklistFromBioHubStatusNames.SubmitBiosafetyChecklistFormOfSMTA2RejectedInfo,
            WorklistFromBioHubStatusNames.SubmitBiosafetyChecklistFormOfSMTA2RejectedTitle)]
        SubmitBiosafetyChecklistFormOfSMTA2 = 5,

        [StatusDescription(
            WorklistFromBioHubStatusNames.WaitForBiosafetyChecklistFormSMTA2BSFsApprovalTitle,
            WorklistFromBioHubStatusNames.WaitForBiosafetyChecklistFormSMTA2BSFsApprovalApprovedInfo,
            WorklistFromBioHubStatusNames.WaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectedInfo,
            WorklistFromBioHubStatusNames.WaitForBiosafetyChecklistFormSMTA2BSFsApprovalRejectedTitle)]
        WaitForBiosafetyChecklistFormSMTA2BSFsApproval = 6,

        [StatusDescription(
            WorklistFromBioHubStatusNames.SubmitBookingFormOfSMTA2Title,
            WorklistFromBioHubStatusNames.SubmitBookingFormOfSMTA2ApprovedInfo,
            WorklistFromBioHubStatusNames.SubmitBookingFormOfSMTA2RejectedInfo,
            WorklistFromBioHubStatusNames.SubmitBookingFormOfSMTA2RejectedTitle)]
        SubmitBookingFormOfSMTA2 = 7,

        [StatusDescription(
            WorklistFromBioHubStatusNames.WaitForBookingFormSMTA2OPSsApprovalTitle,
            WorklistFromBioHubStatusNames.WaitForBookingFormSMTA2OPSsApprovalApprovedInfo,
            WorklistFromBioHubStatusNames.WaitForBookingFormSMTA2OPSsApprovalRejectedInfo,
            WorklistFromBioHubStatusNames.WaitForBookingFormSMTA2OPSsApprovalRejectedTitle)]
        WaitForBookingFormSMTA2OPSsApproval = 8,

        [StatusDescription(
            WorklistFromBioHubStatusNames.SubmitBHFSMTA2ShipmentDocumentsTitle,
            WorklistFromBioHubStatusNames.SubmitBHFSMTA2ShipmentDocumentsApprovedInfo,
            WorklistFromBioHubStatusNames.SubmitBHFSMTA2ShipmentDocumentsRejectedInfo,
            WorklistFromBioHubStatusNames.SubmitBHFSMTA2ShipmentDocumentsRejectedTitle)]
        SubmitBHFSMTA2ShipmentDocuments = 9,

        [StatusDescription(
            WorklistFromBioHubStatusNames.SubmitQESMTA2ShipmentDocumentsTitle,
            WorklistFromBioHubStatusNames.SubmitQESMTA2ShipmentDocumentsApprovedInfo,
            WorklistFromBioHubStatusNames.SubmitQESMTA2ShipmentDocumentsRejectedInfo,
            WorklistFromBioHubStatusNames.SubmitQESMTA2ShipmentDocumentsRejectedTitle)]
        SubmitQESMTA2ShipmentDocuments = 10,

        [StatusDescription(
            WorklistFromBioHubStatusNames.WaitForPickUpFromBioHubCompletedTitle,
            WorklistFromBioHubStatusNames.WaitForPickUpFromBioHubCompletedApprovedInfo,
            WorklistFromBioHubStatusNames.WaitForPickUpFromBioHubCompletedRejectedInfo,
            WorklistFromBioHubStatusNames.WaitForPickUpFromBioHubCompletedRejectedTitle)]
        WaitForPickUpCompleted = 11,

        [StatusDescription(
            WorklistFromBioHubStatusNames.WaitForDeliveryFromBioHubCompletedTitle,
            WorklistFromBioHubStatusNames.WaitForDeliveryFromBioHubCompletedApprovedInfo,
            WorklistFromBioHubStatusNames.WaitForDeliveryFromBioHubCompletedRejectedInfo,
            WorklistFromBioHubStatusNames.WaitForDeliveryFromBioHubCompletedRejectedTitle)]
        WaitForDeliveryCompleted = 12,

        [StatusDescription(
            WorklistFromBioHubStatusNames.WaitForArrivalConditionFromBioHubCheckTitle,
            WorklistFromBioHubStatusNames.WaitForArrivalConditionFromBioHubCheckCompletedInfo,
            WorklistFromBioHubStatusNames.WaitForArrivalConditionFromBioHubCheckAskForFeedbackInfo)]
        WaitForArrivalConditionCheck = 13,


        [StatusDescription(
            WorklistFromBioHubStatusNames.WaitForCommentQESendFeedbackTitle,
            WorklistFromBioHubStatusNames.WaitForCommentQESendFeedbackCommentSubmittedInfo,
            WorklistFromBioHubStatusNames.WaitForCommentQESendFeedbackCommentRejectedInfo,
            WorklistFromBioHubStatusNames.WaitForCommentQESendFeedbackCommentRejectedTitle)]
        WaitForCommentQESendFeedback = 14,

        [StatusDescription(
            WorklistFromBioHubStatusNames.WaitForFinalApprovalFromBioHubTitle,
            WorklistFromBioHubStatusNames.WaitForFinalApprovalFromBioHubCompletedInfo,
            WorklistFromBioHubStatusNames.WaitForFinalApprovalFromBioHubAskForFeedbackInfo)]
        WaitForFinalApproval = 15,

        [StatusDescription(
            WorklistFromBioHubStatusNames.ShipmentCompletedTitle,
            WorklistFromBioHubStatusNames.ShipmentCompletedApprovedInfo,
            WorklistFromBioHubStatusNames.ShipmentCompletedRejectedInfo,
            WorklistFromBioHubStatusNames.ShipmentCompletedRejectedTitle)]
        ShipmentCompleted = 16
    }
}
