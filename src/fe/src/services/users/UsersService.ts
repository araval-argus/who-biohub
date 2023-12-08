import { Either } from "@/utils/either";
import {
  CommunicationError,
  HttpClient,
  iHttpClient,
} from "../shared/HttpClient";
import { CreateUserCommand, CreateUserResponse } from "./models/CreateUser";
import { DeleteUserCommand, DeleteUserResponse } from "./models/DeleteUser";
import { ListUserQuery, ListUserResponse } from "./models/ListUser";
import { ReadUserQuery, ReadUserResponse } from "./models/ReadUser";
import {
  CreateUserFromUserRequestCommand,
  CreateUserFromUserRequestResponse,
} from "./models/CreateUserFromUserRequest";
import {
  ListUsersByLaboratoryIdForWorklistToBioHubItemQuery,
  ListUsersByLaboratoryIdForWorklistToBioHubItemResponse,
} from "./models/ListUsersByLaboratoryIdForWorklistToBioHubItem";

import {
  ListUsersByBioHubFacilityIdForWorklistToBioHubItemQuery,
  ListUsersByBioHubFacilityIdForWorklistToBioHubItemResponse,
} from "./models/ListUsersByBioHubFacilityIdForWorklistToBioHubItem";

import {
  ListCourierUsersForWorklistToBioHubItemQuery,
  ListCourierUsersForWorklistToBioHubItemResponse,
} from "./models/ListCourierUsersForWorklistToBioHubItem";

import {
  ListUsersByLaboratoryIdForWorklistFromBioHubItemQuery,
  ListUsersByLaboratoryIdForWorklistFromBioHubItemResponse,
} from "./models/ListUsersByLaboratoryIdForWorklistFromBioHubItem";

import {
  ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQuery,
  ListUsersByBioHubFacilityIdForWorklistFromBioHubItemResponse,
} from "./models/ListUsersByBioHubFacilityIdForWorklistFromBioHubItem";

import {
  ListCourierUsersForWorklistFromBioHubItemQuery,
  ListCourierUsersForWorklistFromBioHubItemResponse,
} from "./models/ListCourierUsersForWorklistFromBioHubItem";

import { UpdateUserCommand, UpdateUserResponse } from "./models/UpdateUser";

export interface iUsersService {
  read(
    query: ReadUserQuery
  ): Promise<Either<ReadUserResponse, CommunicationError>>;

  list(
    query: ListUserQuery
  ): Promise<Either<ListUserResponse, CommunicationError>>;

  listCourierUsers(
    query: ListUserQuery
  ): Promise<Either<ListUserResponse, CommunicationError>>;

  create(
    command: CreateUserCommand
  ): Promise<Either<CreateUserResponse, CommunicationError>>;

  createCourierUser(
    command: CreateUserCommand
  ): Promise<Either<CreateUserResponse, CommunicationError>>;

  createFromUserRequest(
    command: CreateUserFromUserRequestCommand
  ): Promise<Either<CreateUserFromUserRequestResponse, CommunicationError>>;

  delete(
    command: DeleteUserCommand
  ): Promise<Either<DeleteUserResponse, CommunicationError>>;

  deleteCourierUser(
    command: DeleteUserCommand
  ): Promise<Either<DeleteUserResponse, CommunicationError>>;

  update(
    command: UpdateUserCommand
  ): Promise<Either<UpdateUserResponse, CommunicationError>>;

  updateCourierUser(
    command: UpdateUserCommand
  ): Promise<Either<UpdateUserResponse, CommunicationError>>;

  listUsersByLaboratoryIdForWorklistToBioHubItem(
    query: ListUsersByLaboratoryIdForWorklistToBioHubItemQuery
  ): Promise<
    Either<
      ListUsersByLaboratoryIdForWorklistToBioHubItemResponse,
      CommunicationError
    >
  >;

