import { Laboratory } from "@/models/Laboratory";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListLaboratoryQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListLaboratoryResponse {
  Laboratories: Laboratory[];
}
