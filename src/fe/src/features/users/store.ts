import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { User } from "@/models/User";
import { WorklistItemUser } from "@/models/WorklistItemUser";
import { UserRequest } from "@/models/UserRequest";
import { isLeft, isRight, ParseError } from "@/utils/either";
import { users } from "./mock";
import { worklistToBioHubItemAllLaboratoryUsers } from "./mock";
import { worklistToBioHubItemAllBioHubFacilityUsers } from "./mock";
import { worklistToBioHubItemAllCourierUsers } from "./mock";

import { worklistFromBioHubItemAllLaboratoryUsers } from "./mock";
import { worklistFromBioHubItemAllBioHubFacilityUsers } from "./mock";
import { worklistFromBioHubItemAllCourierUsers } from "./mock";

import { AppError } from "@/models/shared/Error";
import { AppModule } from "../../store/MainStore";
import { CreateUserResponse } from "@/services/users/models/CreateUser";
import { DeleteUserResponse } from "@/services/users/models/DeleteUser";
import { ListUserResponse } from "@/services/users/models/ListUser";

import {
  ReadUserQuery,
  ReadUserResponse,
} from "@/services/users/models/ReadUser";
import {
  ListUsersByLaboratoryIdForWorklistToBioHubItemQuery,
  ListUsersByLaboratoryIdForWorklistToBioHubItemResponse,
} from "@/services/users/models/ListUsersByLaboratoryIdForWorklistToBioHubItem";

import {
  ListUsersByBioHubFacilityIdForWorklistToBioHubItemQuery,
  ListUsersByBioHubFacilityIdForWorklistToBioHubItemResponse,
} from "@/services/users/models/ListUsersByBioHubFacilityIdForWorklistToBioHubItem";

import {
  ListCourierUsersForWorklistToBioHubItemQuery,
  ListCourierUsersForWorklistToBioHubItemResponse,
} from "@/services/users/models/ListCourierUsersForWorklistToBioHubItem";

import {
  ListUsersByLaboratoryIdForWorklistFromBioHubItemQuery,
  ListUsersByLaboratoryIdForWorklistFromBioHubItemResponse,
} from "@/services/users/models/ListUsersByLaboratoryIdForWorklistFromBioHubItem";

import {
  ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQuery,
  ListUsersByBioHubFacilityIdForWorklistFromBioHubItemResponse,
} from "@/services/users/models/ListUsersByBioHubFacilityIdForWorklistFromBioHubItem";

import {
  ListCourierUsersForWorklistFromBioHubItemQuery,
  ListCourierUsersForWorklistFromBioHubItemResponse,
} from "@/services/users/models/ListCourierUsersForWorklistFromBioHubItem";

import { UsersService } from "@/services/users/UsersService";

export interface UserState {
  UserCreate: User | undefined;
  User: User | undefined;
  Users: Array<User>;
  UserPublicCreate: User | undefined;
  UserPublic: User | undefined;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "users",
  store: store,
})
class UserStore extends VuexModule implements UserState {
  // Private variables
  private userCreate: { value: User } = {
    value: this.emptyUser,
  };

  private userPublicCreate: { value: User } = {
    value: this.emptyUser,
  };

  private user: { value: User | undefined } = {
    value: undefined,
  };

  private userPublic: { value: User | undefined } = {
    value: undefined,
  };

  private users: { value: Array<User> } = { value: users };

  private worklistToBioHubItemAllLaboratoryUsers: {
    value: Array<WorklistItemUser>;
  } = { value: worklistToBioHubItemAllLaboratoryUsers };

  private worklistToBioHubItemAllBioHubFacilityUsers: {
    value: Array<WorklistItemUser>;
  } = { value: worklistToBioHubItemAllBioHubFacilityUsers };

  private worklistToBioHubItemAllCourierUsers: {
    value: Array<WorklistItemUser>;
  } = { value: worklistToBioHubItemAllCourierUsers };

  private worklistFromBioHubItemAllLaboratoryUsers: {
    value: Array<WorklistItemUser>;
  } = { value: worklistFromBioHubItemAllLaboratoryUsers };

