import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateShipmentCommand,
  CreateShipmentResponse,
} from "./models/CreateShipment";
import {
  DeleteShipmentCommand,
  DeleteShipmentResponse,
} from "./models/DeleteShipment";
import { ListShipmentQuery, ListShipmentResponse } from "./models/ListShipment";
import { ReadShipmentQuery, ReadShipmentResponse } from "./models/ReadShipment";
import {
  UpdateShipmentCommand,
  UpdateShipmentResponse,
} from "./models/UpdateShipment";
import {
  ListShipmentPublicQuery,
  ListShipmentPublicResponse,
} from "./models/ListShipmentPublic";
import {
  ReadShipmentPublicQuery,
  ReadShipmentPublicResponse,
} from "./models/ReadShipmentPublic";

export interface iShipmentsService {
  read(
    query: ReadShipmentQuery
  ): Promise<Either<ReadShipmentResponse, CommunicationError>>;

  list(
    query: ListShipmentQuery
  ): Promise<Either<ListShipmentResponse, CommunicationError>>;

  readPublic(
    query: ReadShipmentQuery
  ): Promise<Either<ReadShipmentPublicResponse, CommunicationError>>;

  listPublic(
    query: ListShipmentPublicQuery
  ): Promise<Either<ListShipmentPublicResponse, CommunicationError>>;

  create(
    command: CreateShipmentCommand
  ): Promise<Either<CreateShipmentResponse, CommunicationError>>;

  delete(
    command: DeleteShipmentCommand
  ): Promise<Either<DeleteShipmentResponse, CommunicationError>>;

  update(
    command: UpdateShipmentCommand
  ): Promise<Either<UpdateShipmentResponse, CommunicationError>>;
}

export class ShipmentsService implements iShipmentsService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "shipments/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadShipmentQuery
  ): Promise<Either<ReadShipmentResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadShipmentResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListShipmentQuery
  ): Promise<Either<ListShipmentResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListShipmentResponse>(
      this.baseUrl
    );
    return response;
  }

  async readPublic(
    query: ReadShipmentPublicQuery
  ): Promise<Either<ReadShipmentPublicResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClientPublic.getPublic<ReadShipmentPublicResponse>(url);
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListShipmentPublicQuery
  ): Promise<Either<ListShipmentPublicResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListShipmentPublicResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreateShipmentCommand
  ): Promise<Either<CreateShipmentResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateShipmentCommand,
      CreateShipmentResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteShipmentCommand
  ): Promise<Either<DeleteShipmentResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.delete<DeleteShipmentResponse>(url);
    return response;
  }

  async update(
    command: UpdateShipmentCommand
  ): Promise<Either<UpdateShipmentResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateShipmentCommand,
      UpdateShipmentResponse
    >(url, command);
    return response;
  }
}
