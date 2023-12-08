import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateWorklistFromBioHubItemCommand,
  CreateWorklistFromBioHubItemResponse,
} from "./models/CreateWorklistFromBioHubItem";
import {
  DeleteWorklistFromBioHubItemCommand,
  DeleteWorklistFromBioHubItemResponse,
} from "./models/DeleteWorklistFromBioHubItem";
import {
  ListWorklistFromBioHubItemQuery,
  ListWorklistFromBioHubItemResponse,
} from "./models/ListWorklistFromBioHubItem";
import {
  ListDashboardWorklistFromBioHubItemQuery,
  ListDashboardWorklistFromBioHubItemResponse,
} from "./models/ListDashboardWorklistFromBioHubItem";
import {
  ReadWorklistFromBioHubItemQuery,
  ReadWorklistFromBioHubItemResponse,
} from "./models/ReadWorklistFromBioHubItem";
import {
  UpdateWorklistFromBioHubItemCommand,
  UpdateWorklistFromBioHubItemResponse,
} from "./models/UpdateWorklistFromBioHubItem";

import {
  UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommand,
  UpdateWorklistFromBioHubItemBHFShipmentDocumentsResponse,
} from "./models/UpdateWorklistFromBioHubItemBHFShipmentDocuments";

import {
  UpdateWorklistFromBioHubItemQEShipmentDocumentsCommand,
  UpdateWorklistFromBioHubItemQEShipmentDocumentsResponse,
} from "./models/UpdateWorklistFromBioHubItemQEShipmentDocuments";

import {
  DownloadWorklistFromBioHubItemDocumentQuery,
  DownloadWorklistFromBioHubItemDocumentResponse,
} from "./models/DownloadWorklistFromBioHubItemDocument";

export interface iWorklistFromBioHubItemsService {
  read(
    query: ReadWorklistFromBioHubItemQuery
  ): Promise<Either<ReadWorklistFromBioHubItemResponse, CommunicationError>>;

  list(
    query: ListWorklistFromBioHubItemQuery
  ): Promise<Either<ListWorklistFromBioHubItemResponse, CommunicationError>>;

  listForDashboard(
    query: ListDashboardWorklistFromBioHubItemQuery
  ): Promise<
    Either<ListDashboardWorklistFromBioHubItemResponse, CommunicationError>
  >;

  create(
    command: CreateWorklistFromBioHubItemCommand
  ): Promise<Either<CreateWorklistFromBioHubItemResponse, CommunicationError>>;

  delete(
    command: DeleteWorklistFromBioHubItemCommand
  ): Promise<Either<DeleteWorklistFromBioHubItemResponse, CommunicationError>>;

  update(
    command: UpdateWorklistFromBioHubItemCommand,
    file: File | undefined
  ): Promise<Either<UpdateWorklistFromBioHubItemResponse, CommunicationError>>;

  updateBHFShipmentDocuments(
    command: UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommand,
    file: File | undefined
  ): Promise<
    Either<
      UpdateWorklistFromBioHubItemBHFShipmentDocumentsResponse,
      CommunicationError
    >
  >;

  updateQEShipmentDocuments(
    command: UpdateWorklistFromBioHubItemQEShipmentDocumentsCommand,
    file: File | undefined
  ): Promise<
    Either<
      UpdateWorklistFromBioHubItemQEShipmentDocumentsResponse,
      CommunicationError
    >
  >;

  downloadFile(
    query: DownloadWorklistFromBioHubItemDocumentQuery
  ): Promise<
    Either<DownloadWorklistFromBioHubItemDocumentResponse, CommunicationError>
  >;
}

export class WorklistFromBioHubItemsService
  implements iWorklistFromBioHubItemsService
{
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "worklistfrombiohubitems/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadWorklistFromBioHubItemQuery
  ): Promise<Either<ReadWorklistFromBioHubItemResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClient.get<ReadWorklistFromBioHubItemResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListWorklistFromBioHubItemQuery
  ): Promise<Either<ListWorklistFromBioHubItemResponse, CommunicationError>> {
    const response =
      await this.httpClient.get<ListWorklistFromBioHubItemResponse>(
        this.baseUrl
      );
    return response;
  }

  async listForDashboard(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListDashboardWorklistFromBioHubItemQuery
  ): Promise<
    Either<ListDashboardWorklistFromBioHubItemResponse, CommunicationError>
  > {
    const response =
      await this.httpClient.get<ListDashboardWorklistFromBioHubItemResponse>(
        this.baseUrl + "/dashboard"
      );
    return response;
  }

  async create(
    command: CreateWorklistFromBioHubItemCommand
  ): Promise<Either<CreateWorklistFromBioHubItemResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateWorklistFromBioHubItemCommand,
      CreateWorklistFromBioHubItemResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteWorklistFromBioHubItemCommand
  ): Promise<Either<DeleteWorklistFromBioHubItemResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeleteWorklistFromBioHubItemResponse>(url);
    return response;
  }

  async downloadFile(
    query: DownloadWorklistFromBioHubItemDocumentQuery
  ): Promise<Either<any, CommunicationError>> {
    const url = this.buildUrl(
      query.Id + "/downloadfile/" + query.WorklistId + "/worklistid"
    );
    const response = await this.httpClient.downloadFile(url, query.Name);
    return response;
  }

  async update(
    command: UpdateWorklistFromBioHubItemCommand,
    file: File | undefined
  ): Promise<Either<UpdateWorklistFromBioHubItemResponse, CommunicationError>> {
    const formData = new FormData();
    formData.append("Command", JSON.stringify(command));
    if (file != undefined) {
      formData.append("file", file, file.name);
    }
    const response =
      await this.httpClient.patchFile<UpdateWorklistFromBioHubItemResponse>(
        this.buildUrl(command.Id),
        formData
      );

    return response;
  }

  async updateBHFShipmentDocuments(
    command: UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommand,
    file: File | undefined
  ): Promise<
    Either<
      UpdateWorklistFromBioHubItemBHFShipmentDocumentsResponse,
      CommunicationError
    >
  > {
    const formData = new FormData();
    formData.append("Command", JSON.stringify(command));
    if (file != undefined) {
      formData.append("file", file, file.name);
    }
    const response =
      await this.httpClient.patchFile<UpdateWorklistFromBioHubItemBHFShipmentDocumentsResponse>(
        this.buildUrl(command.Id + "/bhfshipmentdocuments"),
        formData
      );

    return response;
  }

  async updateQEShipmentDocuments(
    command: UpdateWorklistFromBioHubItemQEShipmentDocumentsCommand,
    file: File | undefined
  ): Promise<
    Either<
      UpdateWorklistFromBioHubItemQEShipmentDocumentsResponse,
      CommunicationError
    >
  > {
    const formData = new FormData();
    formData.append("Command", JSON.stringify(command));
    if (file != undefined) {
      formData.append("file", file, file.name);
    }
    const response =
      await this.httpClient.patchFile<UpdateWorklistFromBioHubItemQEShipmentDocumentsResponse>(
        this.buildUrl(command.Id + "/qeshipmentdocuments"),
        formData
      );

    return response;
  }
}
