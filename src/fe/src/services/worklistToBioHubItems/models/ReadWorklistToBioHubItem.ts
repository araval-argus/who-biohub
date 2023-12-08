import { WorklistToBioHubItem } from "@/models/WorklistToBioHubItem";

export interface ReadWorklistToBioHubItemQuery {
  Id: string;
}

export interface ReadWorklistToBioHubItemResponse {
  WorklistToBioHubItemDto: WorklistToBioHubItem;
}