  private worklistFromBioHubItemAllBioHubFacilityUsers: {
    value: Array<WorklistItemUser>;
  } = { value: worklistFromBioHubItemAllBioHubFacilityUsers };

  private worklistFromBioHubItemAllCourierUsers: {
    value: Array<WorklistItemUser>;
  } = { value: worklistFromBioHubItemAllCourierUsers };

  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError | undefined): void {
    error = ParseError(error);
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_USER_CREATE(user: User): void {
    this.userCreate.value = user;
  }

  // Details - Edit
  @Mutation
  public SET_USER(user: User | undefined): void {
    this.user.value = user;
  }

  @Mutation
  public SET_WORKLISTTOBIOHUBITEMLABORATORYALLUSERS(
    worklistToBioHubItemAllLaboratoryUsers: Array<WorklistItemUser>
  ): void {
    this.worklistToBioHubItemAllLaboratoryUsers.value =
      worklistToBioHubItemAllLaboratoryUsers;
  }

  @Mutation
  public SET_WORKLISTTOBIOHUBITEMBIOHUBFACILITYALLUSERS(
    worklistToBioHubItemAllBioHubFacilityUsers: Array<WorklistItemUser>
  ): void {
    this.worklistToBioHubItemAllBioHubFacilityUsers.value =
      worklistToBioHubItemAllBioHubFacilityUsers;
  }

  @Mutation
  public SET_WORKLISTTOBIOHUBITEMCOURIERUSERS(
    worklistToBioHubItemAllCourierUsers: Array<WorklistItemUser>
  ): void {
    this.worklistToBioHubItemAllCourierUsers.value =
      worklistToBioHubItemAllCourierUsers;
  }

  @Mutation
  public SET_WORKLISTFROMBIOHUBITEMLABORATORYALLUSERS(
    worklistFromBioHubItemAllLaboratoryUsers: Array<WorklistItemUser>
  ): void {
    this.worklistFromBioHubItemAllLaboratoryUsers.value =
      worklistFromBioHubItemAllLaboratoryUsers;
  }

  @Mutation
  public SET_WORKLISTFROMBIOHUBITEMBIOHUBFACILITYALLUSERS(
    worklistFromBioHubItemAllBioHubFacilityUsers: Array<WorklistItemUser>
  ): void {
    this.worklistFromBioHubItemAllBioHubFacilityUsers.value =
      worklistFromBioHubItemAllBioHubFacilityUsers;
  }

  @Mutation
  public SET_WORKLISTFROMBIOHUBITEMCOURIERUSERS(
    worklistFromBioHubItemAllCourierUsers: Array<WorklistItemUser>
  ): void {
    this.worklistFromBioHubItemAllCourierUsers.value =
      worklistFromBioHubItemAllCourierUsers;
  }

  @Mutation
  public CLEAR_USER(): void {
    this.user.value = undefined;
  }

  @Mutation
  public CLEAR_USER_PUBLIC(): void {
    this.userPublic.value = undefined;
  }

  @Mutation
  public CLEAR_USER_CREATE(): void {
    this.userCreate.value = Object.create({
      Id: "",
      FirstName: "",
      LastName: "",
      Email: "",
      JobTitle: "",
      MobilePhone: "",
      BusinessPhone: "",
      RoleId: "",
      LaboratoryId: "",
      BioHubFacilityId: "",
      OperationalFocalPoint: false,
      CreationDate: new Date(),
      IsActive: false,
    });
  }

  // List
  @Mutation
  public SET_USERS(users: Array<User>): void {
    this.users.value = users;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateUser(): Promise<void> {
    AppModule.ShowLoading();
    const service = new UsersService();
    const user = this.userCreate.value;
    if (user === undefined) {
      this.SET_ERROR({
        message:
          "UsersStore: not expecting user to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.create(user);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateUserResponse = response.value;
      user.Id = createResponse.Id;
      this.SET_USER(user);
      this.SET_USER_CREATE(this.emptyUser);
      AppModule.SetSuccessNotifications("User successfully created");
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    AppModule.HideLoading();
    throw response.value;
  }

  @Action({ rawError: true })
  public async CreateCourierUser(): Promise<void> {
    AppModule.ShowLoading();
    const service = new UsersService();
    const user = this.userCreate.value;
    if (user === undefined) {
      this.SET_ERROR({
        message:
          "UsersStore: not expecting user to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.createCourierUser(user);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateUserResponse = response.value;
      user.Id = createResponse.Id;
      this.SET_USER(user);
      this.SET_USER_CREATE(this.emptyUser);
      AppModule.SetSuccessNotifications("User successfully created");
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    AppModule.HideLoading();
    throw response.value;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateUserFromUserRequest(
    userRequest: UserRequest | undefined
  ): Promise<void> {
    const service = new UsersService();
    const user = userRequest;
    if (user === undefined) {
      this.SET_ERROR({
        message:
          "UsersStore: not expecting user to be undefined; this should be a bug",
      });
      return;
    }

    const response = await service.createFromUserRequest(user);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateUserResponse = response.value;
      user.Id = createResponse.Id;
      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    throw response.value;
  }

  @Action({ rawError: true })
  public async ListUsers(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new UsersService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListUserResponse = response.value;
      this.SET_USERS(listResponse.Users);
      return;
    }
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    this.SET_ERROR(response.value as AppError);
    throw response.value;
  }

  @Action({ rawError: true })
  public async ListCourierUsers(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new UsersService();
    const response = await service.listCourierUsers({});
    if (isLeft(response)) {
      const listResponse: ListUserResponse = response.value;
      this.SET_USERS(listResponse.Users);
      return;
    }
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    this.SET_ERROR(response.value as AppError);
    throw response.value;
  }

  @Action({ rawError: true })
  public async ReadUser(id: string): Promise<void> {
    const service = new UsersService();
    const query: ReadUserQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ReadUserResponse = response.value;
      this.SET_USER(readResponse.User);
      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    throw response.value;
  }

  @Action({ rawError: true })
  public async ReadCourierUser(id: string): Promise<void> {
    const service = new UsersService();
    const query: ReadUserQuery = { Id: id };
    const response = await service.readCourierUser(query);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ReadUserResponse = response.value;
      this.SET_USER(readResponse.User);
      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    throw response.value;
  }

  @Action({ rawError: true })
  public async ListUsersByLaboratoryIdForWorklistToBioHubItem(
    info: Map<string, string>
  ): Promise<void> {
    const service = new UsersService();
    const laboratoryId = info.get("LaboratoryId");
    const worklistToBioHubItemId = info.get("WorklistToBioHubItemId");

    const query: ListUsersByLaboratoryIdForWorklistToBioHubItemQuery = {
      LaboratoryId: laboratoryId !== undefined ? laboratoryId : "",
      WorklistToBioHubItemId:
        worklistToBioHubItemId !== undefined ? worklistToBioHubItemId : "",
    };
    const response =
      await service.listUsersByLaboratoryIdForWorklistToBioHubItem(query);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ListUsersByLaboratoryIdForWorklistToBioHubItemResponse =
        response.value;
      this.SET_WORKLISTTOBIOHUBITEMLABORATORYALLUSERS(
        readResponse.WorklistToBioHubItemUsers
      );

      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    throw response.value;
  }

  @Action({ rawError: true })
  public async ListUsersByBioHubFacilityIdForWorklistToBioHubItem(
    info: Map<string, string>
  ): Promise<void> {
    const service = new UsersService();
    const bioHubFacilityId = info.get("BioHubFacilityId");
    const worklistToBioHubItemId = info.get("WorklistToBioHubItemId");

    const query: ListUsersByBioHubFacilityIdForWorklistToBioHubItemQuery = {
      BioHubFacilityId: bioHubFacilityId !== undefined ? bioHubFacilityId : "",
      WorklistToBioHubItemId:
        worklistToBioHubItemId !== undefined ? worklistToBioHubItemId : "",
    };
    const response =
      await service.listUsersByBioHubFacilityIdForWorklistToBioHubItem(query);

    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ListUsersByBioHubFacilityIdForWorklistToBioHubItemResponse =
        response.value;
      this.SET_WORKLISTTOBIOHUBITEMBIOHUBFACILITYALLUSERS(
        readResponse.WorklistToBioHubItemUsers
      );
      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    throw response.value;
  }

  @Action({ rawError: true })
  public async ListCourierUsersForWorklistToBioHubItem(
    info: Map<string, string>
  ): Promise<void> {
    const service = new UsersService();
    const worklistToBioHubItemId = info.get("WorklistToBioHubItemId");

    const query: ListCourierUsersForWorklistToBioHubItemQuery = {
      WorklistToBioHubItemId:
        worklistToBioHubItemId !== undefined ? worklistToBioHubItemId : "",
    };
    const response = await service.listCourierUsersForWorklistToBioHubItem(
      query
    );
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ListCourierUsersForWorklistToBioHubItemResponse =
        response.value;
      this.SET_WORKLISTTOBIOHUBITEMCOURIERUSERS(
        readResponse.WorklistToBioHubItemUsers
      );
      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    throw response.value;
  }

  @Action({ rawError: true })
  public async ListUsersByLaboratoryIdForWorklistFromBioHubItem(
    info: Map<string, string>
  ): Promise<void> {
    const service = new UsersService();
    const laboratoryId = info.get("LaboratoryId");
    const worklistFromBioHubItemId = info.get("WorklistFromBioHubItemId");

    const query: ListUsersByLaboratoryIdForWorklistFromBioHubItemQuery = {
      LaboratoryId: laboratoryId !== undefined ? laboratoryId : "",
      WorklistFromBioHubItemId:
        worklistFromBioHubItemId !== undefined ? worklistFromBioHubItemId : "",
    };
    const response =
      await service.listUsersByLaboratoryIdForWorklistFromBioHubItem(query);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ListUsersByLaboratoryIdForWorklistFromBioHubItemResponse =
        response.value;
      this.SET_WORKLISTFROMBIOHUBITEMLABORATORYALLUSERS(
        readResponse.WorklistFromBioHubItemUsers
      );

      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    throw response.value;
  }

  @Action({ rawError: true })
  public async ListUsersByBioHubFacilityIdForWorklistFromBioHubItem(
    info: Map<string, string>
  ): Promise<void> {
    const service = new UsersService();
    const bioHubFacilityId = info.get("BioHubFacilityId");
    const worklistFromBioHubItemId = info.get("WorklistFromBioHubItemId");

    const query: ListUsersByBioHubFacilityIdForWorklistFromBioHubItemQuery = {
      BioHubFacilityId: bioHubFacilityId !== undefined ? bioHubFacilityId : "",
      WorklistFromBioHubItemId:
        worklistFromBioHubItemId !== undefined ? worklistFromBioHubItemId : "",
    };
    const response =
      await service.listUsersByBioHubFacilityIdForWorklistFromBioHubItem(query);

    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ListUsersByBioHubFacilityIdForWorklistFromBioHubItemResponse =
        response.value;
      this.SET_WORKLISTFROMBIOHUBITEMBIOHUBFACILITYALLUSERS(
        readResponse.WorklistFromBioHubItemUsers
      );
      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    throw response.value;
  }

  @Action({ rawError: true })
  public async ListCourierUsersForWorklistFromBioHubItem(
    info: Map<string, string>
  ): Promise<void> {
    const service = new UsersService();
    const worklistFromBioHubItemId = info.get("WorklistFromBioHubItemId");

    const query: ListCourierUsersForWorklistFromBioHubItemQuery = {
      WorklistFromBioHubItemId:
        worklistFromBioHubItemId !== undefined ? worklistFromBioHubItemId : "",
    };
    const response = await service.listCourierUsersForWorklistFromBioHubItem(
      query
    );
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ListCourierUsersForWorklistFromBioHubItemResponse =
        response.value;
      this.SET_WORKLISTFROMBIOHUBITEMCOURIERUSERS(
        readResponse.WorklistFromBioHubItemUsers
      );
      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
    throw response.value;
  }

  @Action({ rawError: true })
  public async UpdateUser(): Promise<void> {
    AppModule.ShowLoading();
    const service = new UsersService();
    const user: User | undefined = this.User;
    if (!user) {
      this.SET_ERROR({
        message:
          "UsersStore: not expecting user to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.update(user);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      AppModule.HideLoading();
      throw response.value;
    } else {
      this.SET_ERROR(undefined);
      this.SET_USER(user);
      AppModule.SetSuccessNotifications("User successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async UpdateCourierUser(): Promise<void> {
    AppModule.ShowLoading();
    const service = new UsersService();
    const user: User | undefined = this.User;
    if (!user) {
      this.SET_ERROR({
        message:
          "UsersStore: not expecting user to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.updateCourierUser(user);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      AppModule.HideLoading();
      throw response.value;
    } else {
      this.SET_ERROR(undefined);
      this.SET_USER(user);
      AppModule.SetSuccessNotifications("User successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async DeleteUser(): Promise<void> {
    AppModule.ShowLoading();
    const service = new UsersService();
    const user: User | undefined = this.User;
    if (!user) {
      this.SET_ERROR({
        message:
          "UsersStore: not expecting user to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.delete(user);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      AppModule.HideLoading();
      throw response.value;
    } else {
      const deleteUserResponse: DeleteUserResponse = response.value;
      this.SET_USER(undefined);
      AppModule.SetSuccessNotifications("User successfully deleted");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async DeleteCourierUser(): Promise<void> {
    AppModule.ShowLoading();
    const service = new UsersService();
    const user: User | undefined = this.User;
    if (!user) {
      this.SET_ERROR({
        message:
          "UsersStore: not expecting user to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.deleteCourierUser(user);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      AppModule.HideLoading();
      throw response.value;
    } else {
      const deleteUserResponse: DeleteUserResponse = response.value;
      this.SET_USER(undefined);
      AppModule.SetSuccessNotifications("User successfully deleted");
      AppModule.HideLoading();
      return;
    }
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get ErrorMessage(): any {
    return this.error.value?.message;
  }

  get User(): User | undefined {
    return this.user.value;
  }

  get Users(): User[] {
    return this.users.value ?? new Array<User>();
  }

  get UserPublic(): User | undefined {
    return this.userPublic.value;
  }

  get UserCreate(): User {
    return this.userCreate.value;
  }

  get UserPublicCreate(): User {
    return this.userPublicCreate.value;
  }

  get WorklistToBioHubItemAllLaboratoryUsers(): Array<WorklistItemUser> {
    return this.worklistToBioHubItemAllLaboratoryUsers.value;
  }

  get WorklistToBioHubItemAllBioHubFacilityUsers(): Array<WorklistItemUser> {
    return this.worklistToBioHubItemAllBioHubFacilityUsers.value;
  }

  get WorklistToBioHubItemAllCourierUsers(): Array<WorklistItemUser> {
    return this.worklistToBioHubItemAllCourierUsers.value;
  }

  get WorklistFromBioHubItemAllLaboratoryUsers(): Array<WorklistItemUser> {
    return this.worklistFromBioHubItemAllLaboratoryUsers.value;
  }

  get WorklistFromBioHubItemAllBioHubFacilityUsers(): Array<WorklistItemUser> {
    return this.worklistFromBioHubItemAllBioHubFacilityUsers.value;
  }

  get WorklistFromBioHubItemAllCourierUsers(): Array<WorklistItemUser> {
    return this.worklistFromBioHubItemAllCourierUsers.value;
  }

  get emptyUser(): User {
    return Object.create({
      Id: "",
      FirstName: "",
      LastName: "",
      Email: "",
      JobTitle: "",
      MobilePhone: "",
      BusinessPhone: "",
      RoleId: "",
      LaboratoryId: "",
      BioHubFacilityId: "",
      OperationalFocalPoint: false,
      CreationDate: new Date(),
      IsActive: false,
    } as User);
  }
}

export const UserModule = getModule(UserStore, store);
