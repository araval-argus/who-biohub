import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";

import {
  ListWorklistToBioHubHistoryItemQuery,
  ListWorklistToBioHubHistoryItemResponse,
} from "./models/ListWorklistToBioHubHistoryItem";
import {
  ReadWorklistToBioHubHistoryItemQuery,
  ReadWorklistToBioHubHistoryItemResponse,
} from "./models/ReadWorklistToBioHubHistoryItem";

import {
  DownloadWorklistToBioHubHistoryItemDocumentQuery,
  DownloadWorklistToBioHubHistoryItemDocumentResponse,
} from "./models/DownloadWorklistToBioHubHistoryItemDocument";

export interface iWorklistToBioHubHistoryItemsService {
  read(
    query: ReadWorklistToBioHubHistoryItemQuery
  ): Promise<
    Either<ReadWorklistToBioHubHistoryItemResponse, CommunicationError>
  >;

  list(
    query: ListWorklistToBioHubHistoryItemQuery
  ): Promise<
    Either<ListWorklistToBioHubHistoryItemResponse, CommunicationError>
  >;

  downloadFile(
    query: DownloadWorklistToBioHubHistoryItemDocumentQuery
  ): Promise<
    Either<
      DownloadWorklistToBioHubHistoryItemDocumentResponse,
      CommunicationError
    >
  >;
}

export class WorklistToBioHubHistoryItemsService
  implements iWorklistToBioHubHistoryItemsService
{
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "worklisttobiohubhistoryitems/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadWorklistToBioHubHistoryItemQuery
  ): Promise<
    Either<ReadWorklistToBioHubHistoryItemResponse, CommunicationError>
  > {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClient.get<ReadWorklistToBioHubHistoryItemResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListWorklistToBioHubHistoryItemQuery
  ): Promise<
    Either<ListWorklistToBioHubHistoryItemResponse, CommunicationError>
  > {
    const url = this.buildUrl(query.WorklistToBioHubItemId);
    const response =
      await this.httpClient.get<ListWorklistToBioHubHistoryItemResponse>(url);
    return response;
  }

  async downloadFile(
    query: DownloadWorklistToBioHubHistoryItemDocumentQuery
  ): Promise<Either<any, CommunicationError>> {
    const url = this.buildUrl(
      query.Id + "/downloadfile/" + query.WorklistId + "/worklistid"
    );
    const response = await this.httpClient.downloadFile(url, query.Name);
    return response;
  }
}
