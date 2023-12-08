import { Gender } from "@/models/enums/Gender";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";
import { YesNoOption } from "./enums/YesNoOption";

export interface MaterialLaboratoryAnalysisInformationGridItem {
  Id: string;
  MaterialShippingInformationId: string;
  MaterialNumber: string;
  FreezingDate: string;
  Temperature: string;
  VirusConcentration: string;
  CulturingCellLine: string;
  CulturingPassagesNumber: number;
  CollectedSpecimenTypes: string;
  TypeOfTransportMedium: string;
  BrandOfTransportMedium: string;
  GSDUploadedToDatabaseString: string;
  GSDUploadedToDatabase: YesNoOption;
  DatabaseUsedForGSDUploadingId: string;
  AccessionNumberInGSDDatabase: string;
}
