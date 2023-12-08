import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { MaterialType } from "@/models/MaterialType";
import { MaterialTypesService } from "@/services/materialTypes/MaterialTypesService";
import { isLeft, isRight, Right } from "@/utils/either";
import { MaterialTypes } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListMaterialTypeResponse } from "@/services/materialTypes/models/ListMaterialType";
import { CreateMaterialTypeResponse } from "@/services/materialTypes/models/CreateMaterialType";
import { DeleteMaterialTypeResponse } from "@/services/materialTypes/models/DeleteMaterialType";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface MaterialTypeState {
  MaterialTypeCreate: MaterialType | undefined;
  MaterialType: MaterialType | undefined;
  MaterialTypes: Array<MaterialType>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "MaterialTypes",
  store: store,
})
class MaterialTypeStore extends VuexModule implements MaterialTypeState {
  // Private variables
  private materialTypeCreate: { value: MaterialType } = {
    value: this.emptyMaterialType,
  };

  private MATERIALTYPE: { value: MaterialType | undefined } = {
    value: undefined,
  };

  private materialTypes: { value: Array<MaterialType> } = {
    value: MaterialTypes,
  };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_MATERIALTYPE_CREATE(MaterialType: MaterialType): void {
    this.materialTypeCreate.value = MaterialType;
  }

  // Details - Edit
  @Mutation
  public SET_MATERIALTYPE(MaterialType: MaterialType | undefined): void {
    this.MATERIALTYPE.value = MaterialType;
  }

  @Mutation
  public CLEAR_MATERIALTYPE(): void {
    this.MATERIALTYPE.value = undefined;
  }

  // List
  @Mutation
  public SET_MATERIALTYPES(MaterialTypes: Array<MaterialType>): void {
    this.materialTypes.value = MaterialTypes;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateMaterialType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialTypesService();
    const MaterialType = this.materialTypeCreate.value;
    if (MaterialType === undefined) {
      this.SET_ERROR({
        message:
          "MaterialTypesStore: not expecting MaterialType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(MaterialType);
    if (isLeft(response)) {
      const createResponse: CreateMaterialTypeResponse = response.value;
      MaterialType.Id = createResponse.Id;
      this.SET_MATERIALTYPE(MaterialType);
      this.SET_MATERIALTYPE_CREATE(this.emptyMaterialType);
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListMaterialTypes(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialTypesService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListMaterialTypeResponse = response.value;
      this.SET_MATERIALTYPES(listResponse.MaterialTypes);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ListMaterialTypesPublic(): Promise<void> {
    const service = new MaterialTypesService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListMaterialTypeResponse = response.value;
      this.SET_MATERIALTYPES(listResponse.MaterialTypes);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateMaterialType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialTypesService();
    const MATERIALTYPE: MaterialType | undefined = this.MaterialType;
    if (!MATERIALTYPE) {
      this.SET_ERROR({
        message:
          "MaterialTypesStore: not expecting MaterialType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(MATERIALTYPE);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeleteMaterialType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new MaterialTypesService();
    const MaterialType: MaterialType | undefined = this.MaterialType;
    if (!MaterialType) {
      this.SET_ERROR({
        message:
          "MaterialTypesStore: not expecting MaterialType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(MaterialType);
    if (isLeft(response)) {
      const deleteMaterialTypeResponse: DeleteMaterialTypeResponse =
        response.value;
      this.SET_MATERIALTYPE(undefined);
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

  get MaterialType(): MaterialType | undefined {
    return this.MATERIALTYPE.value;
  }

  get MaterialTypes(): MaterialType[] {
    return this.materialTypes.value ?? new Array<MaterialType>();
  }

  get MaterialTypeCreate(): MaterialType {
    return this.materialTypeCreate.value;
  }

  get emptyMaterialType(): MaterialType {
    return Object.create({
      Id: "",
      Name: "",
      Description: "",
      IsActive: false,
    });
  }
}

export const MaterialTypeModule = getModule(MaterialTypeStore, store);
