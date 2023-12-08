import { BookingFormOfSMTA1Data } from "@/models/BookingFormOfSMTA1Data";

export interface ReadBookingFormOfSMTA1Query {
  Id: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ReadBookingFormOfSMTA1Response {
  BookingFormOfSMTA1Data: BookingFormOfSMTA1Data;
}
