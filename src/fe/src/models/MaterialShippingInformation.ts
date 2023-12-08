import { MaterialClinicalDetail } from "@/models/MaterialClinicalDetail";
import { MaterialLaboratoryAnalysisInformation } from "./MaterialLaboratoryAnalysisInformation";

export interface MaterialShippingInformation {
  Id: string;
  MaterialNumber: string;
  MaterialProductId: string;
  TransportCategoryId: string;
  Condition: string;
  AdditionalInformation: string;
  Quantity: number;
  Amount: number;
  MaterialClinicalDetails: Array<MaterialClinicalDetail>;
  MaterialLaboratoryAnalysisInformation: Array<MaterialLaboratoryAnalysisInformation>;
}
