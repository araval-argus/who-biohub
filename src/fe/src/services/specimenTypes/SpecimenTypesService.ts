import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  // HttpClientPublic,
  // iHttpClientPublic,
} from "../shared/HttpClient";

import {
  ListSpecimenTypeQuery,
  ListSpecimenTypeResponse,
} from "./models/ListSpecimenType";
import {
  ReadSpecimenTypeQuery,
  ReadSpecimenTypeResponse,
} from "./models/ReadSpecimenType";

export interface iSpecimenTypesService {
  read(
    query: ReadSpecimenTypeQuery
  ): Promise<Either<ReadSpecimenTypeResponse, CommunicationError>>;

  list(
    query: ListSpecimenTypeQuery
  ): Promise<Either<ListSpecimenTypeResponse, CommunicationError>>;

  // listPublic(
  //   query: ListSpecimenTypeQuery
  // ): Promise<Either<ListSpecimenTypeResponse, CommunicationError>>;
}

export class SpecimenTypesService implements iSpecimenTypesService {
  private httpClient: iHttpClient;
  //private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    // this.httpClientPublic = new HttpClientPublic(
    //   process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    // );
  }

  private baseUrl = "specimentypes/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadSpecimenTypeQuery
  ): Promise<Either<ReadSpecimenTypeResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadSpecimenTypeResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListSpecimenTypeQuery
  ): Promise<Either<ListSpecimenTypeResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListSpecimenTypeResponse>(
      this.baseUrl
    );
    return response;
  }

  // async listPublic(
  //   // eslint-disable-next-line @typescript-eslint/no-unused-vars
  //   query: ListSpecimenTypeQuery
  // ): Promise<Either<ListSpecimenTypeResponse, CommunicationError>> {
  //   const response =
  //     await this.httpClientPublic.getPublic<ListSpecimenTypeResponse>(
  //       this.baseUrl
  //     );
  //   return response;
  // }
}
