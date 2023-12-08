import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { Kpi } from "@/models/Kpi";

import { KpiDatasService } from "@/services/kpiDatas/KpiDatasService";
import { isLeft, isRight, Right } from "@/utils/either";
import { kpiDatas } from "./mock";
import { AppError } from "@/models/shared/Error";

import { CommunicationError } from "@/services/shared/HttpClient";
import { ReadKpiQuery } from "@/services/kpiDatas/models/ReadKpiData";
import { ReadKpiResponse } from "@/services/kpiDatas/models/ReadKpiData";

import { ReadKpiDataPublicResponse } from "@/services/kpiDatas/models/ReadKpiDataPublic";
import { ReadKpiDataPublicQuery } from "@/services/kpiDatas/models/ReadKpiDataPublic";
import { kpiDatasPublic } from "./mock";
import { KpiDataPublic } from "@/models/KpiDataPublic";
import { AppModule } from "@/store/MainStore";

export interface KpiDataState {
  Kpi: Kpi | undefined;

  KpiDataPublic: KpiDataPublic | undefined;

  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "kpiDatas",
  store: store,
})
class KpiDataStore extends VuexModule implements KpiDataState {
  // Private variables

  private kpi: { value: Kpi | undefined } = {
    value: undefined,
  };

  private kpiDataPublic: { value: KpiDataPublic | undefined } = {
    value: undefined,
  };

  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Details - Edit
  @Mutation
  public SET_KPI(kpiData: Kpi | undefined): void {
    this.kpi.value = kpiData;
  }

  @Mutation
  public CLEAR_KPI(): void {
    this.kpi.value = undefined;
  }

  @Mutation
  public CLEAR_KPI_DATA_PUBLIC(): void {
    this.kpi.value = undefined;
  }

  @Action({ rawError: true })
  public async ReadKpi(): Promise<void> {
    const service = new KpiDatasService();

    const response = await service.read({});
    if (isLeft(response)) {
      const readResponse: ReadKpiResponse = response.value;
      this.SET_KPI(readResponse.Kpi);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ReadKpiDataPublic(): Promise<void> {
    const service = new KpiDatasService();

    const response = await service.readPublic({});
    if (isLeft(response)) {
      const readResponse: ReadKpiDataPublicResponse = response.value;
      this.SET_KPI_DATA_PUBLIC(readResponse.Kpi);
      return;
    }
  }

  @Mutation
  public SET_KPI_DATA_PUBLIC(kpiData: KpiDataPublic | undefined): void {
    this.kpiDataPublic.value = kpiData;
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get Kpi(): Kpi | undefined {
    return this.kpi.value;
  }

  get KpiDataPublic(): KpiDataPublic | undefined {
    return this.kpiDataPublic.value;
  }
}

export const KpiDataModule = getModule(KpiDataStore, store);
