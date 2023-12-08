import { Shipment } from "@/models/Shipment";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListShipmentQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListShipmentResponse {
  Shipments: Shipment[];
}
