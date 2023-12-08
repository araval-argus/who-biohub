import { SMTA2WorkflowStatus } from "@/models/enums/SMTA2WorkflowStatus";

export interface DownloadSMTA2WorkflowHistoryItemDocumentQuery {
  Id: string;
  Name: string;
  WorklistId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface DownloadSMTA2WorkflowHistoryItemDocumentResponse {}
