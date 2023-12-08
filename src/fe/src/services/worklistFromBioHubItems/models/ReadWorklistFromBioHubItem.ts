import { WorklistFromBioHubItem } from "@/models/WorklistFromBioHubItem";

export interface ReadWorklistFromBioHubItemQuery {
  Id: string;
}

export interface ReadWorklistFromBioHubItemResponse {
  WorklistFromBioHubItemDto: WorklistFromBioHubItem;
}
