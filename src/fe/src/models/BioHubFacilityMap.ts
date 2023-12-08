import { Coordinates } from "./shared/Coordinates";

export interface BioHubFacilityMap {
  Id: string;
  Name: string;
  Abbreviation: string;
  IsActive: boolean;
  Description: string;
  Address: string;
  Latitude: number;
  Longitude: number;
  BSLLevelId: string;
  CountryId: string;
  IsPublicFacing: boolean;
  ToBioHubConnectedInstitutesLatLng: Array<Coordinates>;
  FromBioHubConnectedInstitutesLatLng: Array<Coordinates>;
}
