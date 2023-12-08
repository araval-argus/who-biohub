import { SpecimenType } from "@/models/SpecimenType";

export interface ReadSpecimenTypeQuery {
  Id: string;
}

export interface ReadSpecimenTypeResponse {
  SpecimenType: SpecimenType;
}
