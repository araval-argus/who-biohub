import {
  VuexModule,
  Module,
  Mutation,
  getModule,
  Action,
} from "vuex-module-decorators";
import store from "@/store";
import { Shipment } from "@/models/Shipment";
import { ShipmentsService } from "@/services/shipments/ShipmentsService";
import { isLeft, isRight, Right } from "@/utils/either";
import { shipments } from "./mock";
import { AppError } from "@/models/shared/Error";
import { ListShipmentResponse } from "@/services/shipments/models/ListShipment";
import { CreateShipmentResponse } from "@/services/shipments/models/CreateShipment";
import { DeleteShipmentResponse } from "@/services/shipments/models/DeleteShipment";
import { CommunicationError } from "@/services/shared/HttpClient";
import { ReadShipmentQuery } from "@/services/shipments/models/ReadShipment";
import { ReadShipmentResponse } from "@/services/shipments/models/ReadShipment";
import { ListShipmentPublicResponse } from "@/services/shipments/models/ListShipmentPublic";
import { ReadShipmentPublicResponse } from "@/services/shipments/models/ReadShipmentPublic";
import { ReadShipmentPublicQuery } from "@/services/shipments/models/ReadShipmentPublic";
import { shipmentsPublic } from "./mock";
import { ShipmentPublic } from "@/models/ShipmentPublic";
import { AppModule } from "@/store/MainStore";
import { ShipmentMaterial } from "@/models/ShipmentMaterial";
import { ShipmentPublicMaterial } from "@/models/ShipmentPublicMaterial";
import { Document } from "@/models/Document";
import { EForm } from "@/models/EForm";

export interface ShipmentState {
  //ShipmentCreate: Shipment | undefined;
  Shipment: Shipment | undefined;
  Shipments: Array<Shipment>;
  ShipmentPublic: ShipmentPublic | undefined;
  ShipmentsPublic: Array<ShipmentPublic>;

  Error: AppError | undefined;
}

@Module({
  namespaced: true,
  dynamic: true,
  name: "shipments",
  store: store,
})
class ShipmentStore extends VuexModule implements ShipmentState {
  // Private variables
  // private shipmentCreate: { value: Shipment } = {
  //   value: {},
  // };

  private shipment: { value: Shipment | undefined } = {
    value: undefined,
  };

  private shipments: { value: Array<Shipment> } = {
    value: shipments,
  };

  private shipmentPublic: { value: ShipmentPublic | undefined } = {
    value: undefined,
  };

  private shipmentsPublic: { value: Array<ShipmentPublic> } = {
    value: shipmentsPublic,
  };

  private currentFolderDocuments: { value: Array<Document> } = {
    value: [],
  };

  private currentFolderEForms: { value: Array<EForm> } = {
    value: [],
  };

  private error: { value: AppError | undefined } = { value: undefined };

  // Mutations
  @Mutation
  public SET_ERROR(error: AppError | undefined): void {
    this.error.value = error;
  }

  // Create
  // @Mutation
  // public SET_SHIPMENT_CREATE(shipment: Shipment): void {
  //   this.shipmentCreate.value = shipment;
  // }

  // Details - Edit
  @Mutation
  public SET_SHIPMENT(shipment: Shipment | undefined): void {
    this.shipment.value = shipment;
  }

  @Mutation
  public CLEAR_SHIPMENT(): void {
    this.shipment.value = undefined;
  }

  @Mutation
  public CLEAR_SHIPMENT_PUBLIC(): void {
    this.shipment.value = undefined;
  }

  // List
  @Mutation
  public SET_SHIPMENTS(shipments: Array<Shipment>): void {
    this.shipments.value = shipments;
  }

  // Actions
  // @Action({ rawError: true })
  // public async CreateShipment(): Promise<void> {
  //   AppModule.ShowLoading();
  //   const service = new ShipmentsService();
  //   const shipment = this.shipmentCreate.value;
  //   if (shipment === undefined) {
  //     this.SET_ERROR({
  //       message:
  //         "ShipmentsStore: not expecting shipment to be undefined; this should be a bug",
  //     });
  //     AppModule.HideLoading();
  //     return;
  //   }

  //   const response = await service.create(shipment);
  //   if (isLeft(response)) {
  //     const createResponse: CreateShipmentResponse = response.value;
  //     shipment.Id = createResponse.Id;
  //     this.SET_SHIPMENT(shipment);
  //     this.SET_SHIPMENT_CREATE(this.emptyShipment);
  //     AppModule.HideLoading();
  //     return;
  //   }

  //   this.SET_ERROR(response.value as AppError);
  //   AppModule.HideLoading();
  // }

  @Action({ rawError: true })
  public async ListShipments(): Promise<void> {
    const service = new ShipmentsService();
    const response = await service.list({});
    if (isLeft(response)) {
      const listResponse: ListShipmentResponse = response.value;
      this.SET_SHIPMENTS(listResponse.Shipments);
      return;
    }

    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
  }

