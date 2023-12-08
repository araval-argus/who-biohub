import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { SMTARequest } from "@/models/SMTARequest";
import { SMTARequestsService } from "@/services/SMTARequests/SMTARequestsService";
import { isLeft, isRight, Right } from "@/utils/either";
import { SMTARequests } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListSMTARequestResponse } from "@/services/SMTARequests/models/ListSMTARequest";

import { CommunicationError } from "@/services/shared/HttpClient";

import { AppModule } from "@/store/MainStore";

export interface SMTARequestState {
  SMTARequest: SMTARequest | undefined;
  SMTARequests: Array<SMTARequest>;

  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "SMTARequests",
  store: store,
})
class SMTARequestStore extends VuexModule implements SMTARequestState {
  private smtaRequest: { value: SMTARequest | undefined } = {
    value: undefined,
  };

  private smtaRequests: { value: Array<SMTARequest> } = {
    value: SMTARequests,
  };

  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Details - Edit
  @Mutation
  public SET_SMTA_REQUEST(SMTARequest: SMTARequest | undefined): void {
    this.smtaRequest.value = SMTARequest;
  }

  @Mutation
  public CLEAR_SMTA_REQUEST(): void {
    this.smtaRequest.value = undefined;
  }

  // List
  @Mutation
  public SET_SMTA_REQUESTS(SMTARequests: Array<SMTARequest>): void {
    this.smtaRequests.value = SMTARequests;
  }

  @Action({ rawError: true })
  public async ListSMTARequests(): Promise<void> {
    const service = new SMTARequestsService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListSMTARequestResponse = response.value;
      this.SET_SMTA_REQUESTS(listResponse.SMTARequests);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get SMTARequest(): SMTARequest | undefined {
    return this.smtaRequest.value;
  }

  get SMTARequests(): SMTARequest[] {
    return this.smtaRequests.value ?? new Array<SMTARequest>();
  }
}

export const SMTARequestModule = getModule(SMTARequestStore, store);
