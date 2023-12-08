import { UserRegistrationStatus } from "@/models/enums/UserRegistrationStatus";

export interface UpdateUserRequestCommand {
  Id: string;
  FirstName: string;
  LastName: string;
  Email: string;
  Purpose: string;
  TermsAndConditionAccepted: boolean;
  Status: UserRegistrationStatus;
  RoleId: string;
  CountryId: string;
  LaboratoryId: string;
  Message: string;
  IsConfirmed: boolean;
  ConfirmationDate: Date;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateUserRequestResponse {}
