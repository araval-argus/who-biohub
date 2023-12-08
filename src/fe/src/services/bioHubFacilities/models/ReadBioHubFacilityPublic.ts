import { BioHubFacilityPublic } from "@/models/BioHubFacilityPublic";

export interface ReadBioHubFacilityPublicQuery {
  Id: string;
}

export interface ReadBioHubFacilityPublicResponse {
  BioHubFacility: BioHubFacilityPublic;
}
