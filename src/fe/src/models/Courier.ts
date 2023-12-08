import { CourierBookingFormGridItem } from "./CourierBookingFormGridItem";

export interface Courier {
  Id: string;
  Name: string;
  WHOAccountNumber: string;
  BusinessPhone: string;
  IsActive: boolean;
  Description: string;
  Address: string;
  Email: string;
  Latitude: number;
  Longitude: number;
  CountryId: string;
}
