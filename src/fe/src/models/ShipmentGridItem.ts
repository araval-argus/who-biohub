import { ShipmentDirection } from "./enums/ShipmentDirection";

export interface ShipmentGridItem {
  Id: string;
  ReferenceNumber: string;
  From: string;
  To: string;
  StatusOfRequest: string;
  CompletedOn: string;
  ShipmentDirection: ShipmentDirection;
  ShipmentDirectionString: string;
}
