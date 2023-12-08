import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  ListUserRequestStatusesQuery,
  ListUserRequestStatusesResponse,
} from "./models/ListUserRequestStatuses";
import {
  ReadUserRequestStatusQuery,
  ReadUserRequestStatusQueryResponse,
} from "./models/ReadUserRequestStatus";
import {
  ReadUserRequestStatusByStatusQuery,
  ReadUserRequestStatusByStatusQueryResponse,
} from "./models/ReadUserRequestStatusByStatus";

export interface iUserRequestStatusesService {
  read(
    query: ReadUserRequestStatusQuery
  ): Promise<Either<ReadUserRequestStatusQueryResponse, CommunicationError>>;

  readByStatus(
    query: ReadUserRequestStatusByStatusQuery
  ): Promise<
    Either<ReadUserRequestStatusByStatusQueryResponse, CommunicationError>
  >;
}

export class UserRequestStatusesService implements iUserRequestStatusesService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;

  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "userrequeststatuses/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadUserRequestStatusQuery
  ): Promise<Either<ReadUserRequestStatusQueryResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClient.get<ReadUserRequestStatusQueryResponse>(url);
    return response;
  }

  async readByStatus(
    query: ReadUserRequestStatusByStatusQuery
  ): Promise<
    Either<ReadUserRequestStatusByStatusQueryResponse, CommunicationError>
  > {
    const url = this.buildUrl(query.Status.toString());
    const response =
      await this.httpClientPublic.getPublic<ReadUserRequestStatusByStatusQueryResponse>(
        url
      );
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListUserRequestStatusesQuery
  ): Promise<Either<ListUserRequestStatusesResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListUserRequestStatusesResponse>(
      this.baseUrl
    );
    return response;
  }
}
