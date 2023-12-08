import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import { CreateRoleCommand, CreateRoleResponse } from "./models/CreateRole";
import { DeleteRoleCommand, DeleteRoleResponse } from "./models/DeleteRole";
import { ListRoleQuery, ListRoleResponse } from "./models/ListRole";
import {
  ListRolePublicQuery,
  ListRolePublicResponse,
} from "./models/ListRolePublic";
import { ReadRoleQuery, ReadRoleResponse } from "./models/ReadRole";
import { UpdateRoleCommand, UpdateRoleResponse } from "./models/UpdateRole";

export interface iRolesService {
  read(
    query: ReadRoleQuery
  ): Promise<Either<ReadRoleResponse, CommunicationError>>;

  list(
    query: ListRoleQuery
  ): Promise<Either<ListRoleResponse, CommunicationError>>;

  listPublic(
    query: ListRolePublicQuery
  ): Promise<Either<ListRolePublicResponse, CommunicationError>>;

  create(
    command: CreateRoleCommand
  ): Promise<Either<CreateRoleResponse, CommunicationError>>;

  delete(
    command: DeleteRoleCommand
  ): Promise<Either<DeleteRoleResponse, CommunicationError>>;

  update(
    command: UpdateRoleCommand
  ): Promise<Either<UpdateRoleResponse, CommunicationError>>;
}

export class RolesService implements iRolesService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "roles/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadRoleQuery
  ): Promise<Either<ReadRoleResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadRoleResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListRoleQuery
  ): Promise<Either<ListRoleResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListRoleResponse>(this.baseUrl);
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListRolePublicQuery
  ): Promise<Either<ListRolePublicResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListRolePublicResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreateRoleCommand
  ): Promise<Either<CreateRoleResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateRoleCommand,
      CreateRoleResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteRoleCommand
  ): Promise<Either<DeleteRoleResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.delete<DeleteRoleResponse>(url);
    return response;
  }

  async update(
    command: UpdateRoleCommand
  ): Promise<Either<UpdateRoleResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateRoleCommand,
      UpdateRoleResponse
    >(url, command);
    return response;
  }
}
