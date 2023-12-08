import { SMTA2WorkflowStatus } from "@/models/enums/SMTA2WorkflowStatus";
import { PermissionNames } from "./PermissionNames";

export const SMTA2WorkflowDownloadPermissionsByStatusList = new Map<
  SMTA2WorkflowStatus,
  string
>([
  [SMTA2WorkflowStatus.SubmitSMTA2, PermissionNames.CanDownloadFileSubmitSMTA2],
  [
    SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
    PermissionNames.CanDownloadFileWaitingForSMTA2SECsApproval,
  ],
  [
    SMTA2WorkflowStatus.SMTA2WorkflowComplete,
    PermissionNames.CanDownloadFileSMTA2WorkflowComplete,
  ],
]);

export const SMTA2WorkflowDownloadPermissionsByStatusPastList = new Map<
  SMTA2WorkflowStatus,
  string
>([
  [
    SMTA2WorkflowStatus.SubmitSMTA2,
    PermissionNames.CanDownloadFileSubmitSMTA2Past,
  ],
  [
    SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval,
    PermissionNames.CanDownloadFileWaitingForSMTA2SECsApprovalPast,
  ],
  [
    SMTA2WorkflowStatus.SMTA2WorkflowComplete,
    PermissionNames.CanDownloadFileSMTA2WorkflowCompletePast,
  ],
]);
