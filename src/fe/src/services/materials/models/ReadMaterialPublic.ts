import { MaterialPublic } from "@/models/MaterialPublic";

export interface ReadMaterialPublicQuery {
  Id: string;
}

export interface ReadMaterialPublicResponse {
  Material: MaterialPublic;
}
