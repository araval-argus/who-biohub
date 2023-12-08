import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
} from "../shared/HttpClient";
import {
  CreateCultivabilityTypeCommand,
  CreateCultivabilityTypeResponse,
} from "./models/CreateCultivabilityType";
import {
  DeleteCultivabilityTypeCommand,
  DeleteCultivabilityTypeResponse,
} from "./models/DeleteCultivabilityType";
import {
  ListCultivabilityTypeQuery,
  ListCultivabilityTypeResponse,
} from "./models/ListCultivabilityType";
import {
  ReadCultivabilityTypeQuery,
  ReadCultivabilityTypeResponse,
} from "./models/ReadCultivabilityType";
import {
  UpdateCultivabilityTypeCommand,
  UpdateCultivabilityTypeResponse,
} from "./models/UpdateCultivabilityType";

export interface iCultivabilityTypesService {
  read(
    query: ReadCultivabilityTypeQuery
  ): Promise<Either<ReadCultivabilityTypeResponse, CommunicationError>>;

  list(
    query: ListCultivabilityTypeQuery
  ): Promise<Either<ListCultivabilityTypeResponse, CommunicationError>>;

  create(
    command: CreateCultivabilityTypeCommand
  ): Promise<Either<CreateCultivabilityTypeResponse, CommunicationError>>;

  delete(
    command: DeleteCultivabilityTypeCommand
  ): Promise<Either<DeleteCultivabilityTypeResponse, CommunicationError>>;

  update(
    command: UpdateCultivabilityTypeCommand
  ): Promise<Either<UpdateCultivabilityTypeResponse, CommunicationError>>;
}

export class CultivabilityTypesService implements iCultivabilityTypesService {
  private httpClient: iHttpClient;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
  }

  private baseUrl = "cultivabilitytypes/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadCultivabilityTypeQuery
  ): Promise<Either<ReadCultivabilityTypeResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadCultivabilityTypeResponse>(
      url
    );
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListCultivabilityTypeQuery
  ): Promise<Either<ListCultivabilityTypeResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListCultivabilityTypeResponse>(
      this.baseUrl
    );
    return response;
  }

  async create(
    command: CreateCultivabilityTypeCommand
  ): Promise<Either<CreateCultivabilityTypeResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateCultivabilityTypeCommand,
      CreateCultivabilityTypeResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteCultivabilityTypeCommand
  ): Promise<Either<DeleteCultivabilityTypeResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeleteCultivabilityTypeResponse>(url);
    return response;
  }

  async update(
    command: UpdateCultivabilityTypeCommand
  ): Promise<Either<UpdateCultivabilityTypeResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateCultivabilityTypeCommand,
      UpdateCultivabilityTypeResponse
    >(url, command);
    return response;
  }
}
