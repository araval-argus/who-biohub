import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";

export interface ReadWorklistToBioHubHistoryItemQuery {
  Id: string;
}

export interface ReadWorklistToBioHubHistoryItemResponse {
  WorklistToBioHubHistoryItemDto: WorklistToBioHubItem;
}