  listUsersByBioHubFacilityIdForWorklistToBioHubItem(
    query: ListUsersByBioHubFacilityIdForWorklistToBioHubItemQuery
  ): Promise<
    Either<
      ListUsersByBioHubFacilityIdForWorklistToBioHubItemResponse,
      CommunicationError
    >
  >;

  listCourierUsersForWorklistToBioHubItem(
    query: ListCourierUsersForWorklistToBioHubItemQuery
  ): Promise<
    Either<ListCourierUsersForWorklistToBioHubItemResponse, CommunicationError>
  >;

  listUsersByLaboratoryIdForWorklistFromBioHubItem(
    query: ListUsersByLaboratoryIdForWorklistFromBioHubItemQuery
  ): Promise<
    Either<
      ListUsersByLaboratoryIdForWorklistFromBioHubItemResponse,
      CommunicationError
    >
  >;

  listUsersByBioHubFacilityIdForWorklistFromBioHubItem(
    query: ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQuery
  ): Promise<
    Either<
      ListUsersByBioHubFacilityIdForWorklistFromBioHubItemResponse,
      CommunicationError
    >
  >;

  listCourierUsersForWorklistFromBioHubItem(
    query: ListCourierUsersForWorklistFromBioHubItemQuery
  ): Promise<
    Either<
      ListCourierUsersForWorklistFromBioHubItemResponse,
      CommunicationError
    >
  >;
}

export class UsersService implements iUsersService {
  private httpClient: iHttpClient;

  constructor() {
    this.httpClient = new HttpClient(process.env?.VUE_APP_API_DATA_URL ?? "");
  }

  private baseUrl = "users/";
  private buildUrl(suffix: string): string {
    return `${this.baseUrl}${suffix}`;
  }

