import { EForm } from "./EForm";
import { Document } from "./Document";
import { ShipmentDirection } from "./enums/ShipmentDirection";
import { ShipmentMaterial } from "./ShipmentMaterial";
import { BookingFormOfSMTA } from "./BookingFormOfSMTA";

export interface Shipment {
  Id: string;
  ReferenceNumber: string;
  From: string;
  To: string;
  StatusOfRequest: string;
  CompletedOn: Date;
  LaboratoryId: string;
  BioHubFacilityId: string;
  ShipmentMaterials: Array<ShipmentMaterial>;
  ShipmentDirection: ShipmentDirection;
  WorklistFromBioHubItemId: string;
  WorklistToBioHubItemId: string;
  BookingForms: Array<BookingFormOfSMTA>;
  Documents: Array<Document>;
  EForms: Array<EForm>;
  CanEditReferenceNumber: boolean;
}
