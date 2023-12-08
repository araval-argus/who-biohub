import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";

export interface DownloadWorklistToBioHubHistoryItemDocumentQuery {
  Id: string;
  Name: string;
  WorklistId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface DownloadWorklistToBioHubHistoryItemDocumentResponse {}
