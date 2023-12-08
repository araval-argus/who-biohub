import { CourierBookingForm } from "@/models/CourierBookingForm";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListCourierBookingFormQuery {
  CourierId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListCourierBookingFormResponse {
  CourierBookingForms: CourierBookingForm[];
}
