import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateMaterialProductCommand,
  CreateMaterialProductResponse,
} from "./models/CreateMaterialProduct";
import {
  DeleteMaterialProductCommand,
  DeleteMaterialProductResponse,
} from "./models/DeleteMaterialProduct";
import {
  ListMaterialProductQuery,
  ListMaterialProductResponse,
} from "./models/ListMaterialProduct";
import {
  ReadMaterialProductQuery,
  ReadMaterialProductResponse,
} from "./models/ReadMaterialProduct";
import {
  UpdateMaterialProductCommand,
  UpdateMaterialProductResponse,
} from "./models/UpdateMaterialProduct";

export interface iMaterialProductsService {
  read(
    query: ReadMaterialProductQuery
  ): Promise<Either<ReadMaterialProductResponse, CommunicationError>>;

  list(
    query: ListMaterialProductQuery
  ): Promise<Either<ListMaterialProductResponse, CommunicationError>>;

  listPublic(
    query: ListMaterialProductQuery
  ): Promise<Either<ListMaterialProductResponse, CommunicationError>>;

  create(
    command: CreateMaterialProductCommand
  ): Promise<Either<CreateMaterialProductResponse, CommunicationError>>;

  delete(
    command: DeleteMaterialProductCommand
  ): Promise<Either<DeleteMaterialProductResponse, CommunicationError>>;

  update(
    command: UpdateMaterialProductCommand
  ): Promise<Either<UpdateMaterialProductResponse, CommunicationError>>;
}

export class MaterialProductsService implements iMaterialProductsService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "materialproducts/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadMaterialProductQuery
  ): Promise<Either<ReadMaterialProductResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadMaterialProductResponse>(
      url
    );
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListMaterialProductQuery
  ): Promise<Either<ListMaterialProductResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListMaterialProductResponse>(
      this.baseUrl
    );
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListMaterialProductQuery
  ): Promise<Either<ListMaterialProductResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListMaterialProductResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreateMaterialProductCommand
  ): Promise<Either<CreateMaterialProductResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateMaterialProductCommand,
      CreateMaterialProductResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteMaterialProductCommand
  ): Promise<Either<DeleteMaterialProductResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeleteMaterialProductResponse>(url);
    return response;
  }

  async update(
    command: UpdateMaterialProductCommand
  ): Promise<Either<UpdateMaterialProductResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateMaterialProductCommand,
      UpdateMaterialProductResponse
    >(url, command);
    return response;
  }
}
