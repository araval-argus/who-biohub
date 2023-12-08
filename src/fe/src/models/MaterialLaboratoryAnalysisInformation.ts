import { Gender } from "@/models/enums/Gender";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";
import { YesNoOption } from "./enums/YesNoOption";

export interface MaterialLaboratoryAnalysisInformation {
  Id: string;
  MaterialShippingInformationId: string;
  MaterialNumber: string;
  FreezingDate: Date;
  Temperature: number;
  UnitOfMeasureId: string;
  VirusConcentration: string;
  CulturingCellLine: string;
  CulturingPassagesNumber: number;
  CollectedSpecimenTypes: string[];
  TypeOfTransportMedium: string;
  BrandOfTransportMedium: string;
  GSDUploadedToDatabase: YesNoOption;
  DatabaseUsedForGSDUploadingId: string;
  AccessionNumberInGSDDatabase: string;
}
