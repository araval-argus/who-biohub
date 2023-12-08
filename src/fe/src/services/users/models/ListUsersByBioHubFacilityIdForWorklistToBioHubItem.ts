import { WorklistItemUser } from "@/models/WorklistItemUser";

export interface ListUsersByBioHubFacilityIdForWorklistToBioHubItemQuery {
  BioHubFacilityId: string;
  WorklistToBioHubItemId: string;
}

export interface ListUsersByBioHubFacilityIdForWorklistToBioHubItemResponse {
  WorklistToBioHubItemUsers: Array<WorklistItemUser>;
}
