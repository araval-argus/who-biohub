import { RoleType } from "@/models/enums/RoleType";

export interface CreateRoleCommand {
  Name: string;
  Description: string;
  RoleType: RoleType;
  AddToRegistration: boolean;
}

export interface CreateRoleResponse {
  Id: string;
}
