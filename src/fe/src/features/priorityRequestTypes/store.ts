import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { PriorityRequestType } from "@/models/PriorityRequestType";
import { PriorityRequestTypesService } from "@/services/priorityRequestTypes/PriorityRequestTypesService";
import { isLeft, isRight, Right } from "@/utils/either";
import { PriorityRequestTypes } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListPriorityRequestTypeResponse } from "@/services/priorityRequestTypes/models/ListPriorityRequestType";
import { CreatePriorityRequestTypeResponse } from "@/services/priorityRequestTypes/models/CreatePriorityRequestType";
import { DeletePriorityRequestTypeResponse } from "@/services/priorityRequestTypes/models/DeletePriorityRequestType";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface PriorityRequestTypeState {
  PriorityRequestTypeCreate: PriorityRequestType | undefined;
  PriorityRequestType: PriorityRequestType | undefined;
  PriorityRequestTypes: Array<PriorityRequestType>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "PriorityRequestTypes",
  store: store,
})
class PriorityRequestTypeStore
  extends VuexModule
  implements PriorityRequestTypeState
{
  // Private variables
  private priorityRequestTypeCreate: { value: PriorityRequestType } = {
    value: this.emptyPriorityRequestType,
  };

  private priorityRequestType: { value: PriorityRequestType | undefined } = {
    value: undefined,
  };

  private priorityRequestTypes: { value: Array<PriorityRequestType> } = {
    value: PriorityRequestTypes,
  };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_PRIORITYREQUESTTYPE_CREATE(
    PriorityRequestType: PriorityRequestType
  ): void {
    this.priorityRequestTypeCreate.value = PriorityRequestType;
  }

  // Details - Edit
  @Mutation
  public SET_PRIORITYREQUESTTYPE(
    PriorityRequestType: PriorityRequestType | undefined
  ): void {
    this.priorityRequestType.value = PriorityRequestType;
  }

  @Mutation
  public CLEAR_PRIORITYREQUESTTYPE(): void {
    this.priorityRequestType.value = undefined;
  }

  // List
  @Mutation
  public SET_PRIORITYREQUESTTYPES(
    PriorityRequestTypes: Array<PriorityRequestType>
  ): void {
    this.priorityRequestTypes.value = PriorityRequestTypes;
  }

  // Actions
  @Action({ rawError: true })
  public async CreatePriorityRequestType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new PriorityRequestTypesService();
    const PriorityRequestType = this.priorityRequestTypeCreate.value;
    if (PriorityRequestType === undefined) {
      this.SET_ERROR({
        message:
          "PriorityRequestTypesStore: not expecting PriorityRequestType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(PriorityRequestType);
    if (isLeft(response)) {
      const createResponse: CreatePriorityRequestTypeResponse = response.value;
      PriorityRequestType.Id = createResponse.Id;
      this.SET_PRIORITYREQUESTTYPE(PriorityRequestType);
      this.SET_PRIORITYREQUESTTYPE_CREATE(this.emptyPriorityRequestType);
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListPriorityRequestTypes(): Promise<void> {
    const service = new PriorityRequestTypesService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListPriorityRequestTypeResponse = response.value;
      this.SET_PRIORITYREQUESTTYPES(listResponse.PriorityRequestTypes);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ListPriorityRequestTypesPublic(): Promise<void> {
    const service = new PriorityRequestTypesService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListPriorityRequestTypeResponse = response.value;
      this.SET_PRIORITYREQUESTTYPES(listResponse.PriorityRequestTypes);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdatePriorityRequestType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new PriorityRequestTypesService();
    const PriorityRequestType: PriorityRequestType | undefined =
      this.PriorityRequestType;
    if (!PriorityRequestType) {
      this.SET_ERROR({
        message:
          "PriorityRequestTypesStore: not expecting PriorityRequestType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(PriorityRequestType);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeletePriorityRequestType(): Promise<void> {
    AppModule.ShowLoading();
    const service = new PriorityRequestTypesService();
    const PriorityRequestType: PriorityRequestType | undefined =
      this.PriorityRequestType;
    if (!PriorityRequestType) {
      this.SET_ERROR({
        message:
          "PriorityRequestTypesStore: not expecting PriorityRequestType to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(PriorityRequestType);
    if (isLeft(response)) {
      const deletePriorityRequestTypeResponse: DeletePriorityRequestTypeResponse =
        response.value;
      this.SET_PRIORITYREQUESTTYPE(undefined);
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

  get PriorityRequestType(): PriorityRequestType | undefined {
    return this.priorityRequestType.value;
  }

  get PriorityRequestTypes(): PriorityRequestType[] {
    return this.priorityRequestTypes.value ?? new Array<PriorityRequestType>();
  }

  get PriorityRequestTypeCreate(): PriorityRequestType {
    return this.priorityRequestTypeCreate.value;
  }

  get emptyPriorityRequestType(): PriorityRequestType {
    return Object.create({
      Id: "",
      Name: "",
      HexColor: "",
      Description: "",
      IsActive: false,
    });
  }
}

export const PriorityRequestTypeModule = getModule(
  PriorityRequestTypeStore,
  store
);
