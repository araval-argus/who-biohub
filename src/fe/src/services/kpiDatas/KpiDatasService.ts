import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";

import {
  ReadKpiDataPublicQuery,
  ReadKpiDataPublicResponse,
} from "./models/ReadKpiDataPublic";

import { ReadKpiQuery, ReadKpiResponse } from "./models/ReadKpiData";

export interface iKpiDatasService {
  read(
    query: ReadKpiQuery
  ): Promise<Either<ReadKpiResponse, CommunicationError>>;

  readPublic(
    query: ReadKpiDataPublicQuery
  ): Promise<Either<ReadKpiDataPublicResponse, CommunicationError>>;
}

export class KpiDatasService implements iKpiDatasService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "kpidatas/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadKpiQuery
  ): Promise<Either<ReadKpiResponse, CommunicationError>> {
    const response = await this.httpClient.get<ReadKpiResponse>(this.baseUrl);
    return response;
  }

  async readPublic(
    query: ReadKpiDataPublicQuery
  ): Promise<Either<ReadKpiDataPublicResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ReadKpiDataPublicResponse>(
        this.baseUrl
      );
    return response;
  }
}
