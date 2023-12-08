import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { GeneticSequenceData } from "@/models/GeneticSequenceData";
import { GeneticSequenceDatasService } from "@/services/geneticSequenceDatas/GeneticSequenceDatasService";
import { isLeft, isRight, Right } from "@/utils/either";
import { GeneticSequenceDatas } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListGeneticSequenceDataResponse } from "@/services/geneticSequenceDatas/models/ListGeneticSequenceData";
import { CreateGeneticSequenceDataResponse } from "@/services/geneticSequenceDatas/models/CreateGeneticSequenceData";
import { DeleteGeneticSequenceDataResponse } from "@/services/geneticSequenceDatas/models/DeleteGeneticSequenceData";
import { CommunicationError } from "@/services/shared/HttpClient";
import { AppModule } from "@/store/MainStore";

export interface GeneticSequenceDataState {
  GeneticSequenceDataCreate: GeneticSequenceData | undefined;
  GeneticSequenceData: GeneticSequenceData | undefined;
  GeneticSequenceDatas: Array<GeneticSequenceData>;
  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "GeneticSequenceDatas",
  store: store,
})
class GeneticSequenceDataStore
  extends VuexModule
  implements GeneticSequenceDataState
{
  // Private variables
  private geneticSequenceDataCreate: { value: GeneticSequenceData } = {
    value: this.emptyGeneticSequenceData,
  };

  private geneticSequenceData: { value: GeneticSequenceData | undefined } = {
    value: undefined,
  };

  private geneticSequenceDatas: { value: Array<GeneticSequenceData> } = {
    value: GeneticSequenceDatas,
  };
  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Create
  @Mutation
  public SET_GENETICSEQUENCEDATA_CREATE(
    GeneticSequenceData: GeneticSequenceData
  ): void {
    this.geneticSequenceDataCreate.value = GeneticSequenceData;
  }

  // Details - Edit
  @Mutation
  public SET_GENETICSEQUENCEDATA(
    GeneticSequenceData: GeneticSequenceData | undefined
  ): void {
    this.geneticSequenceData.value = GeneticSequenceData;
  }

  @Mutation
  public CLEAR_GENETICSEQUENCEDATA(): void {
    this.geneticSequenceData.value = undefined;
  }

  // List
  @Mutation
  public SET_GENETICSEQUENCEDATAS(
    GeneticSequenceDatas: Array<GeneticSequenceData>
  ): void {
    this.geneticSequenceDatas.value = GeneticSequenceDatas;
  }

  // Actions
  @Action({ rawError: true })
  public async CreateGeneticSequenceData(): Promise<void> {
    AppModule.ShowLoading();
    const service = new GeneticSequenceDatasService();
    const GeneticSequenceData = this.geneticSequenceDataCreate.value;
    if (GeneticSequenceData === undefined) {
      this.SET_ERROR({
        message:
          "GeneticSequenceDatasStore: not expecting GeneticSequenceData to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.create(GeneticSequenceData);
    if (isLeft(response)) {
      const createResponse: CreateGeneticSequenceDataResponse = response.value;
      GeneticSequenceData.Id = createResponse.Id;
      this.SET_GENETICSEQUENCEDATA(GeneticSequenceData);
      this.SET_GENETICSEQUENCEDATA_CREATE(this.emptyGeneticSequenceData);
      AppModule.HideLoading();
      return;
    }

    this.SET_ERROR(response.value as AppError);
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async ListGeneticSequenceDatas(): Promise<void> {
    const service = new GeneticSequenceDatasService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListGeneticSequenceDataResponse = response.value;
      this.SET_GENETICSEQUENCEDATAS(listResponse.GeneticSequenceDatas);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ListGeneticSequenceDatasPublic(): Promise<void> {
    const service = new GeneticSequenceDatasService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListGeneticSequenceDataResponse = response.value;
      this.SET_GENETICSEQUENCEDATAS(listResponse.GeneticSequenceDatas);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async UpdateGeneticSequenceData(): Promise<void> {
    AppModule.ShowLoading();
    const service = new GeneticSequenceDatasService();
    const GeneticSequenceData: GeneticSequenceData | undefined =
      this.GeneticSequenceData;
    if (!GeneticSequenceData) {
      this.SET_ERROR({
        message:
          "GeneticSequenceDatasStore: not expecting GeneticSequenceData to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.update(GeneticSequenceData);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
    }
    AppModule.HideLoading();
  }

  @Action({ rawError: true })
  public async DeleteGeneticSequenceData(): Promise<void> {
    AppModule.ShowLoading();
    const service = new GeneticSequenceDatasService();
    const GeneticSequenceData: GeneticSequenceData | undefined =
      this.GeneticSequenceData;
    if (!GeneticSequenceData) {
      this.SET_ERROR({
        message:
          "GeneticSequenceDatasStore: not expecting GeneticSequenceData to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }

    const response = await service.delete(GeneticSequenceData);
    if (isLeft(response)) {
      const deleteGeneticSequenceDataResponse: DeleteGeneticSequenceDataResponse =
        response.value;
      this.SET_GENETICSEQUENCEDATA(undefined);
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

  get GeneticSequenceData(): GeneticSequenceData | undefined {
    return this.geneticSequenceData.value;
  }

  get GeneticSequenceDatas(): GeneticSequenceData[] {
    return this.geneticSequenceDatas.value ?? new Array<GeneticSequenceData>();
  }

  get GeneticSequenceDataCreate(): GeneticSequenceData {
    return this.geneticSequenceDataCreate.value;
  }

  get emptyGeneticSequenceData(): GeneticSequenceData {
    return Object.create({
      Id: "",
      Code: "",
      Name: "",
      Description: "",
      IsActive: false,
    });
  }
}

export const GeneticSequenceDataModule = getModule(
  GeneticSequenceDataStore,
  store
);
