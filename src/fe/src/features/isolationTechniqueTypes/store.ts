import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { IsolationTechniqueType } from "@/models/IsolationTechniqueType";
import { IsolationTechniqueTypesService } from "@/services/isolationTechniqueTypes/IsolationTechniqueTypesService";
import { isLeft, isRight, Right } from "@/utils/either";
import { IsolationTechniqueTypes } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListIsolationTechniqueTypeResponse } from "@/services/isolationTechniqueTypes/models/ListIsolationTechniqueType";
import { CreateIsolationTechniqueTypeResponse } from "@/services/isolationTechniqueTypes/models/CreateIsolationTechniqueType";
import { DeleteIsolationTechniqueTypeResponse } from "@/services/isolationTechniqueTypes/models/DeleteIsolationTechniqueType";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface IsolationTechniqueTypeState {
  IsolationTechniqueTypeCreate: IsolationTechniqueType | undefined;
  IsolationTechniqueType: IsolationTechniqueType | undefined;
  IsolationTechniqueTypes: Array<IsolationTechniqueType>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "IsolationTechniqueTypes",
  store: store,
})
class IsolationTechniqueTypeStore
  extends VuexModule
  implements IsolationTechniqueTypeState
{
  // Private variables
  private isolationTechniqueTypeCreate: { value: IsolationTechniqueType } = {
    value: this.emptyIsolationTechniqueType,
  };

  private isolationTechniqueType: {
    value: IsolationTechniqueType | undefined;
  } = {
    value: undefined,
  };

  private isolationTechniqueTypes: { value: Array<IsolationTechniqueType> } = {
    value: IsolationTechniqueTypes,
  };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_ISOLATIONTECHNIQUETYPE_CREATE(
    IsolationTechniqueType: IsolationTechniqueType
  ): void {
    this.isolationTechniqueTypeCreate.value = IsolationTechniqueType;
  }

  // Details - Edit
  @Mutation
  public SET_ISOLATIONTECHNIQUETYPE(
    IsolationTechniqueType: IsolationTechniqueType | undefined
  ): void {
    this.isolationTechniqueType.value = IsolationTechniqueType;
  }

  @Mutation
  public CLEAR_ISOLATIONTECHNIQUETYPE(): void {
    this.isolationTechniqueType.value = undefined;
  }

  // List
  @Mutation
  public SET_ISOLATIONTECHNIQUETYPES(
    IsolationTechniqueTypes: Array<IsolationTechniqueType>
  ): void {
    this.isolationTechniqueTypes.value = IsolationTechniqueTypes;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateIsolationTechniqueType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new IsolationTechniqueTypesService();
    const IsolationTechniqueType = this.isolationTechniqueTypeCreate.value;
    if (IsolationTechniqueType === undefined) {
      this.SET_ERROR({
        message:
          "IsolationTechniqueTypesStore: not expecting IsolationTechniqueType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(IsolationTechniqueType);
    if (isLeft(response)) {
      const createResponse: CreateIsolationTechniqueTypeResponse =
        response.value;
      IsolationTechniqueType.Id = createResponse.Id;
      this.SET_ISOLATIONTECHNIQUETYPE(IsolationTechniqueType);
      this.SET_ISOLATIONTECHNIQUETYPE_CREATE(this.emptyIsolationTechniqueType);
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListIsolationTechniqueTypes(): Promise<void> {
    const service = new IsolationTechniqueTypesService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListIsolationTechniqueTypeResponse = response.value;
      this.SET_ISOLATIONTECHNIQUETYPES(listResponse.IsolationTechniqueTypes);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateIsolationTechniqueType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new IsolationTechniqueTypesService();
    const IsolationTechniqueType: IsolationTechniqueType | undefined =
      this.IsolationTechniqueType;
    if (!IsolationTechniqueType) {
      this.SET_ERROR({
        message:
          "IsolationTechniqueTypesStore: not expecting IsolationTechniqueType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(IsolationTechniqueType);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeleteIsolationTechniqueType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new IsolationTechniqueTypesService();
    const IsolationTechniqueType: IsolationTechniqueType | undefined =
      this.IsolationTechniqueType;
    if (!IsolationTechniqueType) {
      this.SET_ERROR({
        message:
          "IsolationTechniqueTypesStore: not expecting IsolationTechniqueType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(IsolationTechniqueType);
    if (isLeft(response)) {
      const deleteIsolationTechniqueTypeResponse: DeleteIsolationTechniqueTypeResponse =
        response.value;
      this.SET_ISOLATIONTECHNIQUETYPE(undefined);
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

  get IsolationTechniqueType(): IsolationTechniqueType | undefined {
    return this.isolationTechniqueType.value;
  }

  get IsolationTechniqueTypes(): IsolationTechniqueType[] {
    return (
      this.isolationTechniqueTypes.value ?? new Array<IsolationTechniqueType>()
    );
  }

  get IsolationTechniqueTypeCreate(): IsolationTechniqueType {
    return this.isolationTechniqueTypeCreate.value;
  }

  get emptyIsolationTechniqueType(): IsolationTechniqueType {
    return Object.create({
      Id: "",
      Name: "",
      Description: "",
      IsActive: false,
    });
  }
}

export const IsolationTechniqueTypeModule = getModule(
  IsolationTechniqueTypeStore,
  store
);
