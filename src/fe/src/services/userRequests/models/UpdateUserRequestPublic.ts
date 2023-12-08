import { UserRegistrationStatus } from "@/models/enums/UserRegistrationStatus";

export interface UpdateUserRequestPublicCommand {
  Id: string;
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

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateUserRequestPublicResponse {}
