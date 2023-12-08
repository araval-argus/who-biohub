import { Coordinates } from "./shared/Coordinates";

export interface BioHubFacilityMapPublic {
  Id: string;
  Name: string;
  Abbreviation: string;
  Address: string;
  Latitude: number;
  Longitude: number;
  CountryId: string;
  ToBioHubConnectedInstitutesLatLng: Array<Coordinates>;
  FromBioHubConnectedInstitutesLatLng: Array<Coordinates>;
}
