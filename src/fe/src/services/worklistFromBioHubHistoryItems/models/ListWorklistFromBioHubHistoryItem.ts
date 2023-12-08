import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListWorklistFromBioHubHistoryItemQuery {
  WorklistFromBioHubItemId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListWorklistFromBioHubHistoryItemResponse {
  WorklistFromBioHubHistoryItems: WorklistFromBioHubItem[];
}
