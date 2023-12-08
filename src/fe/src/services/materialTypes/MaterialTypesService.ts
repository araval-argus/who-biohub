import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateMaterialTypeCommand,
  CreateMaterialTypeResponse,
} from "./models/CreateMaterialType";
import {
  DeleteMaterialTypeCommand,
  DeleteMaterialTypeResponse,
} from "./models/DeleteMaterialType";
import {
  ListMaterialTypeQuery,
  ListMaterialTypeResponse,
} from "./models/ListMaterialType";
import {
  ReadMaterialTypeQuery,
  ReadMaterialTypeResponse,
} from "./models/ReadMaterialType";
import {
  UpdateMaterialTypeCommand,
  UpdateMaterialTypeResponse,
} from "./models/UpdateMaterialType";

export interface iMaterialTypesService {
  read(
    query: ReadMaterialTypeQuery
  ): Promise<Either<ReadMaterialTypeResponse, CommunicationError>>;

  list(
    query: ListMaterialTypeQuery
  ): Promise<Either<ListMaterialTypeResponse, CommunicationError>>;

  listPublic(
    query: ListMaterialTypeQuery
  ): Promise<Either<ListMaterialTypeResponse, CommunicationError>>;

  create(
    command: CreateMaterialTypeCommand
  ): Promise<Either<CreateMaterialTypeResponse, CommunicationError>>;

  delete(
    command: DeleteMaterialTypeCommand
  ): Promise<Either<DeleteMaterialTypeResponse, CommunicationError>>;

  update(
    command: UpdateMaterialTypeCommand
  ): Promise<Either<UpdateMaterialTypeResponse, CommunicationError>>;
}

export class MaterialTypesService implements iMaterialTypesService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "materialtypes/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadMaterialTypeQuery
  ): Promise<Either<ReadMaterialTypeResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadMaterialTypeResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListMaterialTypeQuery
  ): Promise<Either<ListMaterialTypeResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListMaterialTypeResponse>(
      this.baseUrl
    );
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListMaterialTypeQuery
  ): Promise<Either<ListMaterialTypeResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListMaterialTypeResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreateMaterialTypeCommand
  ): Promise<Either<CreateMaterialTypeResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateMaterialTypeCommand,
      CreateMaterialTypeResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteMaterialTypeCommand
  ): Promise<Either<DeleteMaterialTypeResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.delete<DeleteMaterialTypeResponse>(
      url
    );
    return response;
  }

  async update(
    command: UpdateMaterialTypeCommand
  ): Promise<Either<UpdateMaterialTypeResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateMaterialTypeCommand,
      UpdateMaterialTypeResponse
    >(url, command);
    return response;
  }
}
