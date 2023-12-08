import { UserRegistrationStatus } from "@/models/enums/UserRegistrationStatus";

export interface CreateUserRequestPublicCommand {
  FirstName: string;
  LastName: string;
  Email: string;
  Purpose: string;
  TermsAndConditionAccepted: boolean;
  Status: UserRegistrationStatus;
  RoleId: string;
  CountryId: string;
  InstituteName: string;
}

export interface CreateUserRequestPublicResponse {
  Id: string;
}
