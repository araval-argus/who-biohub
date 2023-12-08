import { WorklistItemUser } from "@/models/WorklistItemUser";

export interface ListCourierUsersForWorklistFromBioHubItemQuery {
  WorklistFromBioHubItemId: string;
}

export interface ListCourierUsersForWorklistFromBioHubItemResponse {
  WorklistFromBioHubItemUsers: Array<WorklistItemUser>;
}
