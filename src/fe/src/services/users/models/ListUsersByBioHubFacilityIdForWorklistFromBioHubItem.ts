import { WorklistItemUser } from "@/models/WorklistItemUser";

export interface ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQuery {
  BioHubFacilityId: string;
  WorklistFromBioHubItemId: string;
}

export interface ListUsersByBioHubFacilityIdForWorklistFromBioHubItemResponse {
  WorklistFromBioHubItemUsers: Array<WorklistItemUser>;
}
