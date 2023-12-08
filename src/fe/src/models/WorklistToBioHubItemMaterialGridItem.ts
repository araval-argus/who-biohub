import { Gender } from "@/models/enums/Gender";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";
import { MaterialStatus } from "./enums/MaterialStatus";

export interface WorklistToBioHubItemMaterialGridItem {
  Id: string;
  WorklistToBioHubItemId: string;
  MaterialId: string;
  MaterialNumber: string;
  MaterialProductId: string;
  MaterialProduct: string;
  TransportCategoryId: string;
  TransportCategory: string;
  CollectionDate: string;
  Location: string;
  IsolationHostTypeId: string;
  IsolationHostType: string;
  Gender: string;
  Age: number;
  Status: MaterialStatus;
}
