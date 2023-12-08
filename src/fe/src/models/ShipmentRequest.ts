import { ShipmentDirection } from "./enums/ShipmentDirection";

export interface ShipmentRequest {
  Id: string;
  WorklistItemTitle: string;
  OperationDate: Date;
  SendBy: string;
  Institution: string;
  ShipmentDirection: ShipmentDirection;
  LaboratoryCountryIso: string;
  CurrentStatusName: string;
}
