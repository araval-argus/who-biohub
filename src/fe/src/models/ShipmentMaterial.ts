import { Gender } from "./enums/Gender";
import { MaterialStatus } from "./enums/MaterialStatus";
import { ShipmentMaterialCondition } from "./enums/ShipmentMaterialCondition";

export interface ShipmentMaterial {
  Id: string;
  MaterialId: string;
  MaterialNumber: string;
  MaterialProductId: string;
  MaterialName: string;
  CollectionDate: Date;
  Location: string;
  IsolationHostTypeId: string;
  Gender: Gender;
  Age: number;
  Status: MaterialStatus;
  ProviderId: string;
  ShipmentMaterialCondition: ShipmentMaterialCondition;
}
