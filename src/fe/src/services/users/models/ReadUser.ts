import { User } from "@/models/User";

export interface ReadUserQuery {
  Id: string;
}

export interface ReadUserResponse {
  User: User;
}
