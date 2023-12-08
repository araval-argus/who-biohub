import { Laboratory } from "@/models/Laboratory";
export interface CreateLaboratoryFromUserRequestCommand {
  InstituteName: string;
  CountryId: string;
}

export interface CreateLaboratoryFromUserRequestResponse {
  Laboratory: Laboratory;
}
