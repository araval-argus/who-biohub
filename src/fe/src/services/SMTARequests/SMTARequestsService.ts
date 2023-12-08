import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";

import {
  ListSMTARequestQuery,
  ListSMTARequestResponse,
} from "./models/ListSMTARequest";

export interface iSMTARequestsService {
  list(
    query: ListSMTARequestQuery
  ): Promise<Either<ListSMTARequestResponse, CommunicationError>>;
}

export class SMTARequestsService implements iSMTARequestsService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "smtarequests/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListSMTARequestQuery
  ): Promise<Either<ListSMTARequestResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListSMTARequestResponse>(
      this.baseUrl
    );
    return response;
  }
}
