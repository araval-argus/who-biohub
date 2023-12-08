import { MaterialType } from "@/models/MaterialType";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListMaterialTypeQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListMaterialTypeResponse {
  MaterialTypes: MaterialType[];
}
