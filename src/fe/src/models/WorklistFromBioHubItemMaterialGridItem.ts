import { Gender } from "@/models/enums/Gender";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";
import { MaterialStatus } from "./enums/MaterialStatus";

export interface WorklistFromBioHubItemMaterialGridItem {
  Id: string;
  WorklistFromBioHubItemId: string;
  MaterialId: string;
  Quantity: number;
  AvailableQuantity: number;
  QuantityInfo: string;
  Amount: number;
  AmountInfo: string;
  MaterialNumber: string;
  MaterialProductId: string;
  TransportCategoryId: string;
  MaterialName: string;
  CollectionDate: string;
  Location: string;
  IsolationHostTypeId: string;
  Gender: string;
  Age: number;
  Condition: ShipmentMaterialCondition;
  Note: string;
  Status: MaterialStatus;
}
