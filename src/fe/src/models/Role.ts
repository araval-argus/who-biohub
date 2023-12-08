import { RoleType } from "./enums/RoleType";

export interface Role {
  Id: string;
  Name: string;
  Description: string;
  RoleType: RoleType;
  AddToRegistration: boolean;
}
