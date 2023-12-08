import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";

import {
  ListWorklistFromBioHubHistoryItemQuery,
  ListWorklistFromBioHubHistoryItemResponse,
} from "./models/ListWorklistFromBioHubHistoryItem";
import {
  ReadWorklistFromBioHubHistoryItemQuery,
  ReadWorklistFromBioHubHistoryItemResponse,
} from "./models/ReadWorklistFromBioHubHistoryItem";

import {
  DownloadWorklistFromBioHubHistoryItemDocumentQuery,
  DownloadWorklistFromBioHubHistoryItemDocumentResponse,
} from "./models/DownloadWorklistFromBioHubHistoryItemDocument";

export interface iWorklistFromBioHubHistoryItemsService {
  read(
    query: ReadWorklistFromBioHubHistoryItemQuery
  ): Promise<
    Either<ReadWorklistFromBioHubHistoryItemResponse, CommunicationError>
  >;

  list(
    query: ListWorklistFromBioHubHistoryItemQuery
  ): Promise<
    Either<ListWorklistFromBioHubHistoryItemResponse, CommunicationError>
  >;

  downloadFile(
    query: DownloadWorklistFromBioHubHistoryItemDocumentQuery
  ): Promise<
    Either<
      DownloadWorklistFromBioHubHistoryItemDocumentResponse,
      CommunicationError
    >
  >;
}

export class WorklistFromBioHubHistoryItemsService
  implements iWorklistFromBioHubHistoryItemsService
{
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "worklistfrombiohubhistoryitems/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadWorklistFromBioHubHistoryItemQuery
  ): Promise<
    Either<ReadWorklistFromBioHubHistoryItemResponse, CommunicationError>
  > {
    const url = this.buildUrl(query.Id);
    const response =
      await this.httpClient.get<ReadWorklistFromBioHubHistoryItemResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListWorklistFromBioHubHistoryItemQuery
  ): Promise<
    Either<ListWorklistFromBioHubHistoryItemResponse, CommunicationError>
  > {
    const url = this.buildUrl(query.WorklistFromBioHubItemId);
    const response =
      await this.httpClient.get<ListWorklistFromBioHubHistoryItemResponse>(url);
    return response;
  }

  async downloadFile(
    query: DownloadWorklistFromBioHubHistoryItemDocumentQuery
  ): Promise<Either<any, CommunicationError>> {
    const url = this.buildUrl(
      query.Id + "/downloadfile/" + query.WorklistId + "/worklistid"
    );
    const response = await this.httpClient.downloadFile(url, query.Name);
    return response;
  }
}
