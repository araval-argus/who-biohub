import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateCountryCommand,
  CreateCountryResponse,
} from "./models/CreateCountry";
import {
  DeleteCountryCommand,
  DeleteCountryResponse,
} from "./models/DeleteCountry";
import { ListCountryQuery, ListCountryResponse } from "./models/ListCountry";
import { ReadCountryQuery, ReadCountryResponse } from "./models/ReadCountry";
import {
  UpdateCountryCommand,
  UpdateCountryResponse,
} from "./models/UpdateCountry";

export interface iCountriesService {
  read(
    query: ReadCountryQuery
  ): Promise<Either<ReadCountryResponse, CommunicationError>>;

  list(
    query: ListCountryQuery
  ): Promise<Either<ListCountryResponse, CommunicationError>>;

  listPublic(
    query: ListCountryQuery
  ): Promise<Either<ListCountryResponse, CommunicationError>>;

  create(
    command: CreateCountryCommand
  ): Promise<Either<CreateCountryResponse, CommunicationError>>;

  delete(
    command: DeleteCountryCommand
  ): Promise<Either<DeleteCountryResponse, CommunicationError>>;

  update(
    command: UpdateCountryCommand
  ): Promise<Either<UpdateCountryResponse, CommunicationError>>;
}

export class CountriesService implements iCountriesService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "countries/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadCountryQuery
  ): Promise<Either<ReadCountryResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadCountryResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListCountryQuery
  ): Promise<Either<ListCountryResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListCountryResponse>(
      this.baseUrl
    );
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListCountryQuery
  ): Promise<Either<ListCountryResponse, CommunicationError>> {
    const response = await this.httpClientPublic.getPublic<ListCountryResponse>(
      this.baseUrl
    );
    return response;
  }

  async create(
    command: CreateCountryCommand
  ): Promise<Either<CreateCountryResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateCountryCommand,
      CreateCountryResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteCountryCommand
  ): Promise<Either<DeleteCountryResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.delete<DeleteCountryResponse>(url);
    return response;
  }

  async update(
    command: UpdateCountryCommand
  ): Promise<Either<UpdateCountryResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateCountryCommand,
      UpdateCountryResponse
    >(url, command);
    return response;
  }
}
