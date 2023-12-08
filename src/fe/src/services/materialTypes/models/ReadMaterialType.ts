import { MaterialType } from "@/models/MaterialType";

export interface ReadMaterialTypeQuery {
  Id: string;
}

export interface ReadMaterialTypeResponse {
  MaterialType: MaterialType;
}
