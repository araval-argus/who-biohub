import { UserRequestStatus } from "@/models/UserRequestStatus";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListUserRequestStatusesQuery {}

export interface ListUserRequestStatusesResponse {
  UserRequestStatuses: UserRequestStatus[];
}
