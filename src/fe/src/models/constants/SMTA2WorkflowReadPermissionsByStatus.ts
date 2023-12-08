import { SMTA2WorkflowStatus } from "@/models/enums/SMTA2WorkflowStatus";
import { PermissionNames } from "./PermissionNames";

export const SMTA2WorkflowReadPermissionsByStatusList = new Map<
  SMTA2WorkflowStatus,
  string
>([
  [SMTA2WorkflowStatus.SubmitSMTA2, PermissionNames.CanReadSubmitSMTA2],
  [
    SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
    PermissionNames.CanReadWaitingForSMTA2SECsApproval,
  ],
  [
    SMTA2WorkflowStatus.SMTA2WorkflowComplete,
    PermissionNames.CanReadSMTA2WorkflowComplete,
  ],
]);

export const SMTA2WorkflowReadPermissionsByStatusPastList = new Map<
  SMTA2WorkflowStatus,
  string
>([
  [SMTA2WorkflowStatus.SubmitSMTA2, PermissionNames.CanReadSubmitSMTA2Past],
  [
    SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
    PermissionNames.CanReadWaitingForSMTA2SECsApprovalPast,
  ],
  [
    SMTA2WorkflowStatus.SMTA2WorkflowComplete,
    PermissionNames.CanReadSMTA2WorkflowCompletePast,
  ],
]);
