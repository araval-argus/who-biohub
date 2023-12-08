import { Either, isLeft } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
} from "../shared/HttpClient";
import { iMsalClient, MsalClient } from "../shared/MsalClient";
import {
  GetAccessInformationQuery,
  GetAccessInformationResponse,
} from "./models/GetAccessInformation";

export interface iAuthService {
  checkUserAccess(
    query: GetAccessInformationQuery
  ): Promise<Either<GetAccessInformationResponse, CommunicationError>>;
  loginCheck(
    query: GetAccessInformationQuery
  ): Promise<Either<GetAccessInformationResponse, CommunicationError>>;
}

export class AuthService implements iAuthService {
  private httpClient: iHttpClient;
  private msalClient: iMsalClient;

  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.msalClient = new MsalClient();
  }

  async loginAsync(): Promise<void> {
    await this.msalClient.startLoginRedirectAsync();
  }

  async logoutAsync(): Promise<void> {
    await this.msalClient.logoutRedirectAsync();
  }

  async getTokenAsync(): Promise<string> {
    await this.msalClient.loginSilentAsync();
    const acquireTokenResult = await this.msalClient.acquireTokenSilentAsync();
    if (isLeft(acquireTokenResult)) {
      return acquireTokenResult.value.accessToken;
    } else {
      const accountInfo = this.msalClient.getMyAccountInfo();
      if (isLeft(accountInfo)) {
        await this.msalClient.acquireTokenRedirectAsync();
      }
      return "";
    }
  }

  async checkUserAccess(
    query: GetAccessInformationQuery
  ): Promise<Either<GetAccessInformationResponse, CommunicationError>> {
    const url = "checkuseraccess/";
    const response = await this.httpClient.get<GetAccessInformationResponse>(
      url
    );
    return response;
  }

  async loginCheck(
    query: GetAccessInformationQuery
  ): Promise<Either<GetAccessInformationResponse, CommunicationError>> {
    const url = "auth/";
    const response = await this.httpClient.get<GetAccessInformationResponse>(
      url
    );
    return response;
  }
}
