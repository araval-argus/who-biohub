import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
} from "../shared/HttpClient";

import { ListEFormsQuery, ListEFormsResponse } from "./models/ListEForms";

import {
  ReadAnnex2OfSMTA1Query,
  ReadAnnex2OfSMTA1Response,
} from "./models/ReadAnnex2OfSMTA1";

import {
  ReadBookingFormOfSMTA1Query,
  ReadBookingFormOfSMTA1Response,
} from "./models/ReadBookingFormOfSMTA1";

import {
  ReadAnnex2OfSMTA2Query,
  ReadAnnex2OfSMTA2Response,
} from "./models/ReadAnnex2OfSMTA2";

import {
  ReadBiosafetyChecklistOfSMTA2Query,
  ReadBiosafetyChecklistOfSMTA2Response,
} from "./models/ReadBiosafetyChecklistOfSMTA2";

import {
  ReadBookingFormOfSMTA2Query,
  ReadBookingFormOfSMTA2Response,
} from "./models/ReadBookingFormOfSMTA2";

export interface iEFormsService {
  list(
    query: ListEFormsQuery
  ): Promise<Either<ListEFormsResponse, CommunicationError>>;

  readAnnex2OfSMTA1(
    query: ReadAnnex2OfSMTA1Query
  ): Promise<Either<ReadAnnex2OfSMTA1Response, CommunicationError>>;

  readBookingFormOfSMTA1(
    query: ReadBookingFormOfSMTA1Query
  ): Promise<Either<ReadBookingFormOfSMTA1Response, CommunicationError>>;

  readAnnex2OfSMTA2(
    query: ReadAnnex2OfSMTA2Query
  ): Promise<Either<ReadAnnex2OfSMTA2Response, CommunicationError>>;

  readBiosafetyChecklistOfSMTA2(
    query: ReadBiosafetyChecklistOfSMTA2Query
  ): Promise<Either<ReadBiosafetyChecklistOfSMTA2Response, CommunicationError>>;

  readBookingFormOfSMTA2(
    query: ReadBookingFormOfSMTA2Query
  ): Promise<Either<ReadBookingFormOfSMTA2Response, CommunicationError>>;
}

export class EFormsService implements iEFormsService {
  private httpClient: iHttpClient;

  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
  }

  private baseUrl = "eforms/";
  private buildUrl(suffix: string | undefined): string {
    if (suffix) return `${this.baseUrl}${suffix}`;
    else return this.baseUrl;
  }

  async list(): Promise<Either<ListEFormsResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListEFormsResponse>(
      this.baseUrl
    );
    return response;
  }

  async readAnnex2OfSMTA1(
    query: ReadAnnex2OfSMTA1Query
  ): Promise<Either<ReadAnnex2OfSMTA1Response, CommunicationError>> {
    const url = this.buildUrl(query.Id + "/annex2ofsmta1");
    const response = await this.httpClient.get<ReadAnnex2OfSMTA1Response>(url);
    return response;
  }

  async readBookingFormOfSMTA1(
    query: ReadBookingFormOfSMTA1Query
  ): Promise<Either<ReadBookingFormOfSMTA1Response, CommunicationError>> {
    const url = this.buildUrl(query.Id + "/bookingformofsmta1");
    const response = await this.httpClient.get<ReadBookingFormOfSMTA1Response>(
      url
    );
    return response;
  }

  async readAnnex2OfSMTA2(
    query: ReadAnnex2OfSMTA2Query
  ): Promise<Either<ReadAnnex2OfSMTA2Response, CommunicationError>> {
    const url = this.buildUrl(query.Id + "/annex2ofsmta2");
    const response = await this.httpClient.get<ReadAnnex2OfSMTA2Response>(url);
    return response;
  }

  async readBiosafetyChecklistOfSMTA2(
    query: ReadBiosafetyChecklistOfSMTA2Query
  ): Promise<
    Either<ReadBiosafetyChecklistOfSMTA2Response, CommunicationError>
  > {
    const url = this.buildUrl(query.Id + "/biosafetychecklistofsmta2");
    const response =
      await this.httpClient.get<ReadBiosafetyChecklistOfSMTA2Response>(url);
    return response;
  }

  async readBookingFormOfSMTA2(
    query: ReadBookingFormOfSMTA2Query
  ): Promise<Either<ReadBookingFormOfSMTA2Response, CommunicationError>> {
    const url = this.buildUrl(query.Id + "/bookingformofsmta2");
    const response = await this.httpClient.get<ReadBookingFormOfSMTA2Response>(
      url
    );
    return response;
  }
}
