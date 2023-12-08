import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { UserRequestStatus } from "@/models/UserRequestStatus";
import { isLeft, ParseError } from "@/utils/either";
import { AppError } from "@/models/shared/Error";
import { AppModule } from "../../store/MainStore";
import { UserRegistrationStatus } from "@/models/enums/UserRegistrationStatus";
import { UserRequestStatusesService } from "@/services/userRequestStatuses/UserRequestStatusesService";
import {
  ReadUserRequestStatusQuery,
  ReadUserRequestStatusQueryResponse,
} from "@/services/userRequestStatuses/models/ReadUserRequestStatus";
import {
  ReadUserRequestStatusByStatusQuery,
  ReadUserRequestStatusByStatusQueryResponse,
} from "@/services/userRequestStatuses/models/ReadUserRequestStatusByStatus";
import { ListUserRequestStatusesResponse } from "@/services/userRequestStatuses/models/ListUserRequestStatuses";

export interface UserRequestStatusState {
  UserRequestStatus: UserRequestStatus | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "userRequestStatuses",
  store: store,
})
class UserRequestStatusStore
  extends VuexModule
  implements UserRequestStatusState
{
  // Private variables
  private userRequestStatus: { value: UserRequestStatus | undefined } = {
    value: undefined,
  };

  private userRequestStatuses: { value: UserRequestStatus[] | undefined } = {
    value: undefined,
  };

  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError | undefined): void {
    error = ParseError(error);
    this.error.value = error;
  }

  // Details - Edit
  @Mutation
  public SET_USERREQUESTSTATUS(
    userRequestStatus: UserRequestStatus | undefined
  ): void {
    this.userRequestStatus.value = userRequestStatus;
  }

  @Mutation
  public SET_USERREQUESTSTATUSES(
    userRequestStatuses: UserRequestStatus[] | undefined
  ): void {
    this.userRequestStatuses.value = userRequestStatuses;
  }

  @Mutation
  public CLEAR_USERREQUESTSTATUS(): void {
    this.userRequestStatus.value = undefined;
  }

  @Mutation
  public CLEAR_USERREQUESTSTATUSES(): void {
    this.userRequestStatuses.value = undefined;
  }

  // Actions
  @Action({ rawError: true })
  public async ReadUserRequestStatus(id: string): Promise<void> {
    const service = new UserRequestStatusesService();
    const query: ReadUserRequestStatusQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ReadUserRequestStatusQueryResponse = response.value;
      this.SET_USERREQUESTSTATUS(readResponse.UserRequestStatus);
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
  public async ReadUserRequestStatusByStatus(
    status: UserRegistrationStatus
  ): Promise<void> {
    const service = new UserRequestStatusesService();
    const query: ReadUserRequestStatusByStatusQuery = { Status: status };
    const response = await service.readByStatus(query);
    if (isLeft(response)) {
      this.SET_ERROR(undefined);
      const readResponse: ReadUserRequestStatusByStatusQueryResponse =
        response.value;
      this.SET_USERREQUESTSTATUS(readResponse.UserRequestStatus);
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
  public async ListUserRequestStatuses(): Promise<void> {
    const service = new UserRequestStatusesService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListUserRequestStatusesResponse = response.value;
      this.SET_USERREQUESTSTATUSES(listResponse.UserRequestStatuses);
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

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get ErrorMessage(): any {
    return this.error.value?.message;
  }

  get UserRequestStatus(): UserRequestStatus | undefined {
    return this.userRequestStatus.value;
  }

  get UserRequestStatuses(): UserRequestStatus[] | undefined {
    return this.userRequestStatuses.value;
  }

  get emptyUserRequestStatus(): UserRequestStatus {
    return Object.create({
      IsResponseMessage: false,
      Message: "",
      Status: UserRegistrationStatus.Pending,
    } as UserRequestStatus);
  }
}

export const UserRequestStatusModule = getModule(UserRequestStatusStore, store);
