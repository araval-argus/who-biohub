import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
} from "../shared/HttpClient";

import {
  ListDocumentsQuery,
  ListDocumentsResponse,
} from "./models/ListDocuments";

import {
  ListSignedSMTADocumentsQuery,
  ListSignedSMTADocumentsResponse,
} from "./models/ListSignedSMTADocuments";

import {
  CheckDocumentQuery,
  CheckDocumentResponse,
} from "./models/CheckDocument";

import {
  CanStartSMTARequestQuery,
  CanStartSMTARequestResponse,
} from "./models/CanStartSMTARequest";

import { ReadFileQuery, ReadFileResponse } from "./models/ReadFile";

export interface iDocumentsService {
  read(
    query: ReadFileQuery
  ): Promise<Either<ReadFileResponse, CommunicationError>>;

  listSignedSMTA(
    query: ListSignedSMTADocumentsQuery
  ): Promise<Either<ListSignedSMTADocumentsResponse, CommunicationError>>;

  list(
    query: ListDocumentsQuery
  ): Promise<Either<ListDocumentsResponse, CommunicationError>>;

  check(
    query: CheckDocumentQuery
  ): Promise<Either<CheckDocumentResponse, CommunicationError>>;

  canStartSMTARequest(
    query: CanStartSMTARequestQuery
  ): Promise<Either<CanStartSMTARequestResponse, CommunicationError>>;
}

export class DocumentsService implements iDocumentsService {
  private httpClient: iHttpClient;

  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
  }

  private baseUrl = "documents/";
  private buildUrl(suffix: string | undefined): string {
    if (suffix) return `${this.baseUrl}${suffix}`;
    else return this.baseUrl;
  }

  async read(query: ReadFileQuery): Promise<Either<any, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.downloadFile(url, query.Name);
    return response;
  }

  async list(): Promise<Either<ListDocumentsResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListDocumentsResponse>(
      this.baseUrl
    );
    return response;
  }

  async listSignedSMTA(): Promise<
    Either<ListSignedSMTADocumentsResponse, CommunicationError>
  > {
    const url = this.buildUrl("signedsmta");
    const response = await this.httpClient.get<ListSignedSMTADocumentsResponse>(
      url
    );
    return response;
  }

  async check(
    query: CheckDocumentQuery
  ): Promise<Either<CheckDocumentResponse, CommunicationError>> {
    const url =
      "documents/" +
      parseInt(query.Type.toString()) +
      "/type/" +
      query.LaboratoryId +
      "/laboratory/check";
    const response = await this.httpClient.get<CheckDocumentResponse>(url);
    return response;
  }

  async canStartSMTARequest(
    query: CanStartSMTARequestQuery
  ): Promise<Either<CanStartSMTARequestResponse, CommunicationError>> {
    const url =
      "documents/" + parseInt(query.Type.toString()) + "/canstartsmta";
    const response = await this.httpClient.get<CanStartSMTARequestResponse>(
      url
    );
    return response;
  }
}
