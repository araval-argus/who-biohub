import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateWorklistToBioHubItemCommand,
  CreateWorklistToBioHubItemResponse,
} from "./models/CreateWorklistToBioHubItem";
import {
  DeleteWorklistToBioHubItemCommand,
  DeleteWorklistToBioHubItemResponse,
} from "./models/DeleteWorklistToBioHubItem";
import {
  ListWorklistToBioHubItemQuery,
  ListWorklistToBioHubItemResponse,
} from "./models/ListWorklistToBioHubItem";
import {
  ListDashboardWorklistToBioHubItemQuery,
  ListDashboardWorklistToBioHubItemResponse,
} from "./models/ListDashboardWorklistToBioHubItem";
import {
  ReadWorklistToBioHubItemQuery,
  ReadWorklistToBioHubItemResponse,
} from "./models/ReadWorklistToBioHubItem";
import {
  UpdateWorklistToBioHubItemCommand,
  UpdateWorklistToBioHubItemResponse,
} from "./models/UpdateWorklistToBioHubItem";
import {
  UpdateWorklistToBioHubItemShipmentDocumentsCommand,
  UpdateWorklistToBioHubItemShipmentDocumentsResponse,
} from "./models/UpdateWorklistToBioHubItemShipmentDocuments";

import {
  DownloadWorklistToBioHubItemDocumentQuery,
  DownloadWorklistToBioHubItemDocumentResponse,
} from "./models/DownloadWorklistToBioHubItemDocument";

export interface iWorklistToBioHubItemsService {
  read(
    query: ReadWorklistToBioHubItemQuery
  ): Promise<Either<ReadWorklistToBioHubItemResponse, CommunicationError>>;

  list(
    query: ListWorklistToBioHubItemQuery
  ): Promise<Either<ListWorklistToBioHubItemResponse, CommunicationError>>;

  listForDashboard(
    query: ListDashboardWorklistToBioHubItemQuery
  ): Promise<
    Either<ListDashboardWorklistToBioHubItemResponse, CommunicationError>
  >;

  create(
    command: CreateWorklistToBioHubItemCommand
  ): Promise<Either<CreateWorklistToBioHubItemResponse, CommunicationError>>;

  delete(
    command: DeleteWorklistToBioHubItemCommand
  ): Promise<Either<DeleteWorklistToBioHubItemResponse, CommunicationError>>;

  update(
    command: UpdateWorklistToBioHubItemCommand,
    file: File | undefined
  ): Promise<Either<UpdateWorklistToBioHubItemResponse, CommunicationError>>;

  updateShipmentDocuments(
    command: UpdateWorklistToBioHubItemShipmentDocumentsCommand,
    file: File | undefined
  ): Promise<
    Either<
      UpdateWorklistToBioHubItemShipmentDocumentsResponse,
      CommunicationError
    >
  >;

  downloadFile(
    query: DownloadWorklistToBioHubItemDocumentQuery
  ): Promise<
    Either<DownloadWorklistToBioHubItemDocumentResponse, CommunicationError>
  >;
}

export class WorklistToBioHubItemsService
  implements iWorklistToBioHubItemsService
{
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "worklisttobiohubitems/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadWorklistToBioHubItemQuery
  ): Promise<Either<ReadWorklistToBioHubItemResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClient.get<ReadWorklistToBioHubItemResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListWorklistToBioHubItemQuery
  ): Promise<Either<ListWorklistToBioHubItemResponse, CommunicationError>> {
    const response =
      await this.httpClient.get<ListWorklistToBioHubItemResponse>(this.baseUrl);
    return response;
  }

  async listForDashboard(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListDashboardWorklistToBioHubItemQuery
  ): Promise<
    Either<ListDashboardWorklistToBioHubItemResponse, CommunicationError>
  > {
    const response =
      await this.httpClient.get<ListWorklistToBioHubItemResponse>(
        this.baseUrl + "/dashboard"
      );
    return response;
  }

  async create(
    command: CreateWorklistToBioHubItemCommand
  ): Promise<Either<CreateWorklistToBioHubItemResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateWorklistToBioHubItemCommand,
      CreateWorklistToBioHubItemResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteWorklistToBioHubItemCommand
  ): Promise<Either<DeleteWorklistToBioHubItemResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeleteWorklistToBioHubItemResponse>(url);
    return response;
  }

  async downloadFile(
    query: DownloadWorklistToBioHubItemDocumentQuery
  ): Promise<Either<any, CommunicationError>> {
    const url = this.buildUrl(
      query.Id + "/downloadfile/" + query.WorklistId + "/worklistid"
    );
    const response = await this.httpClient.downloadFile(url, query.Name);
    return response;
  }

  async update(
    command: UpdateWorklistToBioHubItemCommand,
    file: File | undefined
  ): Promise<Either<UpdateWorklistToBioHubItemResponse, CommunicationError>> {
    const formData = new FormData();
    formData.append("Command", JSON.stringify(command));
    if (file != undefined) {
      formData.append("file", file, file.name);
    }
    const response =
      await this.httpClient.patchFile<UpdateWorklistToBioHubItemResponse>(
        this.buildUrl(command.Id),
        formData
      );

    return response;
  }

  async updateShipmentDocuments(
    command: UpdateWorklistToBioHubItemShipmentDocumentsCommand,
    file: File | undefined
  ): Promise<
    Either<
      UpdateWorklistToBioHubItemShipmentDocumentsResponse,
      CommunicationError
    >
  > {
    const formData = new FormData();
    formData.append("Command", JSON.stringify(command));
    if (file != undefined) {
      formData.append("file", file, file.name);
    }
    const response =
      await this.httpClient.patchFile<UpdateWorklistToBioHubItemShipmentDocumentsResponse>(
        this.buildUrl(command.Id + "/shipmentdocuments"),
        formData
      );

    return response;
  }
}
