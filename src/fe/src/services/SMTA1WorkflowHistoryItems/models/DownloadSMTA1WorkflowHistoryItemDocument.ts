import { SMTA1WorkflowStatus } from "@/models/enums/SMTA1WorkflowStatus";

export interface DownloadSMTA1WorkflowHistoryItemDocumentQuery {
  Id: string;
  Name: string;
  WorklistId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface DownloadSMTA1WorkflowHistoryItemDocumentResponse {}
