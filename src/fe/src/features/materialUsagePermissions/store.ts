import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { MaterialUsagePermission } from "@/models/MaterialUsagePermission";
import { MaterialUsagePermissionsService } from "@/services/materialUsagePermissions/MaterialUsagePermissionsService";
import { isLeft, isRight, Right } from "@/utils/either";
import { MaterialUsagePermissions } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListMaterialUsagePermissionResponse } from "@/services/materialUsagePermissions/models/ListMaterialUsagePermission";
import { CreateMaterialUsagePermissionResponse } from "@/services/materialUsagePermissions/models/CreateMaterialUsagePermission";
import { DeleteMaterialUsagePermissionResponse } from "@/services/materialUsagePermissions/models/DeleteMaterialUsagePermission";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface MaterialUsagePermissionState {
  MaterialUsagePermissionCreate: MaterialUsagePermission | undefined;
  MaterialUsagePermission: MaterialUsagePermission | undefined;
  MaterialUsagePermissions: Array<MaterialUsagePermission>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "MaterialUsagePermissions",
  store: store,
})
class MaterialUsagePermissionStore
  extends VuexModule
  implements MaterialUsagePermissionState
{
  // Private variables
  private materialUsagePermissionCreate: { value: MaterialUsagePermission } = {
    value: this.emptyMaterialUsagePermission,
  };

  private materialUsagePermission: {
    value: MaterialUsagePermission | undefined;
  } = {
    value: undefined,
  };

  private materialUsagePermissions: { value: Array<MaterialUsagePermission> } =
    {
      value: MaterialUsagePermissions,
    };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_MATERIALUSAGEPERMISSION_CREATE(
    MaterialUsagePermission: MaterialUsagePermission
  ): void {
    this.materialUsagePermissionCreate.value = MaterialUsagePermission;
  }

  // Details - Edit
  @Mutation
  public SET_MATERIALUSAGEPERMISSION(
    MaterialUsagePermission: MaterialUsagePermission | undefined
  ): void {
    this.materialUsagePermission.value = MaterialUsagePermission;
  }

  @Mutation
  public CLEAR_MATERIALUSAGEPERMISSION(): void {
    this.materialUsagePermission.value = undefined;
  }

  // List
  @Mutation
  public SET_MATERIALUSAGEPERMISSIONS(
    MaterialUsagePermissions: Array<MaterialUsagePermission>
  ): void {
    this.materialUsagePermissions.value = MaterialUsagePermissions;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateMaterialUsagePermission(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialUsagePermissionsService();
    const MaterialUsagePermission = this.materialUsagePermissionCreate.value;
    if (MaterialUsagePermission === undefined) {
      this.SET_ERROR({
        message:
          "MaterialUsagePermissionsStore: not expecting MaterialUsagePermission to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(MaterialUsagePermission);
    if (isLeft(response)) {
      const createResponse: CreateMaterialUsagePermissionResponse =
        response.value;
      MaterialUsagePermission.Id = createResponse.Id;
      this.SET_MATERIALUSAGEPERMISSION(MaterialUsagePermission);
      this.SET_MATERIALUSAGEPERMISSION_CREATE(
        this.emptyMaterialUsagePermission
      );
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListMaterialUsagePermissions(): Promise<void> {
    const service = new MaterialUsagePermissionsService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListMaterialUsagePermissionResponse = response.value;
      this.SET_MATERIALUSAGEPERMISSIONS(listResponse.MaterialUsagePermissions);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ListMaterialUsagePermissionsPublic(): Promise<void> {
    const service = new MaterialUsagePermissionsService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListMaterialUsagePermissionResponse = response.value;
      this.SET_MATERIALUSAGEPERMISSIONS(listResponse.MaterialUsagePermissions);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateMaterialUsagePermission(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialUsagePermissionsService();
    const MaterialUsagePermission: MaterialUsagePermission | undefined =
      this.MaterialUsagePermission;
    if (!MaterialUsagePermission) {
      this.SET_ERROR({
        message:
          "MaterialUsagePermissionsStore: not expecting MaterialUsagePermission to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(MaterialUsagePermission);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeleteMaterialUsagePermission(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialUsagePermissionsService();
    const MaterialUsagePermission: MaterialUsagePermission | undefined =
      this.MaterialUsagePermission;
    if (!MaterialUsagePermission) {
      this.SET_ERROR({
        message:
          "MaterialUsagePermissionsStore: not expecting MaterialUsagePermission to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(MaterialUsagePermission);
    if (isLeft(response)) {
      const deleteMaterialUsagePermissionResponse: DeleteMaterialUsagePermissionResponse =
        response.value;
      this.SET_MATERIALUSAGEPERMISSION(undefined);
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

  get MaterialUsagePermission(): MaterialUsagePermission | undefined {
    return this.materialUsagePermission.value;
  }

  get MaterialUsagePermissions(): MaterialUsagePermission[] {
    return (
      this.materialUsagePermissions.value ??
      new Array<MaterialUsagePermission>()
    );
  }

  get MaterialUsagePermissionCreate(): MaterialUsagePermission {
    return this.materialUsagePermissionCreate.value;
  }

  get emptyMaterialUsagePermission(): MaterialUsagePermission {
    return Object.create({
      Id: "",
      Name: "",
      Description: "",
      IsActive: false,
    });
  }
}

export const MaterialUsagePermissionModule = getModule(
  MaterialUsagePermissionStore,
  store
);
