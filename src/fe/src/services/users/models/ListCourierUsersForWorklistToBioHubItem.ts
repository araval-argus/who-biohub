import { WorklistItemUser } from "@/models/WorklistItemUser";

export interface ListCourierUsersForWorklistToBioHubItemQuery {
  WorklistToBioHubItemId: string;
}

export interface ListCourierUsersForWorklistToBioHubItemResponse {
  WorklistToBioHubItemUsers: Array<WorklistItemUser>;
}
