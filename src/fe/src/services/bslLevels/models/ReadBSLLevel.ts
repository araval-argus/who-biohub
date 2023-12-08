import { BSLLevel } from "@/models/BSLLevel";

export interface ReadBSLLevelQuery {
  Id: string;
}

export interface ReadBSLLevelResponse {
  BSLLevel: BSLLevel;
}
