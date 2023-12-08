import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { CultivabilityType } from "@/models/CultivabilityType";
import { CultivabilityTypesService } from "@/services/cultivabilityTypes/CultivabilityTypesService";
import { isLeft, isRight, Right } from "@/utils/either";
import { CultivabilityTypes } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListCultivabilityTypeResponse } from "@/services/cultivabilityTypes/models/ListCultivabilityType";
import { CreateCultivabilityTypeResponse } from "@/services/cultivabilityTypes/models/CreateCultivabilityType";
import { DeleteCultivabilityTypeResponse } from "@/services/cultivabilityTypes/models/DeleteCultivabilityType";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface CultivabilityTypeState {
  CultivabilityTypeCreate: CultivabilityType | undefined;
  CultivabilityType: CultivabilityType | undefined;
  CultivabilityTypes: Array<CultivabilityType>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "CultivabilityTypes",
  store: store,
})
class CultivabilityTypeStore
  extends VuexModule
  implements CultivabilityTypeState
{
  // Private variables
  private cultivabilityTypeCreate: { value: CultivabilityType } = {
    value: this.emptyCultivabilityType,
  };

  private cultivabilityType: { value: CultivabilityType | undefined } = {
    value: undefined,
  };

  private cultivabilityTypes: { value: Array<CultivabilityType> } = {
    value: CultivabilityTypes,
  };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_CULTIVABILITYTYPE_CREATE(
    CultivabilityType: CultivabilityType
  ): void {
    this.cultivabilityTypeCreate.value = CultivabilityType;
  }

  // Details - Edit
  @Mutation
  public SET_CULTIVABILITYTYPE(
    CultivabilityType: CultivabilityType | undefined
  ): void {
    this.cultivabilityType.value = CultivabilityType;
  }

  @Mutation
  public CLEAR_CULTIVABILITYTYPE(): void {
    this.cultivabilityType.value = undefined;
  }

  // List
  @Mutation
  public SET_CULTIVABILITYTYPES(
    CultivabilityTypes: Array<CultivabilityType>
  ): void {
    this.cultivabilityTypes.value = CultivabilityTypes;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateCultivabilityType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new CultivabilityTypesService();
    const CultivabilityType = this.cultivabilityTypeCreate.value;
    if (CultivabilityType === undefined) {
      this.SET_ERROR({
        message:
          "CultivabilityTypesStore: not expecting CultivabilityType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(CultivabilityType);
    if (isLeft(response)) {
      const createResponse: CreateCultivabilityTypeResponse = response.value;
      CultivabilityType.Id = createResponse.Id;
      this.SET_CULTIVABILITYTYPE(CultivabilityType);
      this.SET_CULTIVABILITYTYPE_CREATE(this.emptyCultivabilityType);
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListCultivabilityTypes(): Promise<void> {
    const service = new CultivabilityTypesService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListCultivabilityTypeResponse = response.value;
      this.SET_CULTIVABILITYTYPES(listResponse.CultivabilityTypes);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateCultivabilityType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new CultivabilityTypesService();
    const CultivabilityType: CultivabilityType | undefined =
      this.CultivabilityType;
    if (!CultivabilityType) {
      this.SET_ERROR({
        message:
          "CultivabilityTypesStore: not expecting CultivabilityType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(CultivabilityType);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeleteCultivabilityType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new CultivabilityTypesService();
    const CultivabilityType: CultivabilityType | undefined =
      this.CultivabilityType;
    if (!CultivabilityType) {
      this.SET_ERROR({
        message:
          "CultivabilityTypesStore: not expecting CultivabilityType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(CultivabilityType);
    if (isLeft(response)) {
      const deleteCultivabilityTypeResponse: DeleteCultivabilityTypeResponse =
        response.value;
      this.SET_CULTIVABILITYTYPE(undefined);
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

  get CultivabilityType(): CultivabilityType | undefined {
    return this.cultivabilityType.value;
  }

  get CultivabilityTypes(): CultivabilityType[] {
    return this.cultivabilityTypes.value ?? new Array<CultivabilityType>();
  }

  get CultivabilityTypeCreate(): CultivabilityType {
    return this.cultivabilityTypeCreate.value;
  }

  get emptyCultivabilityType(): CultivabilityType {
    return Object.create({
      Id: "",
      Name: "",
      Description: "",
      IsActive: false,
    });
  }
}

export const CultivabilityTypeModule = getModule(CultivabilityTypeStore, store);
