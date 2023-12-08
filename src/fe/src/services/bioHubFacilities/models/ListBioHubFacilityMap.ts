import { BioHubFacilityMap } from "@/models/BioHubFacilityMap";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListBioHubFacilityMapQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListBioHubFacilityMapResponse {
  BioHubFacilities: BioHubFacilityMap[];
}
