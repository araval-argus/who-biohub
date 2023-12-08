import { WorklistFromBioHubItemMaterial } from "@/models/WorklistFromBioHubItemMaterial";

export interface ListMaterialsForWorklistFromBioHubItemQuery {
  WorklistFromBioHubItemId: string;
  BioHubFacilityId: string;
}

export interface ListMaterialsForWorklistFromBioHubItemResponse {
  WorklistFromBioHubItemMaterials: Array<WorklistFromBioHubItemMaterial>;
}
