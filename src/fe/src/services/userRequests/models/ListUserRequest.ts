import { UserRequest } from "@/models/UserRequest";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListUserRequestQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListUserRequestResponse {
  UserRequests: UserRequest[];
}
