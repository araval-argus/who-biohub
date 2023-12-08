import { User } from "@/models/User";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListUserQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListUserResponse {
  Users: User[];
}
