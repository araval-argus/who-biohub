import { DatabaseUploadedBy } from "@/models/enums/DatabaseUploadedBy";
import { Gender } from "@/models/enums/Gender";
import { GSDUploadingStatus } from "@/models/enums/GSDUploadingStatus";
import { MaterialValidationSelection } from "@/models/enums/MaterialValidationSelection";
import { Readiness } from "@/models/enums/Readiness";
import { ResultType } from "@/models/enums/ResultType";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";
import { YesNoOption } from "@/models/enums/YesNoOption";
import { MaterialGSDInfo } from "@/models/MaterialGSDInfo";

export interface UpdateMaterialForBioHubFacilityCompletionCommand {
  Id: string;
  ReferenceNumber: string;
  Name: string;
  Description: string;
  Temperature: number;
  SampleId: string;
  Variant: string;
  VariantAssessment: string;
  StrainDesignation: string;
  Genotype: string;
  Serotype: string;
  DatabaseAccessionId: string;
  OriginalGeneticSequence: string;
  GSDCulturedMaterialCellLine1: string;
  GSDCulturedMaterialCellLine2: string;
  FacilityGSD: string;
  GMO: boolean;
  //ProductionCellLine: string;
  Infectivity: string;
  ViralTiter: string;
  IsNew: boolean;
  TypeId: string;
  SuspectedEpidemiologicalOriginId: string;
  ProductTypeId: string;
  TransportCategoryId: string;
  UnitOfMeasureId: string;
  UsagePermissionId: string;
  GeneticSequenceDataId: string;
  InternationalTaxonomyClassificationId: string;
  IsolationHostTypeId: string;
  CultivabilityTypeId: string;
  IsolationTechniqueTypeId: string;
  ProviderLaboratoryId: string | null;
  ProviderBioHubFacilityId: string | null;
  CollectionDate: Date | null;
  Location: string;
  PatientStatus: string;
  Age: number | null;
  Gender: Gender | null;
  Approve: boolean;

  DateOfBMEPPReceipt: Date | null;
  OriginalProductTypeId: string;
  GSDCulturedMaterialCellLine3: string;
  CulturingCellLine1: string;
  CulturingCellLine2: string;
  CulturingCellLine3: string;

  Pathogen: string;
  FreezingDate: Date | null;
  VirusConcentration: string;
  ShipmentAmount: number;
  ShipmentTemperature: number;
  ShipmentUnitOfMeasureId: string;
  CulturingCellLine: string;
  CulturingPassagesNumber: number;
  TypeOfTransportMedium: string;
  BrandOfTransportMedium: string;
  MaterialCollectedSpecimenTypes: string[];
  DatabaseUploadedBy: DatabaseUploadedBy;

  CulturingResult: ResultType;
  CulturingResultDate: Date | null;
  QualityControlResult: ResultType;
  QualityControlResultDate: Date | null;
  GSDAnalysisResult: ResultType;
  GSDAnalysisResultDate: Date | null;
  GSDUploadingStatus: GSDUploadingStatus;
  GSDUploadingDate: Date | null;

  LastAliquotsAdditionDate: Date | null;

  MaterialGSDInfo: MaterialGSDInfo[];

  BHFShareReadiness: Readiness | null;
  PublicShare: YesNoOption;

  ShipmentMaterialCondition: ShipmentMaterialCondition;
  ShipmentMaterialConditionNote: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateMaterialForBioHubFacilityCompletionResponse {}
