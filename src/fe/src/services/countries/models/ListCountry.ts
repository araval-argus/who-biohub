import { Country } from "@/models/Country";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListCountryQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListCountryResponse {
  Countries: Country[];
}
