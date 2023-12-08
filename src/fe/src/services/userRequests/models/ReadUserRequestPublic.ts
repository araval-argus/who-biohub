import { UserRequest } from "@/models/UserRequest";

export interface ReadUserRequestPublicQuery {
  Id: string;
}

export interface ReadUserRequestPublicResponse {
  UserRequest: UserRequest;
}
