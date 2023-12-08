import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateIsolationHostTypeCommand,
  CreateIsolationHostTypeResponse,
} from "./models/CreateIsolationHostType";
import {
  DeleteIsolationHostTypeCommand,
  DeleteIsolationHostTypeResponse,
} from "./models/DeleteIsolationHostType";
import {
  ListIsolationHostTypeQuery,
  ListIsolationHostTypeResponse,
} from "./models/ListIsolationHostType";
import {
  ReadIsolationHostTypeQuery,
  ReadIsolationHostTypeResponse,
} from "./models/ReadIsolationHostType";
import {
  UpdateIsolationHostTypeCommand,
  UpdateIsolationHostTypeResponse,
} from "./models/UpdateIsolationHostType";

export interface iIsolationHostTypesService {
  read(
    query: ReadIsolationHostTypeQuery
  ): Promise<Either<ReadIsolationHostTypeResponse, CommunicationError>>;

  list(
    query: ListIsolationHostTypeQuery
  ): Promise<Either<ListIsolationHostTypeResponse, CommunicationError>>;

  listPublic(
    query: ListIsolationHostTypeQuery
  ): Promise<Either<ListIsolationHostTypeResponse, CommunicationError>>;

  create(
    command: CreateIsolationHostTypeCommand
  ): Promise<Either<CreateIsolationHostTypeResponse, CommunicationError>>;

  delete(
    command: DeleteIsolationHostTypeCommand
  ): Promise<Either<DeleteIsolationHostTypeResponse, CommunicationError>>;

  update(
    command: UpdateIsolationHostTypeCommand
  ): Promise<Either<UpdateIsolationHostTypeResponse, CommunicationError>>;
}

export class IsolationHostTypesService implements iIsolationHostTypesService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "isolationhosttypes/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadIsolationHostTypeQuery
  ): Promise<Either<ReadIsolationHostTypeResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadIsolationHostTypeResponse>(
      url
    );
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListIsolationHostTypeQuery
  ): Promise<Either<ListIsolationHostTypeResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListIsolationHostTypeResponse>(
      this.baseUrl
    );
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListIsolationHostTypeQuery
  ): Promise<Either<ListIsolationHostTypeResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListIsolationHostTypeResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreateIsolationHostTypeCommand
  ): Promise<Either<CreateIsolationHostTypeResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateIsolationHostTypeCommand,
      CreateIsolationHostTypeResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteIsolationHostTypeCommand
  ): Promise<Either<DeleteIsolationHostTypeResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeleteIsolationHostTypeResponse>(url);
    return response;
  }

  async update(
    command: UpdateIsolationHostTypeCommand
  ): Promise<Either<UpdateIsolationHostTypeResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateIsolationHostTypeCommand,
      UpdateIsolationHostTypeResponse
    >(url, command);
    return response;
  }
}
