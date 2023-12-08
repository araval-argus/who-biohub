import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateSMTA1WorkflowItemCommand,
  CreateSMTA1WorkflowItemResponse,
} from "./models/CreateSMTA1WorkflowItem";
import {
  DeleteSMTA1WorkflowItemCommand,
  DeleteSMTA1WorkflowItemResponse,
} from "./models/DeleteSMTA1WorkflowItem";
import {
  ListSMTA1WorkflowItemQuery,
  ListSMTA1WorkflowItemResponse,
} from "./models/ListSMTA1WorkflowItem";
import {
  ListDashboardSMTA1WorkflowItemQuery,
  ListDashboardSMTA1WorkflowItemResponse,
} from "./models/ListDashboardSMTA1WorkflowItem";
import {
  ReadSMTA1WorkflowItemQuery,
  ReadSMTA1WorkflowItemResponse,
} from "./models/ReadSMTA1WorkflowItem";
import {
  UpdateSMTA1WorkflowItemCommand,
  UpdateSMTA1WorkflowItemResponse,
} from "./models/UpdateSMTA1WorkflowItem";

import {
  DownloadSMTA1WorkflowItemDocumentQuery,
  DownloadSMTA1WorkflowItemDocumentResponse,
} from "./models/DownloadSMTA1WorkflowItemDocument";

export interface iSMTA1WorkflowItemsService {
  read(
    query: ReadSMTA1WorkflowItemQuery
  ): Promise<Either<ReadSMTA1WorkflowItemResponse, CommunicationError>>;

  list(
    query: ListSMTA1WorkflowItemQuery
  ): Promise<Either<ListSMTA1WorkflowItemResponse, CommunicationError>>;

  listForDashboard(
    query: ListDashboardSMTA1WorkflowItemQuery
  ): Promise<
    Either<ListDashboardSMTA1WorkflowItemResponse, CommunicationError>
  >;

  create(
    command: CreateSMTA1WorkflowItemCommand
  ): Promise<Either<CreateSMTA1WorkflowItemResponse, CommunicationError>>;

  delete(
    command: DeleteSMTA1WorkflowItemCommand
  ): Promise<Either<DeleteSMTA1WorkflowItemResponse, CommunicationError>>;

  update(
    command: UpdateSMTA1WorkflowItemCommand,
    file: File | undefined
  ): Promise<Either<UpdateSMTA1WorkflowItemResponse, CommunicationError>>;

  downloadFile(
    query: DownloadSMTA1WorkflowItemDocumentQuery
  ): Promise<
    Either<DownloadSMTA1WorkflowItemDocumentResponse, CommunicationError>
  >;
}

export class SMTA1WorkflowItemsService implements iSMTA1WorkflowItemsService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "smta1workflowitems/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadSMTA1WorkflowItemQuery
  ): Promise<Either<ReadSMTA1WorkflowItemResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadSMTA1WorkflowItemResponse>(
      url
    );
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListSMTA1WorkflowItemQuery
  ): Promise<Either<ListSMTA1WorkflowItemResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListSMTA1WorkflowItemResponse>(
      this.baseUrl
    );
    return response;
  }

  async listForDashboard(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListDashboardSMTA1WorkflowItemQuery
  ): Promise<
    Either<ListDashboardSMTA1WorkflowItemResponse, CommunicationError>
  > {
    const response = await this.httpClient.get<ListSMTA1WorkflowItemResponse>(
      this.baseUrl + "/dashboard"
    );
    return response;
  }

  async create(
    command: CreateSMTA1WorkflowItemCommand
  ): Promise<Either<CreateSMTA1WorkflowItemResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateSMTA1WorkflowItemCommand,
      CreateSMTA1WorkflowItemResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteSMTA1WorkflowItemCommand
  ): Promise<Either<DeleteSMTA1WorkflowItemResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response =
      await this.httpClient.delete<DeleteSMTA1WorkflowItemResponse>(url);
    return response;
  }

  async downloadFile(
    query: DownloadSMTA1WorkflowItemDocumentQuery
  ): Promise<Either<any, CommunicationError>> {
    const url = this.buildUrl(
      query.Id + "/downloadfile/" + query.WorklistId + "/worklistid"
    );
    const response = await this.httpClient.downloadFile(url, query.Name);
    return response;
  }

  async update(
    command: UpdateSMTA1WorkflowItemCommand,
    file: File | undefined
  ): Promise<Either<UpdateSMTA1WorkflowItemResponse, CommunicationError>> {
    const formData = new FormData();
    formData.append("Command", JSON.stringify(command));
    if (file != undefined) {
      formData.append("file", file, file.name);
    }
    const response =
      await this.httpClient.patchFile<UpdateSMTA1WorkflowItemResponse>(
        this.buildUrl(command.Id),
        formData
      );

    return response;
  }
}
