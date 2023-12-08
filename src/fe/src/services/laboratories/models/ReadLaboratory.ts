import { Laboratory } from "@/models/Laboratory";

export interface ReadLaboratoryQuery {
  Id: string;
}

export interface ReadLaboratoryResponse {
  Laboratory: Laboratory;
}
