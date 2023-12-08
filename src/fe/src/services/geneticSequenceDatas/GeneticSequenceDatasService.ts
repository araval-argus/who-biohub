import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateGeneticSequenceDataCommand,
  CreateGeneticSequenceDataResponse,
} from "./models/CreateGeneticSequenceData";
import {
  DeleteGeneticSequenceDataCommand,
  DeleteGeneticSequenceDataResponse,
} from "./models/DeleteGeneticSequenceData";
import {
  ListGeneticSequenceDataQuery,
  ListGeneticSequenceDataResponse,
} from "./models/ListGeneticSequenceData";
import {
  ReadGeneticSequenceDataQuery,
  ReadGeneticSequenceDataResponse,
} from "./models/ReadGeneticSequenceData";
import {
  UpdateGeneticSequenceDataCommand,
  UpdateGeneticSequenceDataResponse,
} from "./models/UpdateGeneticSequenceData";

export interface iGeneticSequenceDatasService {
  read(
    query: ReadGeneticSequenceDataQuery
  ): Promise<Either<ReadGeneticSequenceDataResponse, CommunicationError>>;

  list(
    query: ListGeneticSequenceDataQuery
  ): Promise<Either<ListGeneticSequenceDataResponse, CommunicationError>>;

  listPublic(
    query: ListGeneticSequenceDataQuery
  ): Promise<Either<ListGeneticSequenceDataResponse, CommunicationError>>;

  create(
    command: CreateGeneticSequenceDataCommand
  ): Promise<Either<CreateGeneticSequenceDataResponse, CommunicationError>>;

  delete(
    command: DeleteGeneticSequenceDataCommand
  ): Promise<Either<DeleteGeneticSequenceDataResponse, CommunicationError>>;

  update(
    command: UpdateGeneticSequenceDataCommand
  ): Promise<Either<UpdateGeneticSequenceDataResponse, CommunicationError>>;
}

export class GeneticSequenceDatasService
  implements iGeneticSequenceDatasService
{
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "geneticsequencedatas/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadGeneticSequenceDataQuery
  ): Promise<Either<ReadGeneticSequenceDataResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadGeneticSequenceDataResponse>(
      url
    );
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListGeneticSequenceDataQuery
  ): Promise<Either<ListGeneticSequenceDataResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListGeneticSequenceDataResponse>(
      this.baseUrl
    );
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListGeneticSequenceDataQuery
  ): Promise<Either<ListGeneticSequenceDataResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListGeneticSequenceDataResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreateGeneticSequenceDataCommand
  ): Promise<Either<CreateGeneticSequenceDataResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateGeneticSequenceDataCommand,
      CreateGeneticSequenceDataResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteGeneticSequenceDataCommand
  ): Promise<Either<DeleteGeneticSequenceDataResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeleteGeneticSequenceDataResponse>(url);
    return response;
  }

  async update(
    command: UpdateGeneticSequenceDataCommand
  ): Promise<Either<UpdateGeneticSequenceDataResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateGeneticSequenceDataCommand,
      UpdateGeneticSequenceDataResponse
    >(url, command);
    return response;
  }
}
