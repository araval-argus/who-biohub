import { BioHubFacility } from "@/models/BioHubFacility";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListBioHubFacilityQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListBioHubFacilityResponse {
  BioHubFacilities: BioHubFacility[];
}
