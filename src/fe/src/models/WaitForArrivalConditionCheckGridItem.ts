import { Gender } from "@/models/enums/Gender";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";

export interface WaitForArrivalConditionCheckGridItem {
  Id: string;
  MaterialShippingInformationId: string;
  MaterialProductId: string;
  MaterialProduct: string;
  TransportCategory: string;
  TransportCategoryId: string;
  MaterialNumber: string;
  CollectionDate: string;
  Location: string;
  IsolationHostType: string;
  IsolationHostTypeId: string;
  Gender: string;
  Age: number;
  PatientStatus: string;
  ConditionString: string;
  Condition: ShipmentMaterialCondition;
  Note: string;
  Quantity: number;
  Amount: number;
  QuantityInfo: string;
  AmountInfo: string;
}
