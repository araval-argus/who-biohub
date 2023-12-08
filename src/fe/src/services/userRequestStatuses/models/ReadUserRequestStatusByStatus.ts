import { UserRegistrationStatus } from "@/models/enums/UserRegistrationStatus";
import { UserRequestStatus } from "@/models/UserRequestStatus";

export interface ReadUserRequestStatusByStatusQuery {
  Status: UserRegistrationStatus;
}

export interface ReadUserRequestStatusByStatusQueryResponse {
  UserRequestStatus: UserRequestStatus;
}
