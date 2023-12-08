import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";

export interface ReadWorklistFromBioHubHistoryItemQuery {
  Id: string;
}

export interface ReadWorklistFromBioHubHistoryItemResponse {
  WorklistFromBioHubHistoryItemDto: WorklistFromBioHubItem;
}
