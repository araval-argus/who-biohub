import { WorklistToBioHubItemMaterial } from "@/models/WorklistToBioHubItemMaterial";

export interface ListMaterialsForWorklistToBioHubItemQuery {
  WorklistToBioHubItemId: string;
}

export interface ListMaterialsForWorklistToBioHubItemResponse {
  WorklistToBioHubItemMaterials: Array<WorklistToBioHubItemMaterial>;
}
