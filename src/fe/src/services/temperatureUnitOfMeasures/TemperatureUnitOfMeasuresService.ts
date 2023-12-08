import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateTemperatureUnitOfMeasureCommand,
  CreateTemperatureUnitOfMeasureResponse,
} from "./models/CreateTemperatureUnitOfMeasure";
import {
  DeleteTemperatureUnitOfMeasureCommand,
  DeleteTemperatureUnitOfMeasureResponse,
} from "./models/DeleteTemperatureUnitOfMeasure";
import {
  ListTemperatureUnitOfMeasureQuery,
  ListTemperatureUnitOfMeasureResponse,
} from "./models/ListTemperatureUnitOfMeasure";
import {
  ReadTemperatureUnitOfMeasureQuery,
  ReadTemperatureUnitOfMeasureResponse,
} from "./models/ReadTemperatureUnitOfMeasure";
import {
  UpdateTemperatureUnitOfMeasureCommand,
  UpdateTemperatureUnitOfMeasureResponse,
} from "./models/UpdateTemperatureUnitOfMeasure";

export interface iTemperatureUnitOfMeasuresService {
  read(
    query: ReadTemperatureUnitOfMeasureQuery
  ): Promise<Either<ReadTemperatureUnitOfMeasureResponse, CommunicationError>>;

  list(
    query: ListTemperatureUnitOfMeasureQuery
  ): Promise<Either<ListTemperatureUnitOfMeasureResponse, CommunicationError>>;

  listPublic(
    query: ListTemperatureUnitOfMeasureQuery
  ): Promise<Either<ListTemperatureUnitOfMeasureResponse, CommunicationError>>;

  create(
    command: CreateTemperatureUnitOfMeasureCommand
  ): Promise<
    Either<CreateTemperatureUnitOfMeasureResponse, CommunicationError>
  >;

  delete(
    command: DeleteTemperatureUnitOfMeasureCommand
  ): Promise<
    Either<DeleteTemperatureUnitOfMeasureResponse, CommunicationError>
  >;

  update(
    command: UpdateTemperatureUnitOfMeasureCommand
  ): Promise<
    Either<UpdateTemperatureUnitOfMeasureResponse, CommunicationError>
  >;
}

export class TemperatureUnitOfMeasuresService
  implements iTemperatureUnitOfMeasuresService
{
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "temperatureunitofmeasures/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadTemperatureUnitOfMeasureQuery
  ): Promise<Either<ReadTemperatureUnitOfMeasureResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClient.get<ReadTemperatureUnitOfMeasureResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListTemperatureUnitOfMeasureQuery
  ): Promise<Either<ListTemperatureUnitOfMeasureResponse, CommunicationError>> {
    const response =
      await this.httpClient.get<ListTemperatureUnitOfMeasureResponse>(
        this.baseUrl
      );
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListTemperatureUnitOfMeasureQuery
  ): Promise<Either<ListTemperatureUnitOfMeasureResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListTemperatureUnitOfMeasureResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreateTemperatureUnitOfMeasureCommand
  ): Promise<
    Either<CreateTemperatureUnitOfMeasureResponse, CommunicationError>
  > {
    const response = await this.httpClient.post<
      CreateTemperatureUnitOfMeasureCommand,
      CreateTemperatureUnitOfMeasureResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteTemperatureUnitOfMeasureCommand
  ): Promise<
    Either<DeleteTemperatureUnitOfMeasureResponse, CommunicationError>
  > {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeleteTemperatureUnitOfMeasureResponse>(url);
    return response;
  }

  async update(
    command: UpdateTemperatureUnitOfMeasureCommand
  ): Promise<
    Either<UpdateTemperatureUnitOfMeasureResponse, CommunicationError>
  > {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateTemperatureUnitOfMeasureCommand,
      UpdateTemperatureUnitOfMeasureResponse
    >(url, command);
    return response;
  }
}
