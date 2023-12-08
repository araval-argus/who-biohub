import { Shipment } from "@/models/Shipment";

export interface ReadShipmentQuery {
  Id: string;
}

export interface ReadShipmentResponse {
  Shipment: Shipment;
}
