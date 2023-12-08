import { UserRequestStatus } from "@/models/UserRequestStatus";

export interface ReadUserRequestStatusQuery {
  Id: string;
}

export interface ReadUserRequestStatusQueryResponse {
  UserRequestStatus: UserRequestStatus;
}
