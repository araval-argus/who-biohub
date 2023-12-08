import { BookingFormOfSMTA2Data } from "@/models/BookingFormOfSMTA2Data";

export interface ReadBookingFormOfSMTA2Query {
  Id: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ReadBookingFormOfSMTA2Response {
  BookingFormOfSMTA2Data: BookingFormOfSMTA2Data;
}
