import { Country } from "@/models/Country";

export interface ReadCountryQuery {
  Id: string;
}

export interface ReadCountryResponse {
  Country: Country;
}
