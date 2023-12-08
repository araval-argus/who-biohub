import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateMaterialCommand,
  CreateMaterialResponse,
} from "./models/CreateMaterial";
import {
  DeleteMaterialCommand,
  DeleteMaterialResponse,
} from "./models/DeleteMaterial";
import { ListMaterialQuery, ListMaterialResponse } from "./models/ListMaterial";
import { ReadMaterialQuery, ReadMaterialResponse } from "./models/ReadMaterial";
import {
  UpdateMaterialCommand,
  UpdateMaterialResponse,
} from "./models/UpdateMaterial";

import {
  ListMaterialPublicQuery,
  ListMaterialPublicResponse,
} from "./models/ListMaterialPublic";
import {
  ReadMaterialPublicQuery,
  ReadMaterialPublicResponse,
} from "./models/ReadMaterialPublic";

import {
  ListMaterialsForWorklistFromBioHubItemQuery,
  ListMaterialsForWorklistFromBioHubItemResponse,
} from "./models/ListMaterialsForWorklistFromBioHubItem";

import {
  ListMaterialsForWorklistToBioHubItemQuery,
  ListMaterialsForWorklistToBioHubItemResponse,
} from "./models/ListMaterialsForWorklistToBioHubItem";

import {
  ReadMaterialForBioHubFacilityCompletionQuery,
  ReadMaterialForBioHubFacilityCompletionResponse,
} from "./models/ReadMaterialForBioHubFacilityCompletion";
import {
  ReadMaterialForLaboratoryCompletionQuery,
  ReadMaterialForLaboratoryCompletionResponse,
} from "./models/ReadMaterialForLaboratoryCompletion";

import {
  UpdateMaterialForBioHubFacilityCompletionCommand,
  UpdateMaterialForBioHubFacilityCompletionResponse,
} from "./models/UpdateMaterialForBioHubFacilityCompletion";
import {
  UpdateMaterialForLaboratoryCompletionCommand,
  UpdateMaterialForLaboratoryCompletionResponse,
} from "./models/UpdateMaterialForLaboratoryCompletion";

import {
  ListMaterialEventQuery,
  ListMaterialEventResponse,
} from "./models/ListMaterialEvent";

export interface iMaterialsService {
  read(
    query: ReadMaterialQuery
  ): Promise<Either<ReadMaterialResponse, CommunicationError>>;

  list(
    query: ListMaterialQuery
  ): Promise<Either<ListMaterialResponse, CommunicationError>>;

  create(
    command: CreateMaterialCommand
  ): Promise<Either<CreateMaterialResponse, CommunicationError>>;

  delete(
    command: DeleteMaterialCommand
  ): Promise<Either<DeleteMaterialResponse, CommunicationError>>;

  update(
    command: UpdateMaterialCommand
  ): Promise<Either<UpdateMaterialResponse, CommunicationError>>;

  readPublic(
    query: ReadMaterialPublicQuery
  ): Promise<Either<ReadMaterialPublicResponse, CommunicationError>>;

  listPublic(
    query: ListMaterialPublicQuery
  ): Promise<Either<ListMaterialPublicResponse, CommunicationError>>;

  listMaterialsForWorklistFromBioHubItem(
    query: ListMaterialsForWorklistFromBioHubItemQuery
  ): Promise<
    Either<ListMaterialsForWorklistFromBioHubItemResponse, CommunicationError>
  >;

  listMaterialsForWorklistToBioHubItem(
    query: ListMaterialsForWorklistToBioHubItemQuery
  ): Promise<
    Either<ListMaterialsForWorklistToBioHubItemResponse, CommunicationError>
  >;

  readForBioHubFacilityCompletion(
    query: ReadMaterialForBioHubFacilityCompletionQuery
  ): Promise<
    Either<ReadMaterialForBioHubFacilityCompletionResponse, CommunicationError>
  >;

  readForLaboratoryCompletion(
    query: ReadMaterialForLaboratoryCompletionQuery
  ): Promise<
    Either<ReadMaterialForLaboratoryCompletionResponse, CommunicationError>
  >;

  updateForBioHubFacilityCompletion(
    command: UpdateMaterialForBioHubFacilityCompletionCommand
  ): Promise<
    Either<
      UpdateMaterialForBioHubFacilityCompletionResponse,
      CommunicationError
    >
  >;

  updateForLaboratoryCompletion(
    command: UpdateMaterialForLaboratoryCompletionCommand
  ): Promise<
    Either<UpdateMaterialForLaboratoryCompletionResponse, CommunicationError>
  >;

  listEvents(
    query: ListMaterialEventQuery
  ): Promise<Either<ListMaterialEventResponse, CommunicationError>>;
}

export class MaterialsService implements iMaterialsService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "materials/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadMaterialQuery
  ): Promise<Either<ReadMaterialResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadMaterialResponse>(url);
    return response;
  }

  async readPublic(
    query: ReadMaterialPublicQuery
  ): Promise<Either<ReadMaterialPublicResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClientPublic.getPublic<ReadMaterialPublicResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListMaterialQuery
  ): Promise<Either<ListMaterialResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListMaterialResponse>(
      this.baseUrl
    );
    return response;
  }

  async listPublic(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListMaterialPublicQuery
  ): Promise<Either<ListMaterialPublicResponse, CommunicationError>> {
    const response =
      await this.httpClientPublic.getPublic<ListMaterialPublicResponse>(
        this.baseUrl
      );
    return response;
  }

  async create(
    command: CreateMaterialCommand
  ): Promise<Either<CreateMaterialResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateMaterialCommand,
      CreateMaterialResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteMaterialCommand
  ): Promise<Either<DeleteMaterialResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.delete<DeleteMaterialResponse>(url);
    return response;
  }

  async update(
    command: UpdateMaterialCommand
  ): Promise<Either<UpdateMaterialResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateMaterialCommand,
      UpdateMaterialResponse
    >(url, command);
    return response;
  }

  async listMaterialsForWorklistFromBioHubItem(
    query: ListMaterialsForWorklistFromBioHubItemQuery
  ): Promise<
    Either<ListMaterialsForWorklistFromBioHubItemResponse, CommunicationError>
  > {
    const url = this.buildUrl(
      query.WorklistFromBioHubItemId +
        "/worklistFromBioHubItemId/" +
        query.BioHubFacilityId +
        "/bioHubFacilityId"
    );
    const response =
      await this.httpClient.get<ListMaterialsForWorklistFromBioHubItemResponse>(
        url
      );
    return response;
  }

  async listMaterialsForWorklistToBioHubItem(
    query: ListMaterialsForWorklistToBioHubItemQuery
  ): Promise<
    Either<ListMaterialsForWorklistToBioHubItemResponse, CommunicationError>
  > {
    const url = this.buildUrl(
      query.WorklistToBioHubItemId + "/worklistToBioHubItemId"
    );
    const response =
      await this.httpClient.get<ListMaterialsForWorklistToBioHubItemResponse>(
        url
      );
    return response;
  }

  async readForBioHubFacilityCompletion(
    query: ReadMaterialForBioHubFacilityCompletionQuery
  ): Promise<
    Either<ReadMaterialForBioHubFacilityCompletionResponse, CommunicationError>
  > {
    const url = this.buildUrl(query.Id + "/biohubcompletion");
    const response =
      await this.httpClient.get<ReadMaterialForBioHubFacilityCompletionResponse>(
        url
      );
    return response;
  }

  async readForLaboratoryCompletion(
    query: ReadMaterialForLaboratoryCompletionQuery
  ): Promise<
    Either<ReadMaterialForLaboratoryCompletionResponse, CommunicationError>
  > {
    const url = this.buildUrl(query.Id + "/laboratorycompletion");
    const response =
      await this.httpClient.get<ReadMaterialForLaboratoryCompletionResponse>(
        url
      );
    return response;
  }

  async updateForBioHubFacilityCompletion(
    command: UpdateMaterialForBioHubFacilityCompletionCommand
  ): Promise<
    Either<
      UpdateMaterialForBioHubFacilityCompletionResponse,
      CommunicationError
    >
  > {
    const url = this.buildUrl(command.Id + "/biohubcompletion");
    const response = await this.httpClient.patch<
      UpdateMaterialForBioHubFacilityCompletionCommand,
      UpdateMaterialForBioHubFacilityCompletionResponse
    >(url, command);
    return response;
  }

  async updateForLaboratoryCompletion(
    command: UpdateMaterialForLaboratoryCompletionCommand
  ): Promise<
    Either<UpdateMaterialForLaboratoryCompletionResponse, CommunicationError>
  > {
    const url = this.buildUrl(command.Id + "/laboratorycompletion");
    const response = await this.httpClient.patch<
      UpdateMaterialForLaboratoryCompletionResponse,
      UpdateMaterialForBioHubFacilityCompletionCommand
    >(url, command);
    return response;
  }

  async listEvents(
    query: ListMaterialEventQuery
  ): Promise<Either<ListMaterialEventResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id + "/events");
    const response = await this.httpClient.get<ListMaterialEventResponse>(url);
    return response;
  }
}
