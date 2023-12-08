import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateMaterialUsagePermissionCommand,
  CreateMaterialUsagePermissionResponse,
} from "./models/CreateMaterialUsagePermission";
import {
  DeleteMaterialUsagePermissionCommand,
  DeleteMaterialUsagePermissionResponse,
} from "./models/DeleteMaterialUsagePermission";
import {
  ListMaterialUsagePermissionQuery,
  ListMaterialUsagePermissionResponse,
} from "./models/ListMaterialUsagePermission";
import {
  ReadMaterialUsagePermissionQuery,
  ReadMaterialUsagePermissionResponse,
} from "./models/ReadMaterialUsagePermission";
import {
  UpdateMaterialUsagePermissionCommand,
  UpdateMaterialUsagePermissionResponse,
} from "./models/UpdateMaterialUsagePermission";

export interface iMaterialUsagePermissionsService {
  read(
    query: ReadMaterialUsagePermissionQuery
  ): Promise<Either<ReadMaterialUsagePermissionResponse, CommunicationError>>;

  list(
    query: ListMaterialUsagePermissionQuery
  ): Promise<Either<ListMaterialUsagePermissionResponse, CommunicationError>>;

  listPublic(
    query: ListMaterialUsagePermissionQuery
  ): Promise<Either<ListMaterialUsagePermissionResponse, CommunicationError>>;

  create(
    command: CreateMaterialUsagePermissionCommand
  ): Promise<Either<CreateMaterialUsagePermissionResponse, CommunicationError>>;

  delete(
    command: DeleteMaterialUsagePermissionCommand
  ): Promise<Either<DeleteMaterialUsagePermissionResponse, CommunicationError>>;

  update(
    command: UpdateMaterialUsagePermissionCommand
  ): Promise<Either<UpdateMaterialUsagePermissionResponse, CommunicationError>>;
}

export class MaterialUsagePermissionsService
  implements iMaterialUsagePermissionsService
{
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "materialusagepermissions/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadMaterialUsagePermissionQuery
  ): Promise<Either<ReadMaterialUsagePermissionResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClient.get<ReadMaterialUsagePermissionResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListMaterialUsagePermissionQuery
  ): Promise<Either<ListMaterialUsagePermissionResponse, CommunicationError>> {
    const response =
      await this.httpClient.get<ListMaterialUsagePermissionResponse>(
        this.baseUrl
      );
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListMaterialUsagePermissionQuery
  ): Promise<Either<ListMaterialUsagePermissionResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListMaterialUsagePermissionResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreateMaterialUsagePermissionCommand
  ): Promise<
    Either<CreateMaterialUsagePermissionResponse, CommunicationError>
  > {
    const response = await this.httpClient.post<
      CreateMaterialUsagePermissionCommand,
      CreateMaterialUsagePermissionResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteMaterialUsagePermissionCommand
  ): Promise<
    Either<DeleteMaterialUsagePermissionResponse, CommunicationError>
  > {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeleteMaterialUsagePermissionResponse>(url);
    return response;
  }

  async update(
    command: UpdateMaterialUsagePermissionCommand
  ): Promise<
    Either<UpdateMaterialUsagePermissionResponse, CommunicationError>
  > {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateMaterialUsagePermissionCommand,
      UpdateMaterialUsagePermissionResponse
    >(url, command);
    return response;
  }
}
