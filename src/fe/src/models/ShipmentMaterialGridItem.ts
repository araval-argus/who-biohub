import { Gender } from "./enums/Gender";
import { MaterialStatus } from "./enums/MaterialStatus";
import { ShipmentMaterialCondition } from "./enums/ShipmentMaterialCondition";

export interface ShipmentMaterialGridItem {
  Id: string;
  MaterialId: string;
  MaterialNumber: string;
  MaterialProductId: string;
  MaterialProduct: string;
  MaterialName: string;
  CollectionDate: string;
  Location: string;
  IsolationHostTypeId: string;
  Gender: string;
  Age: number;
  Status: MaterialStatus;
  StatusDescription: string;
  ProviderId: string;
  ShipmentMaterialCondition: ShipmentMaterialCondition;
}
