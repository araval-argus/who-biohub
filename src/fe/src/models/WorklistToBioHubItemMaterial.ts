import { Gender } from "./enums/Gender";
import { MaterialStatus } from "./enums/MaterialStatus";

export interface WorklistToBioHubItemMaterial {
  Id: string;
  WorklistToBioHubItemId: string;
  MaterialId: string;
  MaterialNumber: string;
  MaterialProductId: string;
  TransportCategoryId: string;
  MaterialName: string;
  CollectionDate: Date;
  Location: string;
  IsolationHostTypeId: string;
  Gender: Gender;
  Age: number;
  Status: MaterialStatus;
  StatusDescription: string;
}
