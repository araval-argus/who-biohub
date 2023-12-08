import { ShipmentDirection } from "./enums/ShipmentDirection";

export interface ShipmentRequestGridItem {
  Id: string;
  WorklistItemTitle: string;
  OperationDate: Date;
  SendDate: string;
  SendBy: string;
  Institution: string;
  ShipmentDirection: ShipmentDirection;
  ShipmentDirectionString: string;
  LaboratoryCountryIso: string;
  CurrentStatusName: string;
}
