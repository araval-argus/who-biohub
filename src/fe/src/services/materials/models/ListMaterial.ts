import { Material } from "@/models/Material";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListMaterialQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListMaterialResponse {
  Materials: Material[];
}
