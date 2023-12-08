import { Role } from "@/models/Role";

export interface ReadRoleQuery {
  Id: string;
}

export interface ReadRoleResponse {
  Role: Role;
}
