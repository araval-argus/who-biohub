import { RoleType } from "./enums/RoleType";

export interface RoleGridItem {
  Id: string;
  Name: string;
  Description: string;
  RoleType: RoleType;
  AddToRegistration: string;
}
