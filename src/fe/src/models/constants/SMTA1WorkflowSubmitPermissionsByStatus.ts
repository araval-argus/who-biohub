import { SMTA1WorkflowStatus } from "@/models/enums/SMTA1WorkflowStatus";
import { PermissionNames } from "./PermissionNames";

export const SMTA1WorkflowSubmitPermissionsByStatusList = new Map<
  SMTA1WorkflowStatus,
  string
>([
  [SMTA1WorkflowStatus.SubmitSMTA1, PermissionNames.CanSubmitSMTA1],
  [
    SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
    PermissionNames.CanSubmitWaitingForSMTA1SECsApproval,
  ],
  [
    SMTA1WorkflowStatus.SMTA1WorkflowComplete,
    PermissionNames.CanSubmitSMTA1WorkflowComplete,
  ],
]);

export const SMTA1WorkflowSubmitPermissionsByStatusPastList = new Map<
  SMTA1WorkflowStatus,
  string
>([
  [SMTA1WorkflowStatus.SubmitSMTA1, PermissionNames.CanSubmitSMTA1Past],
  [
    SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
    PermissionNames.CanSubmitWaitingForSMTA1SECsApprovalPast,
  ],
  [
    SMTA1WorkflowStatus.SMTA1WorkflowComplete,
    PermissionNames.CanSubmitSMTA1WorkflowCompletePast,
  ],
]);
