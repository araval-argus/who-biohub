import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";

import {
  ListSMTA2WorkflowHistoryItemQuery,
  ListSMTA2WorkflowHistoryItemResponse,
} from "./models/ListSMTA2WorkflowHistoryItem";
import {
  ReadSMTA2WorkflowHistoryItemQuery,
  ReadSMTA2WorkflowHistoryItemResponse,
} from "./models/ReadSMTA2WorkflowHistoryItem";

import {
  DownloadSMTA2WorkflowHistoryItemDocumentQuery,
  DownloadSMTA2WorkflowHistoryItemDocumentResponse,
} from "./models/DownloadSMTA2WorkflowHistoryItemDocument";

export interface iSMTA2WorkflowHistoryItemsService {
  read(
    query: ReadSMTA2WorkflowHistoryItemQuery
  ): Promise<Either<ReadSMTA2WorkflowHistoryItemResponse, CommunicationError>>;

  list(
    query: ListSMTA2WorkflowHistoryItemQuery
  ): Promise<Either<ListSMTA2WorkflowHistoryItemResponse, CommunicationError>>;

  downloadFile(
    query: DownloadSMTA2WorkflowHistoryItemDocumentQuery
  ): Promise<
    Either<DownloadSMTA2WorkflowHistoryItemDocumentResponse, CommunicationError>
  >;
}

export class SMTA2WorkflowHistoryItemsService
  implements iSMTA2WorkflowHistoryItemsService
{
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "smta2workflowhistoryitems/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadSMTA2WorkflowHistoryItemQuery
  ): Promise<Either<ReadSMTA2WorkflowHistoryItemResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClient.get<ReadSMTA2WorkflowHistoryItemResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListSMTA2WorkflowHistoryItemQuery
  ): Promise<Either<ListSMTA2WorkflowHistoryItemResponse, CommunicationError>> {
    const url = this.buildUrl(query.SMTA2WorkflowItemId);
    const response =
      await this.httpClient.get<ListSMTA2WorkflowHistoryItemResponse>(url);
    return response;
  }

  async downloadFile(
    query: DownloadSMTA2WorkflowHistoryItemDocumentQuery
  ): Promise<Either<any, CommunicationError>> {
    const url = this.buildUrl(
      query.Id + "/downloadfile/" + query.WorklistId + "/worklistid"
    );
    const response = await this.httpClient.downloadFile(url, query.Name);
    return response;
  }
}
