import { RoleType } from "@/models/enums/RoleType";

export interface UpdateRoleCommand {
  Id: string;
  Name: string;
  Description: string;
  RoleType: RoleType;
  AddToRegistration: boolean;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateRoleResponse {}
