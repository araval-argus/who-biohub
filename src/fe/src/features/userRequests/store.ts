import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { UserRequest } from "@/models/UserRequest";
import { isLeft, isRight, ParseError } from "@/utils/either";
import { userRequests } from "./mock";
import { AppError } from "@/models/shared/Error";
import { AppModule } from "../../store/MainStore";
import { CreateUserRequestPublicResponse } from "@/services/userRequests/models/CreateUserRequestPublic";
import { CreateUserRequestResponse } from "@/services/userRequests/models/CreateUserRequest";
import { DeleteUserRequestResponse } from "@/services/userRequests/models/DeleteUserRequest";
import { ListUserRequestResponse } from "@/services/userRequests/models/ListUserRequest";
import {
  ReadUserRequestQuery,
  ReadUserRequestResponse,
} from "@/services/userRequests/models/ReadUserRequest";
import {
  ReadUserRequestPublicQuery,
  ReadUserRequestPublicResponse,
} from "@/services/userRequests/models/ReadUserRequestPublic";
import { UserRequestsService } from "@/services/userRequests/UserRequestsService";

export interface UserRequestState {
  UserRequestCreate: UserRequest | undefined;
  UserRequest: UserRequest | undefined;
  UserRequests: Array<UserRequest>;
  UserRequestPublicCreate: UserRequest | undefined;
  UserRequestPublic: UserRequest | undefined;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "userRequests",
  store: store,
})
class UserRequestStore extends VuexModule implements UserRequestState {
  // Private variables
  private userRequestCreate: { value: UserRequest } = {
    value: this.emptyUserRequest,
  };

  private userRequestPublicCreate: { value: UserRequest } = {
    value: this.emptyUserRequest,
  };

  private userRequest: { value: UserRequest | undefined } = {
    value: undefined,
  };

  private userRequestPublic: { value: UserRequest | undefined } = {
    value: undefined,
  };

