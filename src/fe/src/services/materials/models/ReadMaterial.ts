import { Material } from "@/models/Material";

export interface ReadMaterialQuery {
  Id: string;
}

export interface ReadMaterialResponse {
  Material: Material;
}
