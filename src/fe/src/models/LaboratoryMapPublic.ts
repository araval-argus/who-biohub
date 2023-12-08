import { InstituteType } from "./enums/InstituteType";
import { Coordinates } from "./shared/Coordinates";

export interface LaboratoryMapPublic {
  Id: string;
  Name: string;
  Abbreviation: string;
  Address: string;
  Latitude: number;
  Longitude: number;
  CountryId: string;
  ToBioHubConnectedInstitutesLatLng: Array<Coordinates>;
  FromBioHubConnectedInstitutesLatLng: Array<Coordinates>;
  InstituteType: InstituteType;
}
