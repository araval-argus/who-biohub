import { Gender } from "@/models/enums/Gender";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";
import { MaterialStatus } from "./enums/MaterialStatus";

export interface WorklistFromBioHubItemMaterial {
  Id: string;
  WorklistFromBioHubItemId: string;
  MaterialId: string;
  Quantity: number;
  AvailableQuantity: number;
  Amount: number;
  MaterialNumber: string;
  MaterialProductId: string;
  TransportCategoryId: string;
  MaterialName: string;
  CollectionDate: Date;
  Location: string;
  IsolationHostTypeId: string;
  Gender: Gender;
  Age: number;
  Condition: ShipmentMaterialCondition;
  Note: string;
  Status: MaterialStatus;
}
