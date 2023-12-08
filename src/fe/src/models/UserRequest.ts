import { UserRegistrationStatus } from "./enums/UserRegistrationStatus";

export interface UserRequest {
  Id: string;
  FirstName: string;
  LastName: string;
  Email: string;
  Purpose: string;
  Status: UserRegistrationStatus;
  TermsAndConditionAccepted: boolean;
  RequestDate: Date;
  RoleId: string;
  CountryId: string;
  Message: string;
  IsConfirmed: boolean;
  InstituteName: string;
  ConfirmationDate: Date;
  RegistrationDate: Date;
  LaboratoryId: string;
  RecaptchaResponse: string;
}
