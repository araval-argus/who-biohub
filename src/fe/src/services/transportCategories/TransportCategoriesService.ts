import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateTransportCategoryCommand,
  CreateTransportCategoryResponse,
} from "./models/CreateTransportCategory";
import {
  DeleteTransportCategoryCommand,
  DeleteTransportCategoryResponse,
} from "./models/DeleteTransportCategory";
import {
  ListTransportCategoryQuery,
  ListTransportCategoryResponse,
} from "./models/ListTransportCategory";
import {
  ReadTransportCategoryQuery,
  ReadTransportCategoryResponse,
} from "./models/ReadTransportCategory";
import {
  UpdateTransportCategoryCommand,
  UpdateTransportCategoryResponse,
} from "./models/UpdateTransportCategory";

export interface iTransportCategoriesService {
  read(
    query: ReadTransportCategoryQuery
  ): Promise<Either<ReadTransportCategoryResponse, CommunicationError>>;

  list(
    query: ListTransportCategoryQuery
  ): Promise<Either<ListTransportCategoryResponse, CommunicationError>>;

  listPublic(
    query: ListTransportCategoryQuery
  ): Promise<Either<ListTransportCategoryResponse, CommunicationError>>;

  create(
    command: CreateTransportCategoryCommand
  ): Promise<Either<CreateTransportCategoryResponse, CommunicationError>>;

  delete(
    command: DeleteTransportCategoryCommand
  ): Promise<Either<DeleteTransportCategoryResponse, CommunicationError>>;

  update(
    command: UpdateTransportCategoryCommand
  ): Promise<Either<UpdateTransportCategoryResponse, CommunicationError>>;
}

export class TransportCategoriesService implements iTransportCategoriesService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "transportcategories/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadTransportCategoryQuery
  ): Promise<Either<ReadTransportCategoryResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadTransportCategoryResponse>(
      url
    );
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListTransportCategoryQuery
  ): Promise<Either<ListTransportCategoryResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListTransportCategoryResponse>(
      this.baseUrl
    );
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListTransportCategoryQuery
  ): Promise<Either<ListTransportCategoryResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListTransportCategoryResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreateTransportCategoryCommand
  ): Promise<Either<CreateTransportCategoryResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateTransportCategoryCommand,
      CreateTransportCategoryResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteTransportCategoryCommand
  ): Promise<Either<DeleteTransportCategoryResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeleteTransportCategoryResponse>(url);
    return response;
  }

  async update(
    command: UpdateTransportCategoryCommand
  ): Promise<Either<UpdateTransportCategoryResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateTransportCategoryCommand,
      UpdateTransportCategoryResponse
    >(url, command);
    return response;
  }
}
