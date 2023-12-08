import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { ShipmentRequest } from "@/models/ShipmentRequest";
import { ShipmentRequestsService } from "@/services/shipmentRequests/ShipmentRequestsService";
import { isLeft, isRight, Right } from "@/utils/either";
import { shipmentRequests } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListShipmentRequestResponse } from "@/services/shipmentRequests/models/ListShipmentRequest";

import { CommunicationError } from "@/services/shared/HttpClient";

import { AppModule } from "@/store/MainStore";

export interface ShipmentRequestState {
  ShipmentRequest: ShipmentRequest | undefined;
  ShipmentRequests: Array<ShipmentRequest>;

  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "shipmentRequests",
  store: store,
})
class ShipmentRequestStore extends VuexModule implements ShipmentRequestState {
  private shipmentRequest: { value: ShipmentRequest | undefined } = {
    value: undefined,
  };

  private shipmentRequests: { value: Array<ShipmentRequest> } = {
    value: shipmentRequests,
  };

  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError): void {
    this.error.value = error;
  }

  // Details - Edit
  @Mutation
  public SET_SHIPMENT_REQUEST(
    shipmentRequest: ShipmentRequest | undefined
  ): void {
    this.shipmentRequest.value = shipmentRequest;
  }

  @Mutation
  public CLEAR_SHIPMENT_REQUEST(): void {
    this.shipmentRequest.value = undefined;
  }

  // List
  @Mutation
  public SET_SHIPMENT_REQUESTS(shipmentRequests: Array<ShipmentRequest>): void {
    this.shipmentRequests.value = shipmentRequests;
  }

  @Action({ rawError: true })
  public async ListShipmentRequests(): Promise<void> {
    const service = new ShipmentRequestsService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListShipmentRequestResponse = response.value;
      this.SET_SHIPMENT_REQUESTS(listResponse.ShipmentRequests);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get ShipmentRequest(): ShipmentRequest | undefined {
    return this.shipmentRequest.value;
  }

  get ShipmentRequests(): ShipmentRequest[] {
    return this.shipmentRequests.value ?? new Array<ShipmentRequest>();
  }
}

export const ShipmentRequestModule = getModule(ShipmentRequestStore, store);
