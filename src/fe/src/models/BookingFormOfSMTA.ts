import TemperatureTransportCondition from "./enums/TemperatureTransportCondition.vue";
import { WorklistItemUser } from "@/models/WorklistItemUser";

export interface BookingFormOfSMTA {
  Id: string;
  WorklistItemId: string;
  MaterialProductId: string;
  MaterialProductName: string;
  MaterialProductDescription: string;
  TransportCategoryId: string;
  TransportCategoryName: string;
  TransportCategoryDescription: string;
  Date: Date;
  RequestDateOfPickup: Date;
  TemperatureTransportCondition: TemperatureTransportCondition;
  TotalNumberOfVials: number;
  TotalAmount: number;
  NumberOfInnerPackagingAndSize: string;
  BookingFormPickupUsers: Array<WorklistItemUser>;
  BookingFormCourierUsers: Array<WorklistItemUser>;
  CourierId: string;
  EstimateDateOfPickup: Date;
  DateOfPickup: Date;
  ShipmentReferenceNumber: string;
  DateOfDelivery: Date;
  TransportModeId: string;
  TransportModeName: string;
  TransportModeDescription: string;
}
