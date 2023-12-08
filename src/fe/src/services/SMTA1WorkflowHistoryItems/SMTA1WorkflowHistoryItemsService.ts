import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";

import {
  ListSMTA1WorkflowHistoryItemQuery,
  ListSMTA1WorkflowHistoryItemResponse,
} from "./models/ListSMTA1WorkflowHistoryItem";
import {
  ReadSMTA1WorkflowHistoryItemQuery,
  ReadSMTA1WorkflowHistoryItemResponse,
} from "./models/ReadSMTA1WorkflowHistoryItem";

import {
  DownloadSMTA1WorkflowHistoryItemDocumentQuery,
  DownloadSMTA1WorkflowHistoryItemDocumentResponse,
} from "./models/DownloadSMTA1WorkflowHistoryItemDocument";

export interface iSMTA1WorkflowHistoryItemsService {
  read(
    query: ReadSMTA1WorkflowHistoryItemQuery
  ): Promise<Either<ReadSMTA1WorkflowHistoryItemResponse, CommunicationError>>;

  list(
    query: ListSMTA1WorkflowHistoryItemQuery
  ): Promise<Either<ListSMTA1WorkflowHistoryItemResponse, CommunicationError>>;

  downloadFile(
    query: DownloadSMTA1WorkflowHistoryItemDocumentQuery
  ): Promise<
    Either<DownloadSMTA1WorkflowHistoryItemDocumentResponse, CommunicationError>
  >;
}

export class SMTA1WorkflowHistoryItemsService
  implements iSMTA1WorkflowHistoryItemsService
{
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "smta1workflowhistoryitems/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadSMTA1WorkflowHistoryItemQuery
  ): Promise<Either<ReadSMTA1WorkflowHistoryItemResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClient.get<ReadSMTA1WorkflowHistoryItemResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListSMTA1WorkflowHistoryItemQuery
  ): Promise<Either<ListSMTA1WorkflowHistoryItemResponse, CommunicationError>> {
    const url = this.buildUrl(query.SMTA1WorkflowItemId);
    const response =
      await this.httpClient.get<ListSMTA1WorkflowHistoryItemResponse>(url);
    return response;
  }

  async downloadFile(
    query: DownloadSMTA1WorkflowHistoryItemDocumentQuery
  ): Promise<Either<any, CommunicationError>> {
    const url = this.buildUrl(
      query.Id + "/downloadfile/" + query.WorklistId + "/worklistid"
    );
    const response = await this.httpClient.downloadFile(url, query.Name);
    return response;
  }
}
