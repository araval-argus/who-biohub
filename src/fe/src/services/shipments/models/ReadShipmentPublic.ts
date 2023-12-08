import { ShipmentPublic } from "@/models/ShipmentPublic";

export interface ReadShipmentPublicQuery {
  Id: string;
}

export interface ReadShipmentPublicResponse {
  Shipment: ShipmentPublic;
}