  private userRequests: { value: Array<UserRequest> } = { value: userRequests };

  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError | undefined): void {
    error = ParseError(error);
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_USERREQUEST_CREATE(userRequest: UserRequest): void {
    this.userRequestCreate.value = userRequest;
  }

  @Mutation
  public SET_USERREQUEST_PUBLIC_CREATE(userRequest: UserRequest): void {
    this.userRequestPublicCreate.value = userRequest;
  }

  // Details - Edit
  @Mutation
  public SET_USERREQUEST(userRequest: UserRequest | undefined): void {
    this.userRequest.value = userRequest;
  }

  @Mutation
  public SET_LABORATORY_ID(laboratoryId: string): void {
    if (this.userRequest.value !== undefined) {
      this.userRequest.value.LaboratoryId = laboratoryId;
    }
  }

  @Mutation
  public SET_USERREQUEST_PUBLIC(userRequest: UserRequest | undefined): void {
    this.userRequestPublic.value = userRequest;
  }

  @Mutation
  public CLEAR_USERREQUEST(): void {
    this.userRequest.value = undefined;
  }

  @Mutation
  public CLEAR_USERREQUEST_PUBLIC(): void {
    this.userRequestPublic.value = undefined;
  }

  @Mutation
  public CLEAR_USERREQUEST_CREATE(): void {
    this.userRequestCreate.value = Object.create({
      Id: "",
      FirstName: "",
      LastName: "",
      Email: "",
      Purpose: "",
      Status: 0,
      TermsAndConditionAccepted: false,
      RoleId: "",
      CountryId: "",
      LaboratoryId: "",
      RecaptchaResponse: "",
    });
  }

  @Mutation
  public CLEAR_USERREQUEST_PUBLIC_CREATE(): void {
    this.userRequestPublicCreate.value = Object.create({
      d: "",
      FirstName: "",
      LastName: "",
      Email: "",
      Purpose: "",
      Status: 0,
      TermsAndConditionAccepted: false,
      RoleId: "",
      CountryId: "",
      RecaptchaResponse: "",
    });
  }

  // List
  @Mutation
  public SET_USERREQUESTS(userRequests: Array<UserRequest>): void {
    this.userRequests.value = userRequests;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateUserRequest(): Promise<void> {
    AppModule.ShowLoading();
    const service = new UserRequestsService();
    const userRequest = this.userRequestCreate.value;
    if (userRequest === undefined) {
      this.SET_ERROR({
        message:
          "UserRequestsStore: not expecting userRequest to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.create(userRequest);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateUserRequestResponse = response.value;
      userRequest.Id = createResponse.Id;
      this.SET_USERREQUEST(userRequest);
      this.SET_USERREQUEST_CREATE(this.emptyUserRequest);
      AppModule.SetSuccessNotifications("User Request successfully created");
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
  public async PublicCreateUserRequest(): Promise<void> {
    AppModule.ShowLoading();
    const service = new UserRequestsService();
    const userRequest = this.userRequestPublicCreate.value;
    if (userRequest === undefined) {
      this.SET_ERROR({
        message:
          "UserRequestsStore: not expecting userRequest to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.createPublic(userRequest);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const createResponse: CreateUserRequestPublicResponse = response.value;
      userRequest.Id = createResponse.Id;
      this.SET_USERREQUEST(userRequest);
      this.SET_USERREQUEST_CREATE(this.emptyUserRequest);
      AppModule.SetSuccessNotifications("User Request successfully created");
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
  public async ListUserRequests(): Promise<void> {
    const service = new UserRequestsService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListUserRequestResponse = response.value;
      this.SET_USERREQUESTS(listResponse.UserRequests);
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
  public async ReadUserRequest(id: string): Promise<void> {
    const service = new UserRequestsService();
    const query: ReadUserRequestQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ReadUserRequestResponse = response.value;
      this.SET_USERREQUEST(readResponse.UserRequest);
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
  public async PublicReadUserRequest(id: string): Promise<void> {
    const service = new UserRequestsService();
    const query: ReadUserRequestPublicQuery = { Id: id };
    const response = await service.readPublic(query);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ReadUserRequestPublicResponse = response.value;
      this.SET_USERREQUEST_PUBLIC(readResponse.UserRequest);
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
  public async UpdateUserRequest(): Promise<void> {
    AppModule.ShowLoading();
    const service = new UserRequestsService();
    const userRequest: UserRequest | undefined = this.UserRequest;
    if (!userRequest) {
      this.SET_ERROR({
        message:
          "UserRequestsStore: not expecting userRequest to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.update(userRequest);
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
      this.SET_USERREQUEST(userRequest);
      AppModule.SetSuccessNotifications("User Request successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async ApproveOrRejectUserRequest(): Promise<void> {
    AppModule.ShowLoading();
    const service = new UserRequestsService();
    const userRequest: UserRequest | undefined = this.UserRequest;
    if (!userRequest) {
      this.SET_ERROR({
        message:
          "UserRequestsStore: not expecting userRequest to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.approveOrReject(userRequest);
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
      this.SET_USERREQUEST(userRequest);
      AppModule.SetSuccessNotifications("Operation completed");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async PublicUpdateUserRequest(): Promise<void> {
    AppModule.ShowLoading();
    const service = new UserRequestsService();
    const userRequest: UserRequest | undefined = this.UserRequest;
    if (!userRequest) {
      this.SET_ERROR({
        message:
          "UserRequestsStore: not expecting userRequest to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.updatePublic(userRequest);
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
      this.SET_USERREQUEST(userRequest);
      AppModule.SetSuccessNotifications("User Request successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async DeleteUserRequest(): Promise<void> {
    AppModule.ShowLoading();
    const service = new UserRequestsService();
    const userRequest: UserRequest | undefined = this.UserRequest;
    if (!userRequest) {
      this.SET_ERROR({
        message:
          "UserRequestsStore: not expecting userRequest to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.delete(userRequest);
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
      const deleteUserRequestResponse: DeleteUserRequestResponse =
        response.value;
      this.SET_USERREQUEST(undefined);
      AppModule.SetSuccessNotifications("User Request successfully deleted");
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

  get UserRequest(): UserRequest | undefined {
    return this.userRequest.value;
  }

  get UserRequests(): UserRequest[] {
    return this.userRequests.value ?? new Array<UserRequest>();
  }

  get UserRequestPublic(): UserRequest | undefined {
    return this.userRequestPublic.value;
  }

  get UserRequestCreate(): UserRequest {
    return this.userRequestCreate.value;
  }

  get UserRequestPublicCreate(): UserRequest {
    return this.userRequestPublicCreate.value;
  }

  get emptyUserRequest(): UserRequest {
    return Object.create({
      Id: "",
      FirstName: "",
      LastName: "",
      Email: "",
      Purpose: "",
      TermsAndConditionAccepted: false,
      Status: 0,
      RoleId: "",
      CountryId: "",
      LaboratoryId: "",
      RecaptchaResponse: "",
    } as UserRequest);
  }
}

export const UserRequestModule = getModule(UserRequestStore, store);
