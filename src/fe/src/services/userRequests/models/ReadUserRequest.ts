import { UserRequest } from "@/models/UserRequest";

export interface ReadUserRequestQuery {
  Id: string;
}

export interface ReadUserRequestResponse {
  UserRequest: UserRequest;
}
