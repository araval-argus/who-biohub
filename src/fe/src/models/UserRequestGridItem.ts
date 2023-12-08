import { UserRegistrationStatus } from "./enums/UserRegistrationStatus";

export interface UserRequestGridItem {
  Id: string;
  Requests: string;
  Email: string;
  Role: string;
  InstituteName: string;
  Country: string;
  RequestDate: string;
  RegistrationDate: string;
  Status: UserRegistrationStatus;
}
