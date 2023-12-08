import { WorklistItemUser } from "@/models/WorklistItemUser";

export interface ListUsersByLaboratoryIdForWorklistFromBioHubItemQuery {
  LaboratoryId: string;
  WorklistFromBioHubItemId: string;
}

export interface ListUsersByLaboratoryIdForWorklistFromBioHubItemResponse {
  WorklistFromBioHubItemUsers: Array<WorklistItemUser>;
}
