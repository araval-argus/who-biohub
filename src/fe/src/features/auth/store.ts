import router from "@/router";
import store from "@/store";
import {
  VuexModule,
  Module,
  Mutation,
  Action,
  getModule,
} from "vuex-module-decorators";

import { AuthService } from "@/services/auth/AuthService";
import { isLeft, ParseError } from "@/utils/either";
import { GetAccessInformationResponse } from "@/services/auth/models/GetAccessInformation";
import { AppError } from "@/models/shared/Error";
import { RoleType } from "@/models/enums/RoleType";
import { UserLoginInfo } from "@/models/UserLoginInfo";
import { UserPermission } from "@/models/UserLoginInfo";

export interface AuthState {
  UserLoginInfo: UserLoginInfo | undefined;
  Error: AppError | undefined;
}

@Module({ namespaced: true, dynamic: true, store: store, name: "auth" })
class AuthStore extends VuexModule implements AuthState {
  private userLoginInfo: { value: UserLoginInfo | undefined } = {
    value: undefined,
  };

  private userToken: { value: string | undefined } = {
    value: undefined,
  };

  private error: { value: AppError | undefined } = { value: undefined };

  @Mutation
  public SET_USER_LOGIN_INFO(userLoginInfo: UserLoginInfo | undefined): void {
    this.userLoginInfo.value = userLoginInfo;
  }

  @Mutation
  public SET_USER_TOKEN(token: string | undefined): void {
    this.userToken.value = token;
  }

  @Action({ rawError: true })
  public async loginAsync(): Promise<void> {
    const service = new AuthService();
    await service.loginAsync();
  }

  @Action({ rawError: true })
  public async setUserTokenAsync(): Promise<void> {
    const service = new AuthService();
    const token = await service.getTokenAsync();
    this.SET_USER_TOKEN(token);
  }

  @Action({ rawError: true })
  public async setUserInfoAsync(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new AuthService();
    const response = await service.checkUserAccess({});
    if (isLeft(response)) {
      const accessInformationResponse: GetAccessInformationResponse =
        response.value;
      this.SET_USER_LOGIN_INFO(accessInformationResponse.UserLoginInfo);
      return;
    }
    this.SET_ERROR(response.value as AppError);
    this.SET_USER_LOGIN_INFO(undefined);

    if (
      response.value?.code == 401 &&
      response.value?.message["Message"] == "Token Expired"
    ) {
      await this.logoutAsync();
    } else if (response.value?.code == 401 || response.value?.code == 403) {
      this.SET_USER_LOGGED(true);
    }
    throw response.value;
  }

  @Action({ rawError: true })
  public async setLoginInfoAsync(): Promise<void> {
    this.SET_ERROR(undefined);
    const service = new AuthService();
    const response = await service.loginCheck({});
    if (isLeft(response)) {
      const accessInformationResponse: GetAccessInformationResponse =
        response.value;
      this.SET_USER_LOGIN_INFO(accessInformationResponse.UserLoginInfo);
      return;
    }
    this.SET_ERROR(response.value as AppError);
    this.SET_USER_LOGIN_INFO(undefined);

    if (
      response.value?.code == 401 &&
      response.value?.message["Message"] == "Token Expired"
    ) {
      await this.logoutAsync();
    } else if (response.value?.code == 401 || response.value?.code == 403) {
      this.SET_USER_LOGGED(true);
    }
    throw response.value;
  }

  @Mutation
  public SET_ERROR(error: AppError | undefined): void {
    error = ParseError(error);
    this.error.value = error;
  }

  @Action
  public async logoutAsync(): Promise<void> {
    this.LOGOUT();
    const service = new AuthService();
    await service.logoutAsync();
  }

  @Mutation
  public LOGOUT(): void {
    this.userLoginInfo.value = undefined;
    this.userToken.value = undefined;
  }

  @Mutation
  public SET_USER_LOGGED(userLogged: boolean): void {
    const userLoginInfo = Object.assign({
      UserLogged: userLogged,
    }) as UserLoginInfo;
    this.userLoginInfo.value = userLoginInfo;
  }

  get IsLogged(): boolean {
    const logged = this.UserLoginInfo?.UserLogged == true ? true : false;
    return logged;
  }

  get LoggedUserName(): string | undefined {
    return this.UserLoginInfo?.LoggedUserName ?? undefined;
  }

  get LoggedUserJobTitle(): string | undefined {
    return this.UserLoginInfo?.JobTitle ?? undefined;
  }

  get RoleType(): RoleType | undefined {
    return this.UserLoginInfo?.RoleType ?? undefined;
  }

  get LaboratoryId(): string | undefined {
    return this.UserLoginInfo?.LaboratoryId ?? "";
  }

  get BioHubFacilityId(): string | undefined {
    return this.UserLoginInfo?.BioHubFacilityId ?? "";
  }

  get RoleId(): string | undefined {
    return this.UserLoginInfo?.RoleId ?? "";
  }

  get RoleName(): string | undefined {
    return this.UserLoginInfo?.RoleName ?? "";
  }

  get RoleDescription(): string | undefined {
    return this.UserLoginInfo?.RoleDescription ?? "";
  }

  get UserId(): string {
    return this.UserLoginInfo?.UserId ?? "";
  }

  get JobTitle(): string | undefined {
    return this.UserLoginInfo?.JobTitle ?? undefined;
  }

  get BusinessPhone(): string | undefined {
    return this.UserLoginInfo?.BusinessPhone ?? undefined;
  }

  get MobilePhone(): string | undefined {
    return this.UserLoginInfo?.MobilePhone ?? undefined;
  }

  get FirstName(): string | undefined {
    return this.UserLoginInfo?.FirstName ?? undefined;
  }

  get LastName(): string | undefined {
    return this.UserLoginInfo?.LastName ?? undefined;
  }

  get Email(): string | undefined {
    return this.UserLoginInfo?.Email ?? undefined;
  }

  get LandingPage(): string | undefined {
    return this.UserLoginInfo?.LandingPage ?? "";
  }

  get Permissions(): Array<string> | undefined {
    return this.UserLoginInfo?.UserPermissions?.map(
      (ups) => ups.PermissionName
    );
  }

  get UserLoginInfo(): UserLoginInfo | undefined {
    return this.userLoginInfo.value;
  }

  get UserToken(): string | undefined {
    return this.userToken.value;
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get ErrorMessage(): any {
    return this.error.value?.message;
  }
}

export const AuthModule = getModule(AuthStore, store);
