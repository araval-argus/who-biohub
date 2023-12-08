import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";

import {
  ListShipmentRequestQuery,
  ListShipmentRequestResponse,
} from "./models/ListShipmentRequest";

export interface iShipmentRequestsService {
  list(
    query: ListShipmentRequestQuery
  ): Promise<Either<ListShipmentRequestResponse, CommunicationError>>;
}

export class ShipmentRequestsService implements iShipmentRequestsService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "shipmentrequests/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListShipmentRequestQuery
  ): Promise<Either<ListShipmentRequestResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListShipmentRequestResponse>(
      this.baseUrl
    );
    return response;
  }
}
