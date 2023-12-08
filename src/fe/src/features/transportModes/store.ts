import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { TransportMode } from "@/models/TransportMode";
import { TransportModesService } from "@/services/transportModes/TransportModesService";
import { isLeft, isRight, Right } from "@/utils/either";
import { TransportModes } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListTransportModeResponse } from "@/services/transportModes/models/ListTransportMode";
import { CreateTransportModeResponse } from "@/services/transportModes/models/CreateTransportMode";
import { DeleteTransportModeResponse } from "@/services/transportModes/models/DeleteTransportMode";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface TransportModeState {
  TransportModeCreate: TransportMode | undefined;
  TransportMode: TransportMode | undefined;
  TransportModes: Array<TransportMode>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "TransportModes",
  store: store,
})
class TransportModeStore extends VuexModule implements TransportModeState {
  // Private variables
  private transportModeCreate: { value: TransportMode } = {
    value: this.emptyTransportMode,
  };

  private transportMode: { value: TransportMode | undefined } = {
    value: undefined,
  };

  private transportModes: { value: Array<TransportMode> } = {
    value: TransportModes,
  };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_TRANSPORTMODE_CREATE(TransportMode: TransportMode): void {
    this.transportModeCreate.value = TransportMode;
  }

  // Details - Edit
  @Mutation
  public SET_TRANSPORTMODE(TransportMode: TransportMode | undefined): void {
    this.transportMode.value = TransportMode;
  }

  @Mutation
  public CLEAR_TRANSPORTMODE(): void {
    this.transportMode.value = undefined;
  }

  // List
  @Mutation
  public SET_TRANSPORTMODES(TransportModes: Array<TransportMode>): void {
    this.transportModes.value = TransportModes;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateTransportMode(): Promise<void> {
    AppModule.ShowLoading();
    const service = new TransportModesService();
    const TransportMode = this.transportModeCreate.value;
    if (TransportMode === undefined) {
      this.SET_ERROR({
        message:
          "TransportModesStore: not expecting TransportMode to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(TransportMode);
    if (isLeft(response)) {
      const createResponse: CreateTransportModeResponse = response.value;
      TransportMode.Id = createResponse.Id;
      this.SET_TRANSPORTMODE(TransportMode);
      this.SET_TRANSPORTMODE_CREATE(this.emptyTransportMode);
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListTransportModes(): Promise<void> {
    const service = new TransportModesService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListTransportModeResponse = response.value;
      this.SET_TRANSPORTMODES(listResponse.TransportModes);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ListTransportModesPublic(): Promise<void> {
    const service = new TransportModesService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListTransportModeResponse = response.value;
      this.SET_TRANSPORTMODES(listResponse.TransportModes);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateTransportMode(): Promise<void> {
    AppModule.ShowLoading();
    const service = new TransportModesService();
    const TransportMode: TransportMode | undefined = this.TransportMode;
    if (!TransportMode) {
      this.SET_ERROR({
        message:
          "TransportModesStore: not expecting TransportMode to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(TransportMode);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeleteTransportMode(): Promise<void> {
    AppModule.ShowLoading();
    const service = new TransportModesService();
    const TransportMode: TransportMode | undefined = this.TransportMode;
    if (!TransportMode) {
      this.SET_ERROR({
        message:
          "TransportModesStore: not expecting TransportMode to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(TransportMode);
    if (isLeft(response)) {
      const deleteTransportModeResponse: DeleteTransportModeResponse =
        response.value;
      this.SET_TRANSPORTMODE(undefined);
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

  get TransportMode(): TransportMode | undefined {
    return this.transportMode.value;
  }

  get TransportModes(): TransportMode[] {
    return this.transportModes.value ?? new Array<TransportMode>();
  }

  get TransportModeCreate(): TransportMode {
    return this.transportModeCreate.value;
  }

  get emptyTransportMode(): TransportMode {
    return Object.create({
      Id: "",
      Name: "",
      HexColor: "",
      Description: "",
      IsActive: false,
    });
  }
}

export const TransportModeModule = getModule(TransportModeStore, store);
