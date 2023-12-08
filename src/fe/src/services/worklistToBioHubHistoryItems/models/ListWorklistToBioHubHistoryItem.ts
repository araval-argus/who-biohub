import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListWorklistToBioHubHistoryItemQuery {
  WorklistToBioHubItemId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListWorklistToBioHubHistoryItemResponse {
  WorklistToBioHubHistoryItems: WorklistToBioHubItem[];
}
