import { RoleType } from "./enums/RoleType";

export interface UserLoginInfo {
  UserId: string;
  LoggedUserName: string;
  Email: string;
  LaboratoryId: string;
  BioHubFacilityId: string;
  CourierId: string;
  RoleId: string;
  RoleType: RoleType;
  RoleName: string;
  RoleDescription: string;
  JobTitle: string;
  BusinessPhone: string;
  MobilePhone: string;
  FirstName: string;
  LastName: string;
  LandingPage: string;
  UserLogged: boolean;
  UserPermissions: Array<UserPermission>;
}

export interface UserPermission {
  PermissionId: string;
  PermissionName: string;
}
