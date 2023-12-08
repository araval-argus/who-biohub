import { MaterialLaboratoryCompletion } from "@/models/MaterialLaboratoryCompletion";

export interface ReadMaterialForLaboratoryCompletionQuery {
  Id: string;
}

export interface ReadMaterialForLaboratoryCompletionResponse {
  Material: MaterialLaboratoryCompletion;
}
