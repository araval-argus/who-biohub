import { BioHubFacility } from "@/models/BioHubFacility";

export interface ReadBioHubFacilityQuery {
  Id: string;
}

export interface ReadBioHubFacilityResponse {
  BioHubFacility: BioHubFacility;
}
