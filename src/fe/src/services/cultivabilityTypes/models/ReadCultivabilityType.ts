import { CultivabilityType } from "@/models/CultivabilityType";

export interface ReadCultivabilityTypeQuery {
  Id: string;
}

export interface ReadCultivabilityTypeResponse {
  CultivabilityType: CultivabilityType;
}
