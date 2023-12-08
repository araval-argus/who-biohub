import { DatabaseUploadedBy } from "./enums/DatabaseUploadedBy";
import { Gender } from "./enums/Gender";
import { GSDUploadingStatus } from "./enums/GSDUploadingStatus";
import { MaterialStatus } from "./enums/MaterialStatus";
import { Readiness } from "./enums/Readiness";
import { ResultType } from "./enums/ResultType";
import { ShipmentMaterialCondition } from "./enums/ShipmentMaterialCondition";
import { YesNoOption } from "./enums/YesNoOption";
import { MaterialGSDInfo } from "./MaterialGSDInfo";

export interface Material {
  Id: string;
  ReferenceNumber: string;
  Name: string;
  Description: string;
  Temperature: number;
  SampleId: string;
  Lineage: string;
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
  Location: string;
  PatientStatus: string;
  Age: number | null;
  Gender: Gender | null;
  CollectionDate: Date | null;
  IsPublicFacing: boolean;
  Approve: boolean;
  Status: MaterialStatus;
  ReadyToShare: boolean;
  OwnerBioHubFacilityId: string | null;
  ShipmentNumberOfVials: number | null;
  CurrentNumberOfVials: number | null;
  WarningEmailCurrentNumberOfVialsThreshold: number | null;
  NumberOfVialsToAdd: number | null;
  ShipmentInformationVisible: boolean;

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

  SharedWithQE: Boolean;
  ShipmentMaterialCondition: ShipmentMaterialCondition;
  ShipmentMaterialConditionNote: string;
}
