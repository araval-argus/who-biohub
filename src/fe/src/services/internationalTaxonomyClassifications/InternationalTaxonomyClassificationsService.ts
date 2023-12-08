import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateInternationalTaxonomyClassificationCommand,
  CreateInternationalTaxonomyClassificationResponse,
} from "./models/CreateInternationalTaxonomyClassification";
import {
  DeleteInternationalTaxonomyClassificationCommand,
  DeleteInternationalTaxonomyClassificationResponse,
} from "./models/DeleteInternationalTaxonomyClassification";
import {
  ListInternationalTaxonomyClassificationQuery,
  ListInternationalTaxonomyClassificationResponse,
} from "./models/ListInternationalTaxonomyClassification";
import {
  ReadInternationalTaxonomyClassificationQuery,
  ReadInternationalTaxonomyClassificationResponse,
} from "./models/ReadInternationalTaxonomyClassification";
import {
  UpdateInternationalTaxonomyClassificationCommand,
  UpdateInternationalTaxonomyClassificationResponse,
} from "./models/UpdateInternationalTaxonomyClassification";

export interface iInternationalTaxonomyClassificationsService {
  read(
    query: ReadInternationalTaxonomyClassificationQuery
  ): Promise<
    Either<ReadInternationalTaxonomyClassificationResponse, CommunicationError>
  >;

  list(
    query: ListInternationalTaxonomyClassificationQuery
  ): Promise<
    Either<ListInternationalTaxonomyClassificationResponse, CommunicationError>
  >;

  listPublic(
    query: ListInternationalTaxonomyClassificationQuery
  ): Promise<
    Either<ListInternationalTaxonomyClassificationResponse, CommunicationError>
  >;

  create(
    command: CreateInternationalTaxonomyClassificationCommand
  ): Promise<
    Either<
      CreateInternationalTaxonomyClassificationResponse,
      CommunicationError
    >
  >;

  delete(
    command: DeleteInternationalTaxonomyClassificationCommand
  ): Promise<
    Either<
      DeleteInternationalTaxonomyClassificationResponse,
      CommunicationError
    >
  >;

  update(
    command: UpdateInternationalTaxonomyClassificationCommand
  ): Promise<
    Either<
      UpdateInternationalTaxonomyClassificationResponse,
      CommunicationError
    >
  >;
}

export class InternationalTaxonomyClassificationsService
  implements iInternationalTaxonomyClassificationsService
{
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "internationaltaxonomyclassifications/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadInternationalTaxonomyClassificationQuery
  ): Promise<
    Either<ReadInternationalTaxonomyClassificationResponse, CommunicationError>
  > {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClient.get<ReadInternationalTaxonomyClassificationResponse>(
        url
      );
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListInternationalTaxonomyClassificationQuery
  ): Promise<
    Either<ListInternationalTaxonomyClassificationResponse, CommunicationError>
  > {
    const response =
      await this.httpClient.get<ListInternationalTaxonomyClassificationResponse>(
        this.baseUrl
      );
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListInternationalTaxonomyClassificationQuery
  ): Promise<
    Either<ListInternationalTaxonomyClassificationResponse, CommunicationError>
  > {
    const response =
      await this.httpClientPublic.getPublic<ListInternationalTaxonomyClassificationResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreateInternationalTaxonomyClassificationCommand
  ): Promise<
    Either<
      CreateInternationalTaxonomyClassificationResponse,
      CommunicationError
    >
  > {
    const response = await this.httpClient.post<
      CreateInternationalTaxonomyClassificationCommand,
      CreateInternationalTaxonomyClassificationResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteInternationalTaxonomyClassificationCommand
  ): Promise<
    Either<
      DeleteInternationalTaxonomyClassificationResponse,
      CommunicationError
    >
  > {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeleteInternationalTaxonomyClassificationResponse>(
        url
      );
    return response;
  }

  async update(
    command: UpdateInternationalTaxonomyClassificationCommand
  ): Promise<
    Either<
      UpdateInternationalTaxonomyClassificationResponse,
      CommunicationError
    >
  > {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateInternationalTaxonomyClassificationCommand,
      UpdateInternationalTaxonomyClassificationResponse
    >(url, command);
    return response;
  }
}
