import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateSMTA2WorkflowItemCommand,
  CreateSMTA2WorkflowItemResponse,
} from "./models/CreateSMTA2WorkflowItem";
import {
  DeleteSMTA2WorkflowItemCommand,
  DeleteSMTA2WorkflowItemResponse,
} from "./models/DeleteSMTA2WorkflowItem";
import {
  ListSMTA2WorkflowItemQuery,
  ListSMTA2WorkflowItemResponse,
} from "./models/ListSMTA2WorkflowItem";
import {
  ListDashboardSMTA2WorkflowItemQuery,
  ListDashboardSMTA2WorkflowItemResponse,
} from "./models/ListDashboardSMTA2WorkflowItem";
import {
  ReadSMTA2WorkflowItemQuery,
  ReadSMTA2WorkflowItemResponse,
} from "./models/ReadSMTA2WorkflowItem";
import {
  UpdateSMTA2WorkflowItemCommand,
  UpdateSMTA2WorkflowItemResponse,
} from "./models/UpdateSMTA2WorkflowItem";

import {
  DownloadSMTA2WorkflowItemDocumentQuery,
  DownloadSMTA2WorkflowItemDocumentResponse,
} from "./models/DownloadSMTA2WorkflowItemDocument";

export interface iSMTA2WorkflowItemsService {
  read(
    query: ReadSMTA2WorkflowItemQuery
  ): Promise<Either<ReadSMTA2WorkflowItemResponse, CommunicationError>>;

  list(
    query: ListSMTA2WorkflowItemQuery
  ): Promise<Either<ListSMTA2WorkflowItemResponse, CommunicationError>>;

  listForDashboard(
    query: ListDashboardSMTA2WorkflowItemQuery
  ): Promise<
    Either<ListDashboardSMTA2WorkflowItemResponse, CommunicationError>
  >;

  create(
    command: CreateSMTA2WorkflowItemCommand
  ): Promise<Either<CreateSMTA2WorkflowItemResponse, CommunicationError>>;

  delete(
    command: DeleteSMTA2WorkflowItemCommand
  ): Promise<Either<DeleteSMTA2WorkflowItemResponse, CommunicationError>>;

  update(
    command: UpdateSMTA2WorkflowItemCommand,
    file: File | undefined
  ): Promise<Either<UpdateSMTA2WorkflowItemResponse, CommunicationError>>;

  downloadFile(
    query: DownloadSMTA2WorkflowItemDocumentQuery
  ): Promise<
    Either<DownloadSMTA2WorkflowItemDocumentResponse, CommunicationError>
  >;
}

export class SMTA2WorkflowItemsService implements iSMTA2WorkflowItemsService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "smta2workflowitems/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadSMTA2WorkflowItemQuery
  ): Promise<Either<ReadSMTA2WorkflowItemResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadSMTA2WorkflowItemResponse>(
      url
    );
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListSMTA2WorkflowItemQuery
  ): Promise<Either<ListSMTA2WorkflowItemResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListSMTA2WorkflowItemResponse>(
      this.baseUrl
    );
    return response;
  }

  async listForDashboard(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListDashboardSMTA2WorkflowItemQuery
  ): Promise<
    Either<ListDashboardSMTA2WorkflowItemResponse, CommunicationError>
  > {
    const response = await this.httpClient.get<ListSMTA2WorkflowItemResponse>(
      this.baseUrl + "/dashboard"
    );
    return response;
  }

  async create(
    command: CreateSMTA2WorkflowItemCommand
  ): Promise<Either<CreateSMTA2WorkflowItemResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateSMTA2WorkflowItemCommand,
      CreateSMTA2WorkflowItemResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteSMTA2WorkflowItemCommand
  ): Promise<Either<DeleteSMTA2WorkflowItemResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeleteSMTA2WorkflowItemResponse>(url);
    return response;
  }

  async downloadFile(
    query: DownloadSMTA2WorkflowItemDocumentQuery
  ): Promise<Either<any, CommunicationError>> {
    const url = this.buildUrl(
      query.Id + "/downloadfile/" + query.WorklistId + "/worklistid"
    );
    const response = await this.httpClient.downloadFile(url, query.Name);
    return response;
  }

  async update(
    command: UpdateSMTA2WorkflowItemCommand,
    file: File | undefined
  ): Promise<Either<UpdateSMTA2WorkflowItemResponse, CommunicationError>> {
    const formData = new FormData();
    formData.append("Command", JSON.stringify(command));
    if (file != undefined) {
      formData.append("file", file, file.name);
    }
    const response =
      await this.httpClient.patchFile<UpdateSMTA2WorkflowItemResponse>(
        this.buildUrl(command.Id),
        formData
      );

    return response;
  }
}
