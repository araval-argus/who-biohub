import { Material } from "@/models/Material";

export interface ReadMaterialForBioHubFacilityCompletionQuery {
  Id: string;
}

export interface ReadMaterialForBioHubFacilityCompletionResponse {
  Material: Material;
}
