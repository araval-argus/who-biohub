import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import { PermissionNames } from "./PermissionNames";

export const WorklistToBioHubSubmitPermissionsByStatusList = new Map<
  WorklistToBioHubStatus,
  string
>([
  [
    WorklistToBioHubStatus.SubmitAnnex2OfSMTA1,
    PermissionNames.CanSubmitAnnex2OfSMTA1,
  ],
  [
    WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
    PermissionNames.CanSubmitWaitingForAnnex2OfSMTA1SECsApproval,
  ],
  [
    WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
    PermissionNames.CanSubmitBookingFormOfSMTA1,
  ],
  [
    WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
    PermissionNames.CanSubmitWaitForBookingFormSMTA1OPSApproval,
  ],
  [
    WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments,
    PermissionNames.CanSubmitSMTA1ShipmentDocuments,
  ],
  [
    WorklistToBioHubStatus.WaitForPickUpCompleted,
    PermissionNames.CanSubmitWaitForPickUpCompleted,
  ],

  [
    WorklistToBioHubStatus.WaitForDeliveryCompleted,
    PermissionNames.CanSubmitWaitForDeliveryCompleted,
  ],

  [
    WorklistToBioHubStatus.WaitForArrivalConditionCheck,
    PermissionNames.CanSubmitWaitForArrivalConditionCheck,
  ],

  [
    WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
    PermissionNames.CanSubmitWaitForCommentBHFSendFeedback,
  ],

  [
    WorklistToBioHubStatus.WaitForFinalApproval,
    PermissionNames.CanSubmitWaitForFinalApproval,
  ],

  [
    WorklistToBioHubStatus.ShipmentCompleted,
    PermissionNames.CanSubmitShipmentCompleted,
  ],
]);

export const WorklistToBioHubSubmitPermissionsByStatusPastList = new Map<
  WorklistToBioHubStatus,
  string
>([
  [
    WorklistToBioHubStatus.SubmitAnnex2OfSMTA1,
    PermissionNames.CanSubmitAnnex2OfSMTA1Past,
  ],
  [
    WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
    PermissionNames.CanSubmitWaitingForAnnex2OfSMTA1SECsApprovalPast,
  ],
  [
    WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
    PermissionNames.CanSubmitBookingFormOfSMTA1Past,
  ],
  [
    WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
    PermissionNames.CanSubmitWaitForBookingFormSMTA1OPSApprovalPast,
  ],
  [
    WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments,
    PermissionNames.CanSubmitSMTA1ShipmentDocumentsPast,
  ],
  [
    WorklistToBioHubStatus.WaitForPickUpCompleted,
    PermissionNames.CanSubmitWaitForPickUpCompletedPast,
  ],

  [
    WorklistToBioHubStatus.WaitForDeliveryCompleted,
    PermissionNames.CanSubmitWaitForDeliveryCompletedPast,
  ],

  [
    WorklistToBioHubStatus.WaitForArrivalConditionCheck,
    PermissionNames.CanSubmitWaitForArrivalConditionCheckPast,
  ],

  [
    WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
    PermissionNames.CanSubmitWaitForCommentBHFSendFeedbackPast,
  ],

  [
    WorklistToBioHubStatus.WaitForFinalApproval,
    PermissionNames.CanSubmitWaitForFinalApprovalPast,
  ],

  [
    WorklistToBioHubStatus.ShipmentCompleted,
    PermissionNames.CanSubmitShipmentCompletedPast,
  ],
]);
