import { Courier } from "@/models/Courier";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListCourierQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListCourierResponse {
  Couriers: Courier[];
}
