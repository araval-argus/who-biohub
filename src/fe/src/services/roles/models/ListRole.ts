import { Role } from "@/models/Role";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListRoleQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListRoleResponse {
  Roles: Role[];
}
