import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { TemperatureUnitOfMeasure } from "@/models/TemperatureUnitOfMeasure";
import { TemperatureUnitOfMeasuresService } from "@/services/temperatureUnitOfMeasures/TemperatureUnitOfMeasuresService";
import { isLeft, isRight, Right } from "@/utils/either";
import { TemperatureUnitOfMeasures } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListTemperatureUnitOfMeasureResponse } from "@/services/temperatureUnitOfMeasures/models/ListTemperatureUnitOfMeasure";
import { CreateTemperatureUnitOfMeasureResponse } from "@/services/temperatureUnitOfMeasures/models/CreateTemperatureUnitOfMeasure";
import { DeleteTemperatureUnitOfMeasureResponse } from "@/services/temperatureUnitOfMeasures/models/DeleteTemperatureUnitOfMeasure";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface TemperatureUnitOfMeasureState {
  TemperatureUnitOfMeasureCreate: TemperatureUnitOfMeasure | undefined;
  TemperatureUnitOfMeasure: TemperatureUnitOfMeasure | undefined;
  TemperatureUnitOfMeasures: Array<TemperatureUnitOfMeasure>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "TemperatureUnitOfMeasures",
  store: store,
})
class TemperatureUnitOfMeasureStore
  extends VuexModule
  implements TemperatureUnitOfMeasureState
{
  // Private variables
  private temperatureUnitOfMeasureStoreCreate: {
    value: TemperatureUnitOfMeasure;
  } = {
    value: this.emptyTemperatureUnitOfMeasure,
  };

  private temperatureUnitOfMeasureStore: {
    value: TemperatureUnitOfMeasure | undefined;
  } = {
    value: undefined,
  };

  private temperatureUnitOfMeasureStores: {
    value: Array<TemperatureUnitOfMeasure>;
  } = {
    value: TemperatureUnitOfMeasures,
  };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_TEMPERATUREUNITOFMEASURE_CREATE(
    TemperatureUnitOfMeasure: TemperatureUnitOfMeasure
  ): void {
    this.temperatureUnitOfMeasureStoreCreate.value = TemperatureUnitOfMeasure;
  }

  // Details - Edit
  @Mutation
  public SET_TEMPERATUREUNITOFMEASURE(
    TemperatureUnitOfMeasure: TemperatureUnitOfMeasure | undefined
  ): void {
    this.temperatureUnitOfMeasureStore.value = TemperatureUnitOfMeasure;
  }

  @Mutation
  public CLEAR_TEMPERATUREUNITOFMEASURE(): void {
    this.temperatureUnitOfMeasureStore.value = undefined;
  }

  // List
  @Mutation
  public SET_TEMPERATUREUNITOFMEASURES(
    TemperatureUnitOfMeasures: Array<TemperatureUnitOfMeasure>
  ): void {
    this.temperatureUnitOfMeasureStores.value = TemperatureUnitOfMeasures;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateTemperatureUnitOfMeasure(): Promise<void> {
    AppModule.ShowLoading();
    const service = new TemperatureUnitOfMeasuresService();
    const TemperatureUnitOfMeasure =
      this.temperatureUnitOfMeasureStoreCreate.value;
    if (TemperatureUnitOfMeasure === undefined) {
      this.SET_ERROR({
        message:
          "TemperatureUnitOfMeasuresStore: not expecting TemperatureUnitOfMeasure to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(TemperatureUnitOfMeasure);
    if (isLeft(response)) {
      const createResponse: CreateTemperatureUnitOfMeasureResponse =
        response.value;
      TemperatureUnitOfMeasure.Id = createResponse.Id;
      this.SET_TEMPERATUREUNITOFMEASURE(TemperatureUnitOfMeasure);
      this.SET_TEMPERATUREUNITOFMEASURE_CREATE(
        this.emptyTemperatureUnitOfMeasure
      );
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListTemperatureUnitOfMeasures(): Promise<void> {
    const service = new TemperatureUnitOfMeasuresService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListTemperatureUnitOfMeasureResponse = response.value;
      this.SET_TEMPERATUREUNITOFMEASURES(
        listResponse.TemperatureUnitOfMeasures
      );
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ListTemperatureUnitOfMeasuresPublic(): Promise<void> {
    const service = new TemperatureUnitOfMeasuresService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListTemperatureUnitOfMeasureResponse = response.value;
      this.SET_TEMPERATUREUNITOFMEASURES(
        listResponse.TemperatureUnitOfMeasures
      );
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateTemperatureUnitOfMeasure(): Promise<void> {
    AppModule.ShowLoading();
    const service = new TemperatureUnitOfMeasuresService();
    const TemperatureUnitOfMeasure: TemperatureUnitOfMeasure | undefined =
      this.TemperatureUnitOfMeasure;
    if (!TemperatureUnitOfMeasure) {
      this.SET_ERROR({
        message:
          "TemperatureUnitOfMeasuresStore: not expecting TemperatureUnitOfMeasure to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(TemperatureUnitOfMeasure);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeleteTemperatureUnitOfMeasure(): Promise<void> {
    AppModule.ShowLoading();
    const service = new TemperatureUnitOfMeasuresService();
    const TemperatureUnitOfMeasure: TemperatureUnitOfMeasure | undefined =
      this.TemperatureUnitOfMeasure;
    if (!TemperatureUnitOfMeasure) {
      this.SET_ERROR({
        message:
          "TemperatureUnitOfMeasuresStore: not expecting TemperatureUnitOfMeasure to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(TemperatureUnitOfMeasure);
    if (isLeft(response)) {
      const deleteTemperatureUnitOfMeasureResponse: DeleteTemperatureUnitOfMeasureResponse =
        response.value;
      this.SET_TEMPERATUREUNITOFMEASURE(undefined);
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

  get TemperatureUnitOfMeasure(): TemperatureUnitOfMeasure | undefined {
    return this.temperatureUnitOfMeasureStore.value;
  }

  get TemperatureUnitOfMeasures(): TemperatureUnitOfMeasure[] {
    return (
      this.temperatureUnitOfMeasureStores.value ??
      new Array<TemperatureUnitOfMeasure>()
    );
  }

  get TemperatureUnitOfMeasureCreate(): TemperatureUnitOfMeasure {
    return this.temperatureUnitOfMeasureStoreCreate.value;
  }

  get emptyTemperatureUnitOfMeasure(): TemperatureUnitOfMeasure {
    return Object.create({
      Id: "",
      Name: "",
      Unit: "",
    });
  }
}

export const TemperatureUnitOfMeasureModule = getModule(
  TemperatureUnitOfMeasureStore,
  store
);