  async read(
    query: ReadUserQuery
  ): Promise<Either<ReadUserResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id);
    const response = await this.httpClient.get<ReadUserResponse>(url);
    return response;
  }

  async readCourierUser(
    query: ReadUserQuery
  ): Promise<Either<ReadUserResponse, CommunicationError>> {
    const url = this.buildUrl(query.Id + "/courier");
    const response = await this.httpClient.get<ReadUserResponse>(url);
    return response;
  }

  async list(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListUserQuery
  ): Promise<Either<ListUserResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListUserResponse>(this.baseUrl);
    return response;
  }

  async listCourierUsers(
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    query: ListUserQuery
  ): Promise<Either<ListUserResponse, CommunicationError>> {
    const response = await this.httpClient.get<ListUserResponse>(
      this.baseUrl + "/courier"
    );
    return response;
  }

  async create(
    command: CreateUserCommand
  ): Promise<Either<CreateUserResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateUserCommand,
      CreateUserResponse
    >(this.baseUrl, command);

    return response;
  }

  async createCourierUser(
    command: CreateUserCommand
  ): Promise<Either<CreateUserResponse, CommunicationError>> {
    const response = await this.httpClient.post<
      CreateUserCommand,
      CreateUserResponse
    >(this.baseUrl + "/courier", command);

    return response;
  }

  async createFromUserRequest(
    command: CreateUserFromUserRequestCommand
  ): Promise<Either<CreateUserFromUserRequestResponse, CommunicationError>> {
    const url = this.buildUrl("createfromuserrequest");
    const response = await this.httpClient.post<
      CreateUserFromUserRequestCommand,
      CreateUserFromUserRequestResponse
    >(url, command);

    return response;
  }

  async delete(
    command: DeleteUserCommand
  ): Promise<Either<DeleteUserResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.delete<DeleteUserResponse>(url);
    return response;
  }

  async deleteCourierUser(
    command: DeleteUserCommand
  ): Promise<Either<DeleteUserResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id + "/courier");
    const response = await this.httpClient.delete<DeleteUserResponse>(url);
    return response;
  }

  async update(
    command: UpdateUserCommand
  ): Promise<Either<UpdateUserResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id);
    const response = await this.httpClient.patch<
      UpdateUserCommand,
      UpdateUserResponse
    >(url, command);
    return response;
  }

  async updateCourierUser(
    command: UpdateUserCommand
  ): Promise<Either<UpdateUserResponse, CommunicationError>> {
    const url = this.buildUrl(command.Id + "/courier");
    const response = await this.httpClient.patch<
      UpdateUserCommand,
      UpdateUserResponse
    >(url, command);
    return response;
  }

  async listUsersByLaboratoryIdForWorklistToBioHubItem(
    query: ListUsersByLaboratoryIdForWorklistToBioHubItemQuery
  ): Promise<
    Either<
      ListUsersByLaboratoryIdForWorklistToBioHubItemResponse,
      CommunicationError
    >
  > {
    const url = this.buildUrl(
      query.LaboratoryId +
        "/laboratoryId/" +
        query.WorklistToBioHubItemId +
        "/worklistToBioHubItemId"
    );
    const response =
      await this.httpClient.get<ListUsersByLaboratoryIdForWorklistToBioHubItemResponse>(
        url
      );
    return response;
  }

  async listUsersByBioHubFacilityIdForWorklistToBioHubItem(
    query: ListUsersByBioHubFacilityIdForWorklistToBioHubItemQuery
  ): Promise<
    Either<
      ListUsersByBioHubFacilityIdForWorklistToBioHubItemResponse,
      CommunicationError
    >
  > {
    const url = this.buildUrl(
      query.BioHubFacilityId +
        "/biohubfacilityid/" +
        query.WorklistToBioHubItemId +
        "/worklistToBioHubItemId"
    );
    const response =
      await this.httpClient.get<ListUsersByBioHubFacilityIdForWorklistToBioHubItemResponse>(
        url
      );
    return response;
  }

  async listCourierUsersForWorklistToBioHubItem(
    query: ListCourierUsersForWorklistToBioHubItemQuery
  ): Promise<
    Either<ListCourierUsersForWorklistToBioHubItemResponse, CommunicationError>
  > {
    const url = this.buildUrl(
      "courier/" + query.WorklistToBioHubItemId + "/worklistToBioHubItemId"
    );
    const response =
      await this.httpClient.get<ListCourierUsersForWorklistToBioHubItemResponse>(
        url
      );
    return response;
  }

  async listUsersByLaboratoryIdForWorklistFromBioHubItem(
    query: ListUsersByLaboratoryIdForWorklistFromBioHubItemQuery
  ): Promise<
    Either<
      ListUsersByLaboratoryIdForWorklistFromBioHubItemResponse,
      CommunicationError
    >
  > {
    const url = this.buildUrl(
      query.LaboratoryId +
        "/laboratoryId/" +
        query.WorklistFromBioHubItemId +
        "/worklistFromBioHubItemId"
    );
    const response =
      await this.httpClient.get<ListUsersByLaboratoryIdForWorklistFromBioHubItemResponse>(
        url
      );
    return response;
  }

  async listUsersByBioHubFacilityIdForWorklistFromBioHubItem(
    query: ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQuery
  ): Promise<
    Either<
      ListUsersByBioHubFacilityIdForWorklistFromBioHubItemResponse,
      CommunicationError
    >
  > {
    const url = this.buildUrl(
      query.BioHubFacilityId +
        "/biohubfacilityid/" +
        query.WorklistFromBioHubItemId +
        "/worklistFromBioHubItemId"
    );
    const response =
      await this.httpClient.get<ListUsersByBioHubFacilityIdForWorklistFromBioHubItemResponse>(
        url
      );
    return response;
  }

  async listCourierUsersForWorklistFromBioHubItem(
    query: ListCourierUsersForWorklistFromBioHubItemQuery
  ): Promise<
    Either<
      ListCourierUsersForWorklistFromBioHubItemResponse,
      CommunicationError
    >
  > {
    const url = this.buildUrl(
      "courier/" + query.WorklistFromBioHubItemId + "/worklistFromBioHubItemId"
    );
    const response =
      await this.httpClient.get<ListCourierUsersForWorklistFromBioHubItemResponse>(
        url
      );
    return response;
  }
}
