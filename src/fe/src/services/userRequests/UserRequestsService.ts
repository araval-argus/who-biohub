import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateUserRequestCommand,
  CreateUserRequestResponse,
} from "./models/CreateUserRequest";
import {
  CreateUserRequestPublicCommand,
  CreateUserRequestPublicResponse,
} from "./models/CreateUserRequestPublic";
import {
  DeleteUserRequestCommand,
  DeleteUserRequestResponse,
} from "./models/DeleteUserRequest";
import {
  ListUserRequestQuery,
  ListUserRequestResponse,
} from "./models/ListUserRequest";
import {
  ReadUserRequestQuery,
  ReadUserRequestResponse,
} from "./models/ReadUserRequest";
import {
  ReadUserRequestPublicQuery,
  ReadUserRequestPublicResponse,
} from "./models/ReadUserRequestPublic";
import {
  UpdateUserRequestCommand,
  UpdateUserRequestResponse,
} from "./models/UpdateUserRequest";
import {
  UpdateUserRequestPublicCommand,
  UpdateUserRequestPublicResponse,
} from "./models/UpdateUserRequestPublic";
import {
  ApproveOrRejectUserRequestCommand,
  ApproveOrRejectUserRequestResponse,
} from "./models/ApproveOrRejectUserRequest";

export interface iUserRequestsService {
  read(
    query: ReadUserRequestQuery
  ): Promise<Either<ReadUserRequestResponse, CommunicationError>>;

  readPublic(
    query: ReadUserRequestPublicQuery
  ): Promise<Either<ReadUserRequestPublicResponse, CommunicationError>>;

  list(
    query: ListUserRequestQuery
  ): Promise<Either<ListUserRequestResponse, CommunicationError>>;

  create(
    command: CreateUserRequestCommand
  ): Promise<Either<CreateUserRequestResponse, CommunicationError>>;

  createPublic(
    command: CreateUserRequestPublicCommand
  ): Promise<Either<CreateUserRequestPublicResponse, CommunicationError>>;

  delete(
    command: DeleteUserRequestCommand
  ): Promise<Either<DeleteUserRequestResponse, CommunicationError>>;

  update(
    command: UpdateUserRequestCommand
  ): Promise<Either<UpdateUserRequestResponse, CommunicationError>>;

  approveOrReject(
    command: ApproveOrRejectUserRequestCommand
  ): Promise<Either<ApproveOrRejectUserRequestResponse, CommunicationError>>;

  updatePublic(
    command: UpdateUserRequestPublicCommand
  ): Promise<Either<UpdateUserRequestPublicResponse, CommunicationError>>;
}

export class UserRequestsService implements iUserRequestsService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;

  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "userrequests/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadUserRequestQuery
  ): Promise<Either<ReadUserRequestResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadUserRequestResponse>(url);
    return response;
  }

  async readPublic(
    query: ReadUserRequestPublicQuery
  ): Promise<Either<ReadUserRequestPublicResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClientPublic.getPublic<ReadUserRequestPublicResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListUserRequestQuery
  ): Promise<Either<ListUserRequestResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListUserRequestResponse>(
      this.baseUrl
    );
    return response;
  }

  async create(
    command: CreateUserRequestCommand
  ): Promise<Either<CreateUserRequestResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateUserRequestCommand,
      CreateUserRequestResponse
    >(this.baseUrl, command);

    return response;
  }

  async createPublic(
    command: CreateUserRequestPublicCommand
  ): Promise<Either<CreateUserRequestPublicResponse, CommunicationError>> {
    const response = await this.httpClientPublic.postPublic<
      CreateUserRequestPublicCommand,
      CreateUserRequestPublicResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteUserRequestCommand
  ): Promise<Either<DeleteUserRequestResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.delete<DeleteUserRequestResponse>(
      url
    );
    return response;
  }

  async update(
    command: UpdateUserRequestCommand
  ): Promise<Either<UpdateUserRequestResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateUserRequestCommand,
      UpdateUserRequestResponse
    >(url, command);
    return response;
  }

  async approveOrReject(
    command: ApproveOrRejectUserRequestCommand
  ): Promise<Either<ApproveOrRejectUserRequestResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id + "/approveorreject");
    const response = await this.httpClient.patch<
      ApproveOrRejectUserRequestCommand,
      ApproveOrRejectUserRequestResponse
    >(url, command);
    return response;
  }

  async updatePublic(
    command: UpdateUserRequestPublicCommand
  ): Promise<Either<UpdateUserRequestPublicResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClientPublic.patchPublic<
      UpdateUserRequestPublicCommand,
      UpdateUserRequestPublicResponse
    >(url, command);
    return response;
  }
}
