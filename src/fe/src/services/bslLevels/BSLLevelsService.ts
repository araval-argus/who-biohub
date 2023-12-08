import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
} from "../shared/HttpClient";
import {
  CreateBSLLevelCommand,
  CreateBSLLevelResponse,
} from "./models/CreateBSLLevel";
import {
  DeleteBSLLevelCommand,
  DeleteBSLLevelResponse,
} from "./models/DeleteBSLLevel";
import { ListBSLLevelQuery, ListBSLLevelResponse } from "./models/ListBSLLevel";
import { ReadBSLLevelQuery, ReadBSLLevelResponse } from "./models/ReadBSLLevel";
import {
  UpdateBSLLevelCommand,
  UpdateBSLLevelResponse,
} from "./models/UpdateBSLLevel";

export interface iBSLLevelsService {
  read(
    query: ReadBSLLevelQuery
  ): Promise<Either<ReadBSLLevelResponse, CommunicationError>>;

  list(
    query: ListBSLLevelQuery
  ): Promise<Either<ListBSLLevelResponse, CommunicationError>>;

  create(
    command: CreateBSLLevelCommand
  ): Promise<Either<CreateBSLLevelResponse, CommunicationError>>;

  delete(
    command: DeleteBSLLevelCommand
  ): Promise<Either<DeleteBSLLevelResponse, CommunicationError>>;

  update(
    command: UpdateBSLLevelCommand
  ): Promise<Either<UpdateBSLLevelResponse, CommunicationError>>;
}

export class BSLLevelsService implements iBSLLevelsService {
  private httpClient: iHttpClient;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
  }

  private baseUrl = "bsllevels/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadBSLLevelQuery
  ): Promise<Either<ReadBSLLevelResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadBSLLevelResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListBSLLevelQuery
  ): Promise<Either<ListBSLLevelResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListBSLLevelResponse>(
      this.baseUrl
    );
    return response;
  }

  async create(
    command: CreateBSLLevelCommand
  ): Promise<Either<CreateBSLLevelResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateBSLLevelCommand,
      CreateBSLLevelResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteBSLLevelCommand
  ): Promise<Either<DeleteBSLLevelResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.delete<DeleteBSLLevelResponse>(url);
    return response;
  }

  async update(
    command: UpdateBSLLevelCommand
  ): Promise<Either<UpdateBSLLevelResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateBSLLevelCommand,
      UpdateBSLLevelResponse
    >(url, command);
    return response;
  }
}
