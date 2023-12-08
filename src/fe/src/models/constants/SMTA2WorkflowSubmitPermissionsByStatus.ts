import { SMTA2WorkflowStatus } from "@/models/enums/SMTA2WorkflowStatus";
import { PermissionNames } from "./PermissionNames";

export const SMTA2WorkflowSubmitPermissionsByStatusList = new Map<
  SMTA2WorkflowStatus,
  string
>([
  [SMTA2WorkflowStatus.SubmitSMTA2, PermissionNames.CanSubmitSMTA2],
  [
    SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
    PermissionNames.CanSubmitWaitingForSMTA2SECsApproval,
  ],
  [
    SMTA2WorkflowStatus.SMTA2WorkflowComplete,
    PermissionNames.CanSubmitSMTA2WorkflowComplete,
  ],
]);

export const SMTA2WorkflowSubmitPermissionsByStatusPastList = new Map<
  SMTA2WorkflowStatus,
  string
>([
  [SMTA2WorkflowStatus.SubmitSMTA2, PermissionNames.CanSubmitSMTA2Past],
  [
    SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
    PermissionNames.CanSubmitWaitingForSMTA2SECsApprovalPast,
  ],
  [
    SMTA2WorkflowStatus.SMTA2WorkflowComplete,
    PermissionNames.CanSubmitSMTA2WorkflowCompletePast,
  ],
]);
