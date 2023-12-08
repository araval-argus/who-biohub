import { WorklistItemUser } from "@/models/WorklistItemUser";

export interface ListUsersByLaboratoryIdForWorklistToBioHubItemQuery {
  LaboratoryId: string;
  WorklistToBioHubItemId: string;
}

export interface ListUsersByLaboratoryIdForWorklistToBioHubItemResponse {
  WorklistToBioHubItemUsers: Array<WorklistItemUser>;
}
