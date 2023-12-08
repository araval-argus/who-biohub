import { Gender } from "@/models/enums/Gender";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";

export interface MaterialClinicalDetail {
  Id: string;
  MaterialShippingInformationId: string;
  MaterialProductId: string;
  TransportCategoryId: string;
  MaterialNumber: string;
  CollectionDate: Date;
  Location: string;
  IsolationHostTypeId: string;
  Gender: Gender;
  Age: number;
  PatientStatus: string;
  Condition: ShipmentMaterialCondition;
  Note: string;
}
