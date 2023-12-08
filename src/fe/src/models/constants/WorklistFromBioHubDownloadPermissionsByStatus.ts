import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import { PermissionNames } from "./PermissionNames";

export const WorklistFromBioHubDownloadPermissionsByStatusList = new Map<
  WorklistFromBioHubStatus,
  string
>([
  [
    WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2,
    PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA2,
  ],
  [
    WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
    PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA2SECsApproval,
  ],
  [
    WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
    PermissionNames.CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2,
  ],
  [
    WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
    PermissionNames.CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApproval,
  ],
  [
    WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
    PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA2,
  ],
  [
    WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
    PermissionNames.CanDownloadFileWaitForBookingFormSMTA2OPSsApproval,
  ],
  [
    WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments,
    PermissionNames.CanDownloadBHFSMTA2ShipmentDocuments,
  ],
  [
    WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments,
    PermissionNames.CanDownloadQESMTA2ShipmentDocuments,
  ],
  [
    WorklistFromBioHubStatus.WaitForPickUpCompleted,
    PermissionNames.CanDownloadFileWaitForPickUpFromBioHubCompleted,
  ],
  [
    WorklistFromBioHubStatus.WaitForDeliveryCompleted,
    PermissionNames.CanDownloadFileWaitForDeliveryFromBioHubCompleted,
  ],
  [
    WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
    PermissionNames.CanDownloadFileWaitForArrivalConditionFromBioHubCheck,
  ],
  [
    WorklistFromBioHubStatus.WaitForCommentQESendFeedback,
    PermissionNames.CanDownloadFileWaitForCommentQESendFeedback,
  ],
  [
    WorklistFromBioHubStatus.WaitForFinalApproval,
    PermissionNames.CanDownloadFileWaitForFinalApprovalFromBioHub,
  ],
  [
    WorklistFromBioHubStatus.ShipmentCompleted,
    PermissionNames.CanDownloadFileShipmentFromBioHubCompleted,
  ],
]);

export const WorklistFromBioHubDownloadPermissionsByStatusPastList = new Map<
  WorklistFromBioHubStatus,
  string
>([
  [
    WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2,
    PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA2Past,
  ],
  [
    WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
    PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPast,
  ],
  [
    WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
    PermissionNames.CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2Past,
  ],
  [
    WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
    PermissionNames.CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast,
  ],
  [
    WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
    PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA2Past,
  ],
  [
    WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
    PermissionNames.CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPast,
  ],
  [
    WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments,
    PermissionNames.CanDownloadBHFSMTA2ShipmentDocumentsPast,
  ],
  [
    WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments,
    PermissionNames.CanDownloadQESMTA2ShipmentDocumentsPast,
  ],
  [
    WorklistFromBioHubStatus.WaitForPickUpCompleted,
    PermissionNames.CanDownloadFileWaitForPickUpFromBioHubCompletedPast,
  ],
  [
    WorklistFromBioHubStatus.WaitForDeliveryCompleted,
    PermissionNames.CanDownloadFileWaitForDeliveryFromBioHubCompletedPast,
  ],
  [
    WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
    PermissionNames.CanDownloadFileWaitForArrivalConditionFromBioHubCheckPast,
  ],
  [
    WorklistFromBioHubStatus.WaitForCommentQESendFeedback,
    PermissionNames.CanDownloadFileWaitForCommentQESendFeedbackPast,
  ],
  [
    WorklistFromBioHubStatus.WaitForFinalApproval,
    PermissionNames.CanDownloadFileWaitForFinalApprovalFromBioHubPast,
  ],
  [
    WorklistFromBioHubStatus.ShipmentCompleted,
    PermissionNames.CanDownloadFileShipmentFromBioHubCompletedPast,
  ],
]);