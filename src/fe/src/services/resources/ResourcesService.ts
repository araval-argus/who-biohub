import { Either, isLeft, isRight, Right } from "@/utils/either";
import { SeedData } from "@/models/constants/SeedData";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  UploadResourceFileTokenQuery,
  UploadResourceFileTokenQueryResponse,
} from "./models/UploadResourceFileToken";
import {
  CreateResourceCommand,
  CreateResourceResponse,
} from "./models/CreateResource";
import {
  CreateFolderCommand,
  CreateFolderResponse,
} from "./models/CreateFolder";
// import {
//   DeleteResourceFileTokenQuery,
//   DeleteResourceFileTokenQueryResponse,
// } from "./models/DeleteResourceFileToken";
import {
  DeleteResourceCommand,
  DeleteResourceResponse,
} from "./models/DeleteResource";
import {
  ListResourcesQuery,
  ListResourcesResponse,
} from "./models/ListResources";
import {
  ReadResourceFileTokenQuery,
  ReadResourceFileTokenResponse,
} from "./models/ReadResourceFileToken";

import {
  UpdateResourceCommand,
  UpdateResourceResponse,
} from "./models/UpdateResource";

import {
  ListResourcesPublicQuery,
  ListResourcesPublicResponse,
} from "./models/ListResourcesPublic";
import {
  ReadResourceFileTokenPublicQuery,
  ReadResourceFileTokenPublicResponse,
} from "./models/ReadResourceFileTokenPublic";

export interface iResourcesService {
  downloadResource(
    query: ReadResourceFileTokenQuery
  ): Promise<Either<ReadResourceFileTokenResponse, CommunicationError>>;

  list(
    query: ListResourcesQuery
  ): Promise<Either<ListResourcesResponse, CommunicationError>>;

  uploadResource(
    command: UploadResourceFileTokenQuery,
    file: File
  ): Promise<Either<UploadResourceFileTokenQueryResponse, CommunicationError>>;

  create(
    command: CreateResourceCommand
  ): Promise<Either<CreateResourceResponse, CommunicationError>>;

  createFolder(
    command: CreateFolderCommand
  ): Promise<Either<CreateFolderResponse, CommunicationError>>;

  // deleteResourceFileToken(
  //   query: DeleteResourceFileTokenQuery
  // ): Promise<Either<DeleteResourceFileTokenQueryResponse, CommunicationError>>;

  delete(
    command: DeleteResourceCommand
  ): Promise<Either<DeleteResourceResponse, CommunicationError>>;

  update(
    command: UpdateResourceCommand
  ): Promise<Either<UpdateResourceResponse, CommunicationError>>;

  listPublic(
    query: ListResourcesPublicQuery
  ): Promise<Either<ListResourcesPublicResponse, CommunicationError>>;

  downloadResourcePublic(
    query: ReadResourceFileTokenPublicQuery
  ): Promise<Either<ReadResourceFileTokenPublicResponse, CommunicationError>>;
}

export class ResourcesService implements iResourcesService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;

  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "resources/";
  private buildUrl(suffix: string | undefined): string {
    if (suffix) return `${this.baseUrl}${suffix}`;
    else return this.baseUrl;
  }

  async downloadResource(
    query: ReadResourceFileTokenQuery
  ): Promise<Either<any, CommunicationError>> {
    const url = this.buildUrl(query.Id + "/readfiletoken");

    const fileToken = await this.httpClient.get<ReadResourceFileTokenResponse>(
      url
    );

    if (isLeft(fileToken)) {
      const storageBaseUrl = process.env?.VUE_APP_URL_DOC;

      const storageUrl = storageBaseUrl + fileToken.value.FileToken;

      const response = await this.httpClient.downloadFileFromStorage(
        storageUrl,
        fileToken.value.FileCompleteName
      );
      if (isRight(response)) {
        return new Right(response.value);
      }
    }

    return fileToken;
  }

  async list(
    query: ListResourcesQuery
  ): Promise<Either<ListResourcesResponse, CommunicationError>> {
    const url = this.buildUrl(
      "readfolder/" + query.Id ?? SeedData.ResourceRootFolderId
    );
    const response = await this.httpClient.get<ListResourcesResponse>(url);
    return response;
  }

  async uploadResource(
    query: UploadResourceFileTokenQuery,
    file: File
  ): Promise<Either<UploadResourceFileTokenQueryResponse, CommunicationError>> {
    const url = this.buildUrl("uploadfiletoken");
    const fileToken = await this.httpClient.post<
      UploadResourceFileTokenQuery,
      UploadResourceFileTokenQueryResponse
    >(url, query);

    if (isLeft(fileToken)) {
      const storageBaseUrl = process.env?.VUE_APP_URL_DOC;

      const storageUrl = storageBaseUrl + fileToken.value.FileToken;

      const response = await this.httpClient.uploadFileToStorage(
        storageUrl,
        file
      );
      if (isRight(response)) {
        return new Right(response.value);
      }
    }
    return fileToken;
  }

  async create(
    command: CreateResourceCommand
  ): Promise<Either<CreateResourceResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateResourceCommand,
      CreateResourceResponse
    >(this.baseUrl, command);
    return response;
  }

  async createFolder(
    command: CreateFolderCommand
  ): Promise<Either<CreateFolderResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateFolderCommand,
      CreateFolderResponse
    >(this.baseUrl + "createfolder", command);

    return response;
  }

  async delete(
    command: DeleteResourceCommand
  ): Promise<Either<DeleteResourceResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.delete<DeleteResourceResponse>(url);
    return response;
  }

  async update(
    command: UpdateResourceCommand
  ): Promise<Either<UpdateResourceResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateResourceCommand,
      UpdateResourceResponse
    >(url, command);
    return response;
  }

  async downloadResourcePublic(
    query: ReadResourceFileTokenPublicQuery
  ): Promise<Either<any, CommunicationError>> {
    const url = this.buildUrl(query.Id + "/readfiletoken");

    const fileToken =
      await this.httpClientPublic.getPublic<ReadResourceFileTokenPublicResponse>(
        url
      );
    if (isLeft(fileToken)) {
      const storageBaseUrl = process.env?.VUE_APP_URL_DOC;
      const storageUrl = storageBaseUrl + fileToken.value.FileToken;

      const response =
        await this.httpClientPublic.downloadFileFromStoragePublic(
          storageUrl,
          fileToken.value.FileCompleteName
        );
      if (isRight(response)) {
        return new Right(response.value);
      }
    }

    return fileToken;
  }

  async listPublic(
    query: ListResourcesPublicQuery
  ): Promise<Either<ListResourcesPublicResponse, CommunicationError>> {
    const url = this.buildUrl(
      "readfolder/" + query.Id ?? SeedData.ResourceRootFolderId
    );
    const response =
      await this.httpClientPublic.getPublic<ListResourcesPublicResponse>(url);
    return response;
  }
}
