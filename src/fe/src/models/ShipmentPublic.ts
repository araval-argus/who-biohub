import { ShipmentDirection } from "./enums/ShipmentDirection";
import { ShipmentPublicMaterial } from "./ShipmentPublicMaterial";

export interface ShipmentPublic {
  Id: string;
  ReferenceNumber: string;
  From: string;
  To: string;
  StatusOfRequest: string;
  CompletedOn: Date;
  LaboratoryId: string;
  BioHubFacilityId: string;
  ShipmentMaterials: Array<ShipmentPublicMaterial>;
  ShipmentDirection: ShipmentDirection;
}
