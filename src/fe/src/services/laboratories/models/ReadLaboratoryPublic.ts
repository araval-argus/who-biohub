import { LaboratoryPublic } from "@/models/LaboratoryPublic";

export interface ReadLaboratoryPublicQuery {
  Id: string;
}

export interface ReadLaboratoryPublicResponse {
  Laboratory: LaboratoryPublic;
}
