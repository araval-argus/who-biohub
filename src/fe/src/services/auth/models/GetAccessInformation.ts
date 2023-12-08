import { UserLoginInfo } from "@/models/UserLoginInfo";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface GetAccessInformationQuery {}

export interface GetAccessInformationResponse {
  UserLoginInfo: UserLoginInfo;
}
