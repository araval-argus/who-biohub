import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
} from "../shared/HttpClient";
import {
  CreateIsolationTechniqueTypeCommand,
  CreateIsolationTechniqueTypeResponse,
} from "./models/CreateIsolationTechniqueType";
import {
  DeleteIsolationTechniqueTypeCommand,
  DeleteIsolationTechniqueTypeResponse,
} from "./models/DeleteIsolationTechniqueType";
import {
  ListIsolationTechniqueTypeQuery,
  ListIsolationTechniqueTypeResponse,
} from "./models/ListIsolationTechniqueType";
import {
  ReadIsolationTechniqueTypeQuery,
  ReadIsolationTechniqueTypeResponse,
} from "./models/ReadIsolationTechniqueType";
import {
  UpdateIsolationTechniqueTypeCommand,
  UpdateIsolationTechniqueTypeResponse,
} from "./models/UpdateIsolationTechniqueType";

export interface iIsolationTechniqueTypesService {
  read(
    query: ReadIsolationTechniqueTypeQuery
  ): Promise<Either<ReadIsolationTechniqueTypeResponse, CommunicationError>>;

  list(
    query: ListIsolationTechniqueTypeQuery
  ): Promise<Either<ListIsolationTechniqueTypeResponse, CommunicationError>>;

  create(
    command: CreateIsolationTechniqueTypeCommand
  ): Promise<Either<CreateIsolationTechniqueTypeResponse, CommunicationError>>;

  delete(
    command: DeleteIsolationTechniqueTypeCommand
  ): Promise<Either<DeleteIsolationTechniqueTypeResponse, CommunicationError>>;

  update(
    command: UpdateIsolationTechniqueTypeCommand
  ): Promise<Either<UpdateIsolationTechniqueTypeResponse, CommunicationError>>;
}

export class IsolationTechniqueTypesService
  implements iIsolationTechniqueTypesService
{
  private httpClient: iHttpClient;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
  }

  private baseUrl = "isolationtechniquetypes/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadIsolationTechniqueTypeQuery
  ): Promise<Either<ReadIsolationTechniqueTypeResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClient.get<ReadIsolationTechniqueTypeResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListIsolationTechniqueTypeQuery
  ): Promise<Either<ListIsolationTechniqueTypeResponse, CommunicationError>> {
    const response =
      await this.httpClient.get<ListIsolationTechniqueTypeResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreateIsolationTechniqueTypeCommand
  ): Promise<Either<CreateIsolationTechniqueTypeResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateIsolationTechniqueTypeCommand,
      CreateIsolationTechniqueTypeResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteIsolationTechniqueTypeCommand
  ): Promise<Either<DeleteIsolationTechniqueTypeResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeleteIsolationTechniqueTypeResponse>(url);
    return response;
  }

  async update(
    command: UpdateIsolationTechniqueTypeCommand
  ): Promise<Either<UpdateIsolationTechniqueTypeResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateIsolationTechniqueTypeCommand,
      UpdateIsolationTechniqueTypeResponse
    >(url, command);
    return response;
  }
}
