import { ShipmentRequest } from "@/models/ShipmentRequest";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListShipmentRequestQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListShipmentRequestResponse {
  ShipmentRequests: ShipmentRequest[];
}
