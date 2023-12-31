import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import { PermissionNames } from "./PermissionNames";

export const WorklistFromBioHubSubmitPermissionsByStatusList = new Map<
  WorklistFromBioHubStatus,
  string
>([
  [
    WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2,
    PermissionNames.CanSubmitAnnex2OfSMTA2,
  ],
  [
    WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
    PermissionNames.CanSubmitWaitingForAnnex2OfSMTA2SECsApproval,
  ],
  [
    WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
    PermissionNames.CanSubmitBiosafetyChecklistFormOfSMTA2,
  ],
  [
    WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
    PermissionNames.CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApproval,
  ],
  [
    WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
    PermissionNames.CanSubmitBookingFormOfSMTA2,
  ],
  [
    WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
    PermissionNames.CanSubmitWaitForBookingFormSMTA2OPSsApproval,
  ],
  [
    WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments,
    PermissionNames.CanSubmitBHFSMTA2ShipmentDocuments,
  ],
  [
    WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments,
    PermissionNames.CanSubmitQESMTA2ShipmentDocuments,
  ],
  [
    WorklistFromBioHubStatus.WaitForPickUpCompleted,
    PermissionNames.CanSubmitWaitForPickUpFromBioHubCompleted,
  ],
  [
    WorklistFromBioHubStatus.WaitForDeliveryCompleted,
    PermissionNames.CanSubmitWaitForDeliveryFromBioHubCompleted,
  ],
  [
    WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
    PermissionNames.CanSubmitWaitForArrivalConditionFromBioHubCheck,
  ],
  [
    WorklistFromBioHubStatus.WaitForCommentQESendFeedback,
    PermissionNames.CanSubmitWaitForCommentQESendFeedback,
  ],
  [
    WorklistFromBioHubStatus.WaitForFinalApproval,
    PermissionNames.CanSubmitWaitForFinalApprovalFromBioHub,
  ],
  [
    WorklistFromBioHubStatus.ShipmentCompleted,
    PermissionNames.CanSubmitShipmentFromBioHubCompleted,
  ],
]);

export const WorklistFromBioHubSubmitPermissionsByStatusPastList = new Map<
  WorklistFromBioHubStatus,
  string
>([
  [
    WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2,
    PermissionNames.CanSubmitAnnex2OfSMTA2Past,
  ],
  [
    WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval,
    PermissionNames.CanSubmitWaitingForAnnex2OfSMTA2SECsApprovalPast,
  ],
  [
    WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2,
    PermissionNames.CanSubmitBiosafetyChecklistFormOfSMTA2Past,
  ],
  [
    WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval,
    PermissionNames.CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast,
  ],
  [
    WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2,
    PermissionNames.CanSubmitBookingFormOfSMTA2Past,
  ],
  [
    WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval,
    PermissionNames.CanSubmitWaitForBookingFormSMTA2OPSsApprovalPast,
  ],
  [
    WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments,
    PermissionNames.CanSubmitBHFSMTA2ShipmentDocumentsPast,
  ],
  [
    WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments,
    PermissionNames.CanSubmitQESMTA2ShipmentDocumentsPast,
  ],
  [
    WorklistFromBioHubStatus.WaitForPickUpCompleted,
    PermissionNames.CanSubmitWaitForPickUpFromBioHubCompletedPast,
  ],
  [
    WorklistFromBioHubStatus.WaitForDeliveryCompleted,
    PermissionNames.CanSubmitWaitForDeliveryFromBioHubCompletedPast,
  ],
  [
    WorklistFromBioHubStatus.WaitForArrivalConditionCheck,
    PermissionNames.CanSubmitWaitForArrivalConditionFromBioHubCheckPast,
  ],
  [
    WorklistFromBioHubStatus.WaitForCommentQESendFeedback,
    PermissionNames.CanSubmitWaitForCommentQESendFeedbackPast,
  ],
  [
    WorklistFromBioHubStatus.WaitForFinalApproval,
    PermissionNames.CanSubmitWaitForFinalApprovalFromBioHubPast,
  ],
  [
    WorklistFromBioHubStatus.ShipmentCompleted,
    PermissionNames.CanSubmitShipmentFromBioHubCompletedPast,
  ],
]);
