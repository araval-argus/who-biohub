import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
  HttpClientPublic,
  iHttpClientPublic,
} from "../shared/HttpClient";
import {
  CreateCourierCommand,
  CreateCourierResponse,
} from "./models/CreateCourier";
import {
  DeleteCourierCommand,
  DeleteCourierResponse,
} from "./models/DeleteCourier";
import { ListCourierQuery, ListCourierResponse } from "./models/ListCourier";
import { ReadCourierQuery, ReadCourierResponse } from "./models/ReadCourier";
import {
  UpdateCourierCommand,
  UpdateCourierResponse,
} from "./models/UpdateCourier";

import {
  ListCourierBookingFormQuery,
  ListCourierBookingFormResponse,
} from "./models/ListCourierBookingForm";
import {
  ReadCourierBookingFormQuery,
  ReadCourierBookingFormResponse,
} from "./models/ReadCourierBookingForm";

export interface iCouriersService {
  read(
    query: ReadCourierQuery
  ): Promise<Either<ReadCourierResponse, CommunicationError>>;

  readBookingForm(
    query: ReadCourierBookingFormQuery
  ): Promise<Either<ReadCourierBookingFormResponse, CommunicationError>>;

  list(
    query: ListCourierQuery
  ): Promise<Either<ListCourierResponse, CommunicationError>>;

  listBookingForms(
    query: ListCourierBookingFormQuery
  ): Promise<Either<ListCourierBookingFormResponse, CommunicationError>>;

  create(
    command: CreateCourierCommand
  ): Promise<Either<CreateCourierResponse, CommunicationError>>;

  delete(
    command: DeleteCourierCommand
  ): Promise<Either<DeleteCourierResponse, CommunicationError>>;

  update(
    command: UpdateCourierCommand
  ): Promise<Either<UpdateCourierResponse, CommunicationError>>;
}

export class CouriersService implements iCouriersService {
  private httpClient: iHttpClient;
  private httpClientPublic: iHttpClientPublic;
  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
    this.httpClientPublic = new HttpClientPublic(
      process.env?.VUE_APP_API_PUBLICDATA_URL ?? ""
    );
  }

  private baseUrl = "couriers/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadCourierQuery
  ): Promise<Either<ReadCourierResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadCourierResponse>(url);
    return response;
  }

  async readBookingForm(
    query: ReadCourierBookingFormQuery
  ): Promise<Either<ReadCourierBookingFormResponse, CommunicationError>> {
    const url = "bookingforms/" + query.Id;
    const response = await this.httpClient.get<ReadCourierBookingFormResponse>(
      url
    );
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListCourierQuery
  ): Promise<Either<ListCourierResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListCourierResponse>(
      this.baseUrl
    );
    return response;
  }

  async listBookingForms(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListCourierBookingFormQuery
  ): Promise<Either<ListCourierBookingFormResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListCourierBookingFormResponse>(
      "bookingforms/" + query.CourierId + "/courier"
    );
    return response;
  }

  async create(
    command: CreateCourierCommand
  ): Promise<Either<CreateCourierResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateCourierCommand,
      CreateCourierResponse
    >(this.baseUrl, command);

    return response;
  }

  async delete(
    command: DeleteCourierCommand
  ): Promise<Either<DeleteCourierResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.delete<DeleteCourierResponse>(url);
    return response;
  }

  async update(
    command: UpdateCourierCommand
  ): Promise<Either<UpdateCourierResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateCourierCommand,
      UpdateCourierResponse
    >(url, command);
    return response;
  }
}
