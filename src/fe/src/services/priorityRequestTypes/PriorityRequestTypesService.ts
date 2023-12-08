import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreatePriorityRequestTypeCommand,
  CreatePriorityRequestTypeResponse,
} from "./models/CreatePriorityRequestType";
import {
  DeletePriorityRequestTypeCommand,
  DeletePriorityRequestTypeResponse,
} from "./models/DeletePriorityRequestType";
import {
  ListPriorityRequestTypeQuery,
  ListPriorityRequestTypeResponse,
} from "./models/ListPriorityRequestType";
import {
  ReadPriorityRequestTypeQuery,
  ReadPriorityRequestTypeResponse,
} from "./models/ReadPriorityRequestType";
import {
  UpdatePriorityRequestTypeCommand,
  UpdatePriorityRequestTypeResponse,
} from "./models/UpdatePriorityRequestType";

export interface iPriorityRequestTypesService {
  read(
    query: ReadPriorityRequestTypeQuery
  ): Promise<Either<ReadPriorityRequestTypeResponse, CommunicationError>>;

  list(
    query: ListPriorityRequestTypeQuery
  ): Promise<Either<ListPriorityRequestTypeResponse, CommunicationError>>;

  readPublic(
    query: ReadPriorityRequestTypeQuery
  ): Promise<Either<ReadPriorityRequestTypeResponse, CommunicationError>>;

  listPublic(
    query: ListPriorityRequestTypeQuery
  ): Promise<Either<ListPriorityRequestTypeResponse, CommunicationError>>;

  create(
    command: CreatePriorityRequestTypeCommand
  ): Promise<Either<CreatePriorityRequestTypeResponse, CommunicationError>>;

  delete(
    command: DeletePriorityRequestTypeCommand
  ): Promise<Either<DeletePriorityRequestTypeResponse, CommunicationError>>;

  update(
    command: UpdatePriorityRequestTypeCommand
  ): Promise<Either<UpdatePriorityRequestTypeResponse, CommunicationError>>;
}

export class PriorityRequestTypesService
  implements iPriorityRequestTypesService
{
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "priorityrequesttypes/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadPriorityRequestTypeQuery
  ): Promise<Either<ReadPriorityRequestTypeResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadPriorityRequestTypeResponse>(
      url
    );
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListPriorityRequestTypeQuery
  ): Promise<Either<ListPriorityRequestTypeResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListPriorityRequestTypeResponse>(
      this.baseUrl
    );
    return response;
  }

  async readPublic(
    query: ReadPriorityRequestTypeQuery
  ): Promise<Either<ReadPriorityRequestTypeResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClientPublic.getPublic<ReadPriorityRequestTypeResponse>(
        url
      );
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListPriorityRequestTypeQuery
  ): Promise<Either<ListPriorityRequestTypeResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListPriorityRequestTypeResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreatePriorityRequestTypeCommand
  ): Promise<Either<CreatePriorityRequestTypeResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreatePriorityRequestTypeCommand,
      CreatePriorityRequestTypeResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeletePriorityRequestTypeCommand
  ): Promise<Either<DeletePriorityRequestTypeResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeletePriorityRequestTypeResponse>(url);
    return response;
  }

  async update(
    command: UpdatePriorityRequestTypeCommand
  ): Promise<Either<UpdatePriorityRequestTypeResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdatePriorityRequestTypeCommand,
      UpdatePriorityRequestTypeResponse
    >(url, command);
    return response;
  }
}
