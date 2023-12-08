import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";
import { PermissionNames } from "./PermissionNames";

export const WorklistToBioHubDownloadPermissionsByStatusList = new Map<
  WorklistToBioHubStatus,
  string
>([
  [
    WorklistToBioHubStatus.SubmitAnnex2OfSMTA1,
    PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA1,
  ],
  [
    WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
    PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA1SECsApproval,
  ],
  [
    WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
    PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA1,
  ],
  [
    WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
    PermissionNames.CanDownloadFileWaitForBookingFormSMTA1OPSApproval,
  ],
  [
    WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments,
    PermissionNames.CanDownloadSMTA1ShipmentDocuments,
  ],
  [
    WorklistToBioHubStatus.WaitForPickUpCompleted,
    PermissionNames.CanDownloadFileWaitForPickUpCompleted,
  ],

  [
    WorklistToBioHubStatus.WaitForDeliveryCompleted,
    PermissionNames.CanDownloadFileWaitForDeliveryCompleted,
  ],

  [
    WorklistToBioHubStatus.WaitForArrivalConditionCheck,
    PermissionNames.CanDownloadFileWaitForArrivalConditionCheck,
  ],

  [
    WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
    PermissionNames.CanDownloadFileWaitForCommentBHFSendFeedback,
  ],

  [
    WorklistToBioHubStatus.WaitForFinalApproval,
    PermissionNames.CanDownloadFileWaitForFinalApproval,
  ],

  [
    WorklistToBioHubStatus.ShipmentCompleted,
    PermissionNames.CanDownloadFileShipmentCompleted,
  ],
]);

export const WorklistToBioHubDownloadPermissionsByStatusPastList = new Map<
  WorklistToBioHubStatus,
  string
>([
  [
    WorklistToBioHubStatus.SubmitAnnex2OfSMTA1,
    PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA1Past,
  ],
  [
    WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval,
    PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPast,
  ],
  [
    WorklistToBioHubStatus.SubmitBookingFormOfSMTA1,
    PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA1Past,
  ],
  [
    WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval,
    PermissionNames.CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPast,
  ],
  [
    WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments,
    PermissionNames.CanDownloadSMTA1ShipmentDocumentsPast,
  ],
  [
    WorklistToBioHubStatus.WaitForPickUpCompleted,
    PermissionNames.CanDownloadFileWaitForPickUpCompletedPast,
  ],

  [
    WorklistToBioHubStatus.WaitForDeliveryCompleted,
    PermissionNames.CanDownloadFileWaitForDeliveryCompletedPast,
  ],

  [
    WorklistToBioHubStatus.WaitForArrivalConditionCheck,
    PermissionNames.CanDownloadFileWaitForArrivalConditionCheckPast,
  ],

  [
    WorklistToBioHubStatus.WaitForCommentBHFSendFeedback,
    PermissionNames.CanDownloadFileWaitForCommentBHFSendFeedbackPast,
  ],

  [
    WorklistToBioHubStatus.WaitForFinalApproval,
    PermissionNames.CanDownloadFileWaitForFinalApprovalPast,
  ],

  [
    WorklistToBioHubStatus.ShipmentCompleted,
    PermissionNames.CanDownloadFileShipmentCompletedPast,
  ],
]);
