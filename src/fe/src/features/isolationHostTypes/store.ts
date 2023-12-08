import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { IsolationHostType } from "@/models/IsolationHostType";
import { IsolationHostTypesService } from "@/services/isolationHostTypes/IsolationHostTypesService";
import { isLeft, isRight, Right } from "@/utils/either";
import { IsolationHostTypes } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListIsolationHostTypeResponse } from "@/services/isolationHostTypes/models/ListIsolationHostType";
import { CreateIsolationHostTypeResponse } from "@/services/isolationHostTypes/models/CreateIsolationHostType";
import { DeleteIsolationHostTypeResponse } from "@/services/isolationHostTypes/models/DeleteIsolationHostType";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface IsolationHostTypeState {
  IsolationHostTypeCreate: IsolationHostType | undefined;
  IsolationHostType: IsolationHostType | undefined;
  IsolationHostTypes: Array<IsolationHostType>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "IsolationHostTypes",
  store: store,
})
class IsolationHostTypeStore
  extends VuexModule
  implements IsolationHostTypeState
{
  // Private variables
  private isolationHostTypeCreate: { value: IsolationHostType } = {
    value: this.emptyIsolationHostType,
  };

  private isolationHostType: { value: IsolationHostType | undefined } = {
    value: undefined,
  };

  private isolationHostTypes: { value: Array<IsolationHostType> } = {
    value: IsolationHostTypes,
  };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_ISOLATIONHOSTTYPE_CREATE(
    IsolationHostType: IsolationHostType
  ): void {
    this.isolationHostTypeCreate.value = IsolationHostType;
  }

  // Details - Edit
  @Mutation
  public SET_ISOLATIONHOSTTYPE(
    IsolationHostType: IsolationHostType | undefined
  ): void {
    this.isolationHostType.value = IsolationHostType;
  }

  @Mutation
  public CLEAR_ISOLATIONHOSTTYPE(): void {
    this.isolationHostType.value = undefined;
  }

  // List
  @Mutation
  public SET_ISOLATIONHOSTTYPES(
    IsolationHostTypes: Array<IsolationHostType>
  ): void {
    this.isolationHostTypes.value = IsolationHostTypes;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateIsolationHostType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new IsolationHostTypesService();
    const IsolationHostType = this.isolationHostTypeCreate.value;
    if (IsolationHostType === undefined) {
      this.SET_ERROR({
        message:
          "IsolationHostTypesStore: not expecting IsolationHostType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(IsolationHostType);
    if (isLeft(response)) {
      const createResponse: CreateIsolationHostTypeResponse = response.value;
      IsolationHostType.Id = createResponse.Id;
      this.SET_ISOLATIONHOSTTYPE(IsolationHostType);
      this.SET_ISOLATIONHOSTTYPE_CREATE(this.emptyIsolationHostType);
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListIsolationHostTypes(): Promise<void> {
    const service = new IsolationHostTypesService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListIsolationHostTypeResponse = response.value;
      this.SET_ISOLATIONHOSTTYPES(listResponse.IsolationHostTypes);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ListIsolationHostTypesPublic(): Promise<void> {
    const service = new IsolationHostTypesService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListIsolationHostTypeResponse = response.value;
      this.SET_ISOLATIONHOSTTYPES(listResponse.IsolationHostTypes);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateIsolationHostType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new IsolationHostTypesService();
    const IsolationHostType: IsolationHostType | undefined =
      this.IsolationHostType;
    if (!IsolationHostType) {
      this.SET_ERROR({
        message:
          "IsolationHostTypesStore: not expecting IsolationHostType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(IsolationHostType);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeleteIsolationHostType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new IsolationHostTypesService();
    const IsolationHostType: IsolationHostType | undefined =
      this.IsolationHostType;
    if (!IsolationHostType) {
      this.SET_ERROR({
        message:
          "IsolationHostTypesStore: not expecting IsolationHostType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(IsolationHostType);
    if (isLeft(response)) {
      const deleteIsolationHostTypeResponse: DeleteIsolationHostTypeResponse =
        response.value;
      this.SET_ISOLATIONHOSTTYPE(undefined);
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

  get IsolationHostType(): IsolationHostType | undefined {
    return this.isolationHostType.value;
  }

  get IsolationHostTypes(): IsolationHostType[] {
    return this.isolationHostTypes.value ?? new Array<IsolationHostType>();
  }

  get IsolationHostTypeCreate(): IsolationHostType {
    return this.isolationHostTypeCreate.value;
  }

  get emptyIsolationHostType(): IsolationHostType {
    return Object.create({
      Id: "",
      Name: "",
      Description: "",
      IsActive: false,
    });
  }
}

export const IsolationHostTypeModule = getModule(IsolationHostTypeStore, store);
