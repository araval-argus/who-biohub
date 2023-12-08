import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateLaboratoryCommand,
  CreateLaboratoryResponse,
} from "./models/CreateLaboratory";
import {
  CreateLaboratoryFromUserRequestCommand,
  CreateLaboratoryFromUserRequestResponse,
} from "./models/CreateLaboratoryFromUserRequest";
import {
  DeleteLaboratoryCommand,
  DeleteLaboratoryResponse,
} from "./models/DeleteLaboratory";
import {
  ListLaboratoryMapQuery,
  ListLaboratoryMapResponse,
} from "./models/ListLaboratoryMap";
import {
  ListLaboratoryQuery,
  ListLaboratoryResponse,
} from "./models/ListLaboratory";
import {
  ReadLaboratoryQuery,
  ReadLaboratoryResponse,
} from "./models/ReadLaboratory";
import {
  UpdateLaboratoryCommand,
  UpdateLaboratoryResponse,
} from "./models/UpdateLaboratory";

import {
  ListLaboratoryPublicQuery,
  ListLaboratoryPublicResponse,
} from "./models/ListLaboratoryPublic";

import {
  ListLaboratoryMapPublicQuery,
  ListLaboratoryMapPublicResponse,
} from "./models/ListLaboratoryMapPublic";

import {
  ReadLaboratoryPublicQuery,
  ReadLaboratoryPublicResponse,
} from "./models/ReadLaboratoryPublic";

export interface iLaboratoriesService {
  read(
    query: ReadLaboratoryQuery
  ): Promise<Either<ReadLaboratoryResponse, CommunicationError>>;

  list(
    query: ListLaboratoryQuery
  ): Promise<Either<ListLaboratoryResponse, CommunicationError>>;

  listMap(
    query: ListLaboratoryMapQuery
  ): Promise<Either<ListLaboratoryMapResponse, CommunicationError>>;

  readPublic(
    query: ReadLaboratoryPublicQuery
  ): Promise<Either<ReadLaboratoryPublicResponse, CommunicationError>>;

  listPublic(
    query: ListLaboratoryPublicQuery
  ): Promise<Either<ListLaboratoryPublicResponse, CommunicationError>>;

  listMapPublic(
    query: ListLaboratoryMapPublicQuery
  ): Promise<Either<ListLaboratoryMapPublicResponse, CommunicationError>>;

  create(
    command: CreateLaboratoryCommand
  ): Promise<Either<CreateLaboratoryResponse, CommunicationError>>;

  createFromUserRequest(
    command: CreateLaboratoryFromUserRequestCommand
  ): Promise<
    Either<CreateLaboratoryFromUserRequestResponse, CommunicationError>
  >;

  delete(
    command: DeleteLaboratoryCommand
  ): Promise<Either<DeleteLaboratoryResponse, CommunicationError>>;

  update(
    command: UpdateLaboratoryCommand
  ): Promise<Either<UpdateLaboratoryResponse, CommunicationError>>;
}

export class LaboratoriesService implements iLaboratoriesService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "laboratories/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadLaboratoryQuery
  ): Promise<Either<ReadLaboratoryResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadLaboratoryResponse>(url);
    return response;
  }

  async readPublic(
    query: ReadLaboratoryPublicQuery
  ): Promise<Either<ReadLaboratoryPublicResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClientPublic.getPublic<ReadLaboratoryPublicResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListLaboratoryQuery
  ): Promise<Either<ListLaboratoryResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListLaboratoryResponse>(
      this.baseUrl
    );
    return response;
  }

  async listMap(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListLaboratoryMapQuery
  ): Promise<Either<ListLaboratoryMapResponse, CommunicationError>> {
    const url = this.buildUrl("map");
    const response = await this.httpClient.get<ListLaboratoryMapResponse>(url);
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListLaboratoryPublicQuery
  ): Promise<Either<ListLaboratoryPublicResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListLaboratoryPublicResponse>(
        this.baseUrl
      );
    return response;
  }

  async listMapPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListLaboratoryMapPublicQuery
  ): Promise<Either<ListLaboratoryMapPublicResponse, CommunicationError>> {
    const url = this.buildUrl("map");
    const response =
      await this.httpClientPublic.getPublic<ListLaboratoryMapPublicResponse>(
        url
      );
    return response;
  }

  async create(
    command: CreateLaboratoryCommand
  ): Promise<Either<CreateLaboratoryResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateLaboratoryCommand,
      CreateLaboratoryResponse
    >(this.baseUrl, command);

    return response;
  }

  async createFromUserRequest(
    command: CreateLaboratoryFromUserRequestCommand
  ): Promise<
    Either<CreateLaboratoryFromUserRequestResponse, CommunicationError>
  > {
    const url = this.buildUrl("createfromuserrequest");
    const response = await this.httpClient.post<
      CreateLaboratoryFromUserRequestCommand,
      CreateLaboratoryFromUserRequestResponse
    >(url, command);

    return response;
  }

  async delete(
    command: DeleteLaboratoryCommand
  ): Promise<Either<DeleteLaboratoryResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.delete<DeleteLaboratoryResponse>(
      url
    );
    return response;
  }

  async update(
    command: UpdateLaboratoryCommand
  ): Promise<Either<UpdateLaboratoryResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateLaboratoryCommand,
      UpdateLaboratoryResponse
    >(url, command);
    return response;
  }
}
