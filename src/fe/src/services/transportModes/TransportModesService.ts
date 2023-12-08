import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateTransportModeCommand,
  CreateTransportModeResponse,
} from "./models/CreateTransportMode";
import {
  DeleteTransportModeCommand,
  DeleteTransportModeResponse,
} from "./models/DeleteTransportMode";
import {
  ListTransportModeQuery,
  ListTransportModeResponse,
} from "./models/ListTransportMode";
import {
  ReadTransportModeQuery,
  ReadTransportModeResponse,
} from "./models/ReadTransportMode";
import {
  UpdateTransportModeCommand,
  UpdateTransportModeResponse,
} from "./models/UpdateTransportMode";

export interface iTransportModesService {
  read(
    query: ReadTransportModeQuery
  ): Promise<Either<ReadTransportModeResponse, CommunicationError>>;

  list(
    query: ListTransportModeQuery
  ): Promise<Either<ListTransportModeResponse, CommunicationError>>;

  readPublic(
    query: ReadTransportModeQuery
  ): Promise<Either<ReadTransportModeResponse, CommunicationError>>;

  listPublic(
    query: ListTransportModeQuery
  ): Promise<Either<ListTransportModeResponse, CommunicationError>>;

  create(
    command: CreateTransportModeCommand
  ): Promise<Either<CreateTransportModeResponse, CommunicationError>>;

  delete(
    command: DeleteTransportModeCommand
  ): Promise<Either<DeleteTransportModeResponse, CommunicationError>>;

  update(
    command: UpdateTransportModeCommand
  ): Promise<Either<UpdateTransportModeResponse, CommunicationError>>;
}

export class TransportModesService implements iTransportModesService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "transportmodes/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadTransportModeQuery
  ): Promise<Either<ReadTransportModeResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadTransportModeResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListTransportModeQuery
  ): Promise<Either<ListTransportModeResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListTransportModeResponse>(
      this.baseUrl
    );
    return response;
  }

  async readPublic(
    query: ReadTransportModeQuery
  ): Promise<Either<ReadTransportModeResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClientPublic.getPublic<ReadTransportModeResponse>(url);
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListTransportModeQuery
  ): Promise<Either<ListTransportModeResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListTransportModeResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreateTransportModeCommand
  ): Promise<Either<CreateTransportModeResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateTransportModeCommand,
      CreateTransportModeResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteTransportModeCommand
  ): Promise<Either<DeleteTransportModeResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.delete<DeleteTransportModeResponse>(
      url
    );
    return response;
  }

  async update(
    command: UpdateTransportModeCommand
  ): Promise<Either<UpdateTransportModeResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateTransportModeCommand,
      UpdateTransportModeResponse
    >(url, command);
    return response;
  }
}
