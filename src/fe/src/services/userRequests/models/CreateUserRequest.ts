import { UserRegistrationStatus } from "@/models/enums/UserRegistrationStatus";

export interface CreateUserRequestCommand {
  FirstName: string;
  LastName: string;
  Email: string;
  Purpose: string;
  TermsAndConditionAccepted: boolean;
  Status: UserRegistrationStatus;
  RoleId: string;
  CountryId: string;
  LaboratoryId: string;
}

export interface CreateUserRequestResponse {
  Id: string;
}
