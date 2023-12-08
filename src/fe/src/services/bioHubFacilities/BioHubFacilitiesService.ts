import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateBioHubFacilityCommand,
  CreateBioHubFacilityResponse,
} from "./models/CreateBioHubFacility";
import {
  DeleteBioHubFacilityCommand,
  DeleteBioHubFacilityResponse,
} from "./models/DeleteBioHubFacility";
import {
  ListBioHubFacilityQuery,
  ListBioHubFacilityResponse,
} from "./models/ListBioHubFacility";
import {
  ListBioHubFacilityMapQuery,
  ListBioHubFacilityMapResponse,
} from "./models/ListBioHubFacilityMap";
import {
  ReadBioHubFacilityQuery,
  ReadBioHubFacilityResponse,
} from "./models/ReadBioHubFacility";
import {
  UpdateBioHubFacilityCommand,
  UpdateBioHubFacilityResponse,
} from "./models/UpdateBioHubFacility";

import {
  ListBioHubFacilityPublicQuery,
  ListBioHubFacilityPublicResponse,
} from "./models/ListBioHubFacilityPublic";
import {
  ListBioHubFacilityMapPublicQuery,
  ListBioHubFacilityMapPublicResponse,
} from "./models/ListBioHubFacilityMapPublic";
import {
  ReadBioHubFacilityPublicQuery,
  ReadBioHubFacilityPublicResponse,
} from "./models/ReadBioHubFacilityPublic";

export interface iBioHubFacilitiesService {
  read(
    query: ReadBioHubFacilityQuery
  ): Promise<Either<ReadBioHubFacilityResponse, CommunicationError>>;

  list(
    query: ListBioHubFacilityQuery
  ): Promise<Either<ListBioHubFacilityResponse, CommunicationError>>;

  listMap(
    query: ListBioHubFacilityMapQuery
  ): Promise<Either<ListBioHubFacilityMapResponse, CommunicationError>>;

  readPublic(
    query: ReadBioHubFacilityPublicQuery
  ): Promise<Either<ReadBioHubFacilityPublicResponse, CommunicationError>>;

  listPublic(
    query: ListBioHubFacilityPublicQuery
  ): Promise<Either<ListBioHubFacilityPublicResponse, CommunicationError>>;

  listMapPublic(
    query: ListBioHubFacilityMapPublicQuery
  ): Promise<Either<ListBioHubFacilityMapPublicResponse, CommunicationError>>;

  create(
    command: CreateBioHubFacilityCommand
  ): Promise<Either<CreateBioHubFacilityResponse, CommunicationError>>;

  delete(
    command: DeleteBioHubFacilityCommand
  ): Promise<Either<DeleteBioHubFacilityResponse, CommunicationError>>;

  update(
    command: UpdateBioHubFacilityCommand
  ): Promise<Either<UpdateBioHubFacilityResponse, CommunicationError>>;
}

export class BioHubFacilitiesService implements iBioHubFacilitiesService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "biohubfacilities/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadBioHubFacilityQuery
  ): Promise<Either<ReadBioHubFacilityResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadBioHubFacilityResponse>(url);
    return response;
  }

  async readPublic(
    query: ReadBioHubFacilityPublicQuery
  ): Promise<Either<ReadBioHubFacilityPublicResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClientPublic.getPublic<ReadBioHubFacilityPublicResponse>(
        url
      );
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListBioHubFacilityQuery
  ): Promise<Either<ListBioHubFacilityResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListBioHubFacilityResponse>(
      this.baseUrl
    );
    return response;
  }

  async listMap(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListBioHubFacilityMapQuery
  ): Promise<Either<ListBioHubFacilityMapResponse, CommunicationError>> {
    const url = this.buildUrl("map");
    const response = await this.httpClient.get<ListBioHubFacilityMapResponse>(
      url
    );
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListBioHubFacilityPublicQuery
  ): Promise<Either<ListBioHubFacilityPublicResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListBioHubFacilityPublicResponse>(
        this.baseUrl
      );
    return response;
  }

  async listMapPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListBioHubFacilityMapPublicQuery
  ): Promise<Either<ListBioHubFacilityMapPublicResponse, CommunicationError>> {
    const url = this.buildUrl("map");
    const response =
      await this.httpClientPublic.getPublic<ListBioHubFacilityMapPublicResponse>(
        url
      );
    return response;
  }

  async create(
    command: CreateBioHubFacilityCommand
  ): Promise<Either<CreateBioHubFacilityResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateBioHubFacilityCommand,
      CreateBioHubFacilityResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteBioHubFacilityCommand
  ): Promise<Either<DeleteBioHubFacilityResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.delete<DeleteBioHubFacilityResponse>(
      url
    );
    return response;
  }

  async update(
    command: UpdateBioHubFacilityCommand
  ): Promise<Either<UpdateBioHubFacilityResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateBioHubFacilityCommand,
      UpdateBioHubFacilityResponse
    >(url, command);
    return response;
  }
}
