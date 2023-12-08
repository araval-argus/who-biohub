import { UserRegistrationStatus } from "@/models/enums/UserRegistrationStatus";
import { UserRequestStatus } from "@/models/UserRequestStatus";

export const userRequestStatuses =
  process.env?.VUE_APP_USE_MOCKS !== "true"
    ? []
    : new Array<UserRequestStatus>(
        {
          Id: "8A6E7AD2-DA3D-4078-B755-6B7E5C683820",
          Message: "Default Message For Pending Request",
          IsResponseMessage: false,
          Status: UserRegistrationStatus.Pending,
        },
        {
          Id: "46E8AA2A-67CB-4140-AE3C-1E147EBE65DD",
          Message: "Default Message For Approved Request",
          IsResponseMessage: false,
          Status: UserRegistrationStatus.Approved,
        },
        {
          Id: "651B49BA-BF5B-4C5E-BACC-3BA9D421AE7B",
          Message: "Default Message For Rejected Request",
          IsResponseMessage: false,
          Status: UserRegistrationStatus.Rejected,
        }
      );
