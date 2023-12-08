import { SMTA1WorkflowStatus } from "@/models/enums/SMTA1WorkflowStatus";
import { PermissionNames } from "./PermissionNames";

export const SMTA1WorkflowDownloadPermissionsByStatusList = new Map<
  SMTA1WorkflowStatus,
  string
>([
  [SMTA1WorkflowStatus.SubmitSMTA1, PermissionNames.CanDownloadFileSubmitSMTA1],
  [
    SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
    PermissionNames.CanDownloadFileWaitingForSMTA1SECsApproval,
  ],
  [
    SMTA1WorkflowStatus.SMTA1WorkflowComplete,
    PermissionNames.CanDownloadFileSMTA1WorkflowComplete,
  ],
]);

export const SMTA1WorkflowDownloadPermissionsByStatusPastList = new Map<
  SMTA1WorkflowStatus,
  string
>([
  [
    SMTA1WorkflowStatus.SubmitSMTA1,
    PermissionNames.CanDownloadFileSubmitSMTA1Past,
  ],
  [
    SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval,
    PermissionNames.CanDownloadFileWaitingForSMTA1SECsApprovalPast,
  ],
  [
    SMTA1WorkflowStatus.SMTA1WorkflowComplete,
    PermissionNames.CanDownloadFileSMTA1WorkflowCompletePast,
  ],
]);
