import { ShipmentPublic } from "@/models/ShipmentPublic";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListShipmentPublicQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListShipmentPublicResponse {
  Shipments: ShipmentPublic[];
}