  @Action({ rawError: true })
  public async ReadShipment(id: string): Promise<void> {
    const service = new ShipmentsService();
    const query: ReadShipmentQuery = { Id: id };
    const response = await service.read(query);
    if (isLeft(response)) {
      const readResponse: ReadShipmentResponse = response.value;
      this.SET_SHIPMENT(readResponse.Shipment);
      this.SET_CURRENT_FOLDER_DOCUMENTS(null);
      this.SET_CURRENT_FOLDER_EFORMS(null);
      return;
    }
    this.SET_ERROR(response.value as AppError);
    if (
      response.value.message !== undefined &&
      response.value.message["ErrorType"] != 3
    ) {
      AppModule.SetErrorNotifications(this.ErrorMessage);
    }
  }

  @Action({ rawError: true })
  public async UpdateShipment(): Promise<void> {
    AppModule.ShowLoading();
    const service = new ShipmentsService();
    const shipment: Shipment | undefined = this.Shipment;
    if (!shipment) {
      this.SET_ERROR({
        message:
          "ShipmentsStore: not expecting shipment to be undefined; this should be a bug",
      });
      AppModule.HideLoading();
      return;
    }
    AppModule.ClearErrorNotifications();
    AppModule.ClearSuccessNotifications();
    const response = await service.update(shipment);
    if (isRight(response)) {
      this.SET_ERROR(response.value as AppError);
      if (
        response.value.message !== undefined &&
        response.value.message["ErrorType"] != 3
      ) {
        AppModule.SetErrorNotifications(this.ErrorMessage);
      }
      AppModule.HideLoading();
      throw response.value;
    } else {
      this.SET_ERROR(undefined);
      this.SET_SHIPMENT(shipment);
      AppModule.SetSuccessNotifications("Shipment successfully updated");
      AppModule.HideLoading();
      return;
    }
  }

  @Action({ rawError: true })
  public async ListShipmentsPublic(): Promise<void> {
    const service = new ShipmentsService();
    const response = await service.listPublic({});
    if (isLeft(response)) {
      const listResponse: ListShipmentPublicResponse = response.value;
      this.SET_SHIPMENTS_PUBLIC(listResponse.Shipments);
      return;
    }

    this.SET_ERROR(response.value as AppError);
  }

  @Action({ rawError: true })
  public async ReadShipmentPublic(id: string): Promise<void> {
    const service = new ShipmentsService();
    const query: ReadShipmentPublicQuery = { Id: id };
    const response = await service.readPublic(query);
    if (isLeft(response)) {
      const readResponse: ReadShipmentPublicResponse = response.value;
      this.SET_SHIPMENT_PUBLIC(readResponse.Shipment);
      return;
    }
  }

  @Mutation
  public SET_SHIPMENTS_PUBLIC(shipments: Array<ShipmentPublic>): void {
    this.shipmentsPublic.value = shipments;
  }

  @Mutation
  public SET_SHIPMENT_PUBLIC(shipment: ShipmentPublic | undefined): void {
    this.shipmentPublic.value = shipment;
  }

  @Mutation
  public SET_CURRENT_FOLDER_DOCUMENTS(folderId: string | null): void {
    this.currentFolderDocuments.value =
      this.shipment.value?.Documents.filter((r) => {
        return r.ParentId == folderId;
      }) ?? [];
  }

  @Mutation
  public SET_CURRENT_FOLDER_EFORMS(folderId: string | null): void {
    this.currentFolderEForms.value =
      this.shipment.value?.EForms.filter((r) => {
        return r.ParentId == folderId;
      }) ?? [];
  }

  // Getters
  get Error(): AppError | undefined {
    return this.error.value;
  }

  get Shipment(): Shipment | undefined {
    return this.shipment.value;
  }

  get Shipments(): Shipment[] {
    return this.shipments.value ?? new Array<Shipment>();
  }

  get WorklistToBioHubItemId(): string {
    return this.shipment.value?.WorklistToBioHubItemId ?? "";
  }

  get WorklistFromBioHubItemId(): string {
    return this.shipment.value?.WorklistFromBioHubItemId ?? "";
  }

  // get ShipmentCreate(): Shipment {
  //   return this.shipmentCreate.value;
  // }

  get ShipmentsPublic(): ShipmentPublic[] {
    return this.shipmentsPublic.value ?? new Array<ShipmentPublic>();
  }

  get ShipmentPublic(): ShipmentPublic | undefined {
    return this.shipmentPublic.value;
  }

  get ShipmentMaterials(): ShipmentMaterial[] {
    return (
      this.shipment.value?.ShipmentMaterials ?? new Array<ShipmentMaterial>()
    );
  }

  get ShipmentPublicMaterials(): ShipmentPublicMaterial[] {
    return (
      this.shipmentPublic.value?.ShipmentMaterials ??
      new Array<ShipmentPublicMaterial>()
    );
  }

  get Documents(): Document[] {
    return this.shipment.value?.Documents ?? new Array<Document>();
  }

  get EForms(): EForm[] {
    return this.shipment.value?.EForms ?? new Array<EForm>();
  }

  get CurrentFolderDocuments(): Document[] {
    return this.currentFolderDocuments.value ?? new Array<Document>();
  }

  get CurrentFolderEForms(): EForm[] {
    return this.currentFolderEForms.value ?? new Array<EForm>();
  }

  get ErrorMessage(): any {
    return this.error.value?.message;
  }
}

export const ShipmentModule = getModule(ShipmentStore, store);
