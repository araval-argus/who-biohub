import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import { PermissionNames } from "./PermissionNames";

export const WorklistToBioHubReadPermissionsByStatusList = new Map<
  WorklistToBioHubStatus,
  string
>([
  [
    WorklistToBioHubStatus.SubmitAnnex2OfSMTA1,
    PermissionNames.CanReadSubmitAnnex2OfSMTA1,
  ],
  [
    WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
    PermissionNames.CanReadWaitingForAnnex2OfSMTA1SECsApproval,
  ],
  [
    WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
    PermissionNames.CanReadSubmitBookingFormOfSMTA1,
  ],
  [
    WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
    PermissionNames.CanReadWaitForBookingFormSMTA1OPSApproval,
  ],
  [
    WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments,
    PermissionNames.CanReadSMTA1ShipmentDocuments,
  ],

  [
    WorklistToBioHubStatus.WaitForPickUpCompleted,
    PermissionNames.CanReadWaitForPickUpCompleted,
  ],

  [
    WorklistToBioHubStatus.WaitForDeliveryCompleted,
    PermissionNames.CanReadWaitForDeliveryCompleted,
  ],

  [
    WorklistToBioHubStatus.WaitForArrivalConditionCheck,
    PermissionNames.CanReadWaitForArrivalConditionCheck,
  ],

  [
    WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
    PermissionNames.CanReadWaitForCommentBHFSendFeedback,
  ],

  [
    WorklistToBioHubStatus.WaitForFinalApproval,
    PermissionNames.CanReadWaitForFinalApproval,
  ],

  [
    WorklistToBioHubStatus.ShipmentCompleted,
    PermissionNames.CanReadShipmentCompleted,
  ],
]);

export const WorklistToBioHubReadPermissionsByStatusPastList = new Map<
  WorklistToBioHubStatus,
  string
>([
  [
    WorklistToBioHubStatus.SubmitAnnex2OfSMTA1,
    PermissionNames.CanReadSubmitAnnex2OfSMTA1Past,
  ],
  [
    WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
    PermissionNames.CanReadWaitingForAnnex2OfSMTA1SECsApprovalPast,
  ],
  [
    WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
    PermissionNames.CanReadSubmitBookingFormOfSMTA1Past,
  ],
  [
    WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
    PermissionNames.CanReadWaitForBookingFormSMTA1OPSApprovalPast,
  ],
  [
    WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments,
    PermissionNames.CanReadSMTA1ShipmentDocumentsPast,
  ],

  [
    WorklistToBioHubStatus.WaitForPickUpCompleted,
    PermissionNames.CanReadWaitForPickUpCompletedPast,
  ],

  [
    WorklistToBioHubStatus.WaitForDeliveryCompleted,
    PermissionNames.CanReadWaitForDeliveryCompletedPast,
  ],

  [
    WorklistToBioHubStatus.WaitForArrivalConditionCheck,
    PermissionNames.CanReadWaitForArrivalConditionCheckPast,
  ],

  [
    WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
    PermissionNames.CanReadWaitForCommentBHFSendFeedbackPast,
  ],

  [
    WorklistToBioHubStatus.WaitForFinalApproval,
    PermissionNames.CanReadWaitForFinalApprovalPast,
  ],

  [
    WorklistToBioHubStatus.ShipmentCompleted,
    PermissionNames.CanReadShipmentCompletedPast,
  ],
]);
