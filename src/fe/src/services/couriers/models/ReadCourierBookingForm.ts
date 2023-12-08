import { CourierBookingForm } from "@/models/CourierBookingForm";

export interface ReadCourierBookingFormQuery {
  Id: string;
}

export interface ReadCourierBookingFormResponse {
  CourierBookingForm: CourierBookingForm;
}
