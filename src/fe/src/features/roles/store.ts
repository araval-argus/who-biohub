import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { Role } from "@/models/Role";
import { RolePublic } from "@/models/RolePublic";
import { RolesService } from "@/services/roles/RolesService";
import { isLeft, isRight, Right } from "@/utils/either";
import { Roles } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListRoleResponse } from "@/services/roles/models/ListRole";
import { ListRolePublicResponse } from "@/services/roles/models/ListRolePublic";
import { CreateRoleResponse } from "@/services/roles/models/CreateRole";
import { DeleteRoleResponse } from "@/services/roles/models/DeleteRole";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface RoleState {
  RoleCreate: Role | undefined;
  Role: Role | undefined;
  RolesPublic: Array<RolePublic>;
  Roles: Array<Role>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "Roles",
  store: store,
})
class RoleStore extends VuexModule implements RoleState {
  // Private variables
  private roleStoreCreate: {
    value: Role;
  } = {
    value: this.emptyRole,
  };

  private roleStore: {
    value: Role | undefined;
  } = {
    value: undefined,
  };

  private roleStores: {
    value: Array<Role>;
  } = {
    value: Roles,
  };

  private rolePublicStores: {
    value: Array<RolePublic>;
  } = {
    value: Roles,
  };

  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_ROLE_CREATE(Role: Role): void {
    this.roleStoreCreate.value = Role;
  }

  // Details - Edit
  @Mutation
  public SET_ROLE(Role: Role | undefined): void {
    this.roleStore.value = Role;
  }

  @Mutation
  public CLEAR_ROLE(): void {
    this.roleStore.value = undefined;
  }

  // List
  @Mutation
  public SET_ROLES(Roles: Array<Role>): void {
    this.roleStores.value = Roles;
  }

  @Mutation
  public SET_ROLES_PUBLIC(Roles: Array<RolePublic>): void {
    this.rolePublicStores.value = Roles;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateRole(): Promise<void> {
    AppModule.ShowLoading();
    const service = new RolesService();
    const Role = this.roleStoreCreate.value;
    if (Role === undefined) {
      this.SET_ERROR({
        message:
          "RolesStore: not expecting Role to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(Role);
    if (isLeft(response)) {
      const createResponse: CreateRoleResponse = response.value;
      Role.Id = createResponse.Id;
      this.SET_ROLE(Role);
      this.SET_ROLE_CREATE(this.emptyRole);
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListRoles(): Promise<void> {
    const service = new RolesService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListRoleResponse = response.value;
      this.SET_ROLES(listResponse.Roles);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ListRolesPublic(): Promise<void> {
    const service = new RolesService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListRolePublicResponse = response.value;
      this.SET_ROLES_PUBLIC(listResponse.Roles);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateRole(): Promise<void> {
    AppModule.ShowLoading();
    const service = new RolesService();
    const Role: Role | undefined = this.Role;
    if (!Role) {
      this.SET_ERROR({
        message:
          "RolesStore: not expecting Role to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(Role);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeleteRole(): Promise<void> {
    AppModule.ShowLoading();
    const service = new RolesService();
    const Role: Role | undefined = this.Role;
    if (!Role) {
      this.SET_ERROR({
        message:
          "RolesStore: not expecting Role to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(Role);
    if (isLeft(response)) {
      const deleteRoleResponse: DeleteRoleResponse = response.value;
      this.SET_ROLE(undefined);
      AppModule.HideLoading();
      return;
    }

    const error = (response as Right<CommunicationError>).value;
    this.SET_ERROR(error as AppError);
    AppModule.HideLoading();
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get Role(): Role | undefined {
    return this.roleStore.value;
  }

  get Roles(): Role[] {
    return this.roleStores.value ?? new Array<Role>();
  }

  get RolesPublic(): RolePublic[] {
    return this.rolePublicStores.value ?? new Array<RolePublic>();
  }

  get RoleCreate(): Role {
    return this.roleStoreCreate.value;
  }

  get emptyRole(): Role {
    return Object.create({
      Id: "",
      Name: "",
      Description: "",
      RoleType: 0,
      AddToRegistration: false,
    });
  }
}

export const RoleModule = getModule(RoleStore, store);
