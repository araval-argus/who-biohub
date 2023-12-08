import { SMTA1WorkflowStatus } from "@/models/enums/SMTA1WorkflowStatus";
import { PermissionNames } from "./PermissionNames";

export const SMTA1WorkflowReadPermissionsByStatusList = new Map<
  SMTA1WorkflowStatus,
  string
>([
  [SMTA1WorkflowStatus.SubmitSMTA1, PermissionNames.CanReadSubmitSMTA1],
  [
    SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
    PermissionNames.CanReadWaitingForSMTA1SECsApproval,
  ],
  [
    SMTA1WorkflowStatus.SMTA1WorkflowComplete,
    PermissionNames.CanReadSMTA1WorkflowComplete,
  ],
]);

export const SMTA1WorkflowReadPermissionsByStatusPastList = new Map<
  SMTA1WorkflowStatus,
  string
>([
  [SMTA1WorkflowStatus.SubmitSMTA1, PermissionNames.CanReadSubmitSMTA1Past],
  [
    SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
    PermissionNames.CanReadWaitingForSMTA1SECsApprovalPast,
  ],
  [
    SMTA1WorkflowStatus.SMTA1WorkflowComplete,
    PermissionNames.CanReadSMTA1WorkflowCompletePast,
  ],
]);
