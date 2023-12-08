import { UserRegistrationStatus } from "./enums/UserRegistrationStatus";

export interface UserRequestStatus {
  Id: string;
  Message: string;
  IsResponseMessage: boolean;
  Status: UserRegistrationStatus;
}
