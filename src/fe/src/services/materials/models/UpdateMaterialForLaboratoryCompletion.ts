import { DatabaseUploadedBy } from "@/models/enums/DatabaseUploadedBy";
import { Gender } from "@/models/enums/Gender";
import { GSDUploadingStatus } from "@/models/enums/GSDUploadingStatus";
import { MaterialValidationSelection } from "@/models/enums/MaterialValidationSelection";
import { Readiness } from "@/models/enums/Readiness";
import { ResultType } from "@/models/enums/ResultType";
import { ShipmentMaterialCondition } from "@/models/enums/ShipmentMaterialCondition";
import { YesNoOption } from "@/models/enums/YesNoOption";
import { MaterialGSDInfo } from "@/models/MaterialGSDInfo";

export interface UpdateMaterialForLaboratoryCompletionCommand {
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

  ReferenceNumberValidation: MaterialValidationSelection;
  NameValidation: MaterialValidationSelection;
  DescriptionValidation: MaterialValidationSelection;
  TypeValidation: MaterialValidationSelection;
  SuspectedEpidemiologicalOriginValidation: MaterialValidationSelection;
  ProductTypeValidation: MaterialValidationSelection;
  TransportCategoryValidation: MaterialValidationSelection;
  TemperatureValidation: MaterialValidationSelection;
  UnitOfMeasureValidation: MaterialValidationSelection;
  UsagePermissionValidation: MaterialValidationSelection;
  SampleIdValidation: MaterialValidationSelection;
  LineageValidation: MaterialValidationSelection;
  VariantValidation: MaterialValidationSelection;
  VariantAssessmentValidation: MaterialValidationSelection;
  StrainDesignationValidation: MaterialValidationSelection;
  GenotypeValidation: MaterialValidationSelection;
  SerotypeValidation: MaterialValidationSelection;
  GeneticSequenceDataValidation: MaterialValidationSelection;
  DatabaseAccessionIdValidation: MaterialValidationSelection;
  OriginalGeneticSequenceValidation: MaterialValidationSelection;

  FacilityGSDValidation: MaterialValidationSelection;
  InternationalTaxonomyClassificationValidation: MaterialValidationSelection;
  GMOValidation: MaterialValidationSelection;
  IsolationHostTypeValidation: MaterialValidationSelection;
  CultivabilityTypeValidation: MaterialValidationSelection;
  //ProductionCellLineValidation: MaterialValidationSelection;
  IsolationTechniqueTypeValidation: MaterialValidationSelection;
  InfectivityValidation: MaterialValidationSelection;
  ViralTiterValidation: MaterialValidationSelection;
  CollectionDateValidation: MaterialValidationSelection;
  LocationValidation: MaterialValidationSelection;
  GenderValidation: MaterialValidationSelection;
  AgeValidation: MaterialValidationSelection;
  PatientStatusValidation: MaterialValidationSelection;
  ReferenceNumberComment: string;
  NameComment: string;
  DescriptionComment: string;
  TypeComment: string;
  SuspectedEpidemiologicalOriginComment: string;
  ProductTypeComment: string;
  TransportCategoryComment: string;
  TemperatureComment: string;
  UnitOfMeasureComment: string;
  UsagePermissionComment: string;
  SampleIdComment: string;
  LineageComment: string;
  VariantComment: string;
  VariantAssessmentComment: string;
  StrainDesignationComment: string;
  GenotypeComment: string;
  SerotypeComment: string;
  GeneticSequenceDataComment: string;
  DatabaseAccessionIdComment: string;
  OriginalGeneticSequenceComment: string;

  FacilityGSDComment: string;
  InternationalTaxonomyClassificationComment: string;
  GMOComment: string;
  IsolationHostTypeComment: string;
  CultivabilityTypeComment: string;
  //ProductionCellLineComment: string;
  IsolationTechniqueTypeComment: string;
  InfectivityComment: string;
  ViralTiterComment: string;
  CollectionDateComment: string;
  LocationComment: string;
  GenderComment: string;
  AgeComment: string;
  PatientStatusComment: string;

  DateOfBMEPPReceipt: Date | null;
  OriginalProductTypeId: string;

  BHFShareReadiness: Readiness | null;
  PublicShare: YesNoOption;

  OriginalProductTypeValidation: MaterialValidationSelection;

  OriginalProductTypeComment: string;

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

  PathogenValidation: MaterialValidationSelection;
  FreezingDateValidation: MaterialValidationSelection;
  VirusConcentrationValidation: MaterialValidationSelection;
  ShipmentAmountValidation: MaterialValidationSelection;
  ShipmentTemperatureValidation: MaterialValidationSelection;
  ShipmentUnitOfMeasureValidation: MaterialValidationSelection;
  CulturingCellLineValidation: MaterialValidationSelection;
  CulturingPassagesNumberValidation: MaterialValidationSelection;
  TypeOfTransportMediumValidation: MaterialValidationSelection;
  BrandOfTransportMediumValidation: MaterialValidationSelection;
  MaterialCollectedSpecimenTypesValidation: MaterialValidationSelection;
  DatabaseUploadedByValidation: MaterialValidationSelection;

  PathogenComment: string;
  FreezingDateComment: string;
  VirusConcentrationComment: string;
  ShipmentAmountComment: string;
  ShipmentTemperatureComment: string;
  ShipmentUnitOfMeasureComment: string;
  CulturingCellLineComment: string;
  CulturingPassagesNumberComment: string;
  TypeOfTransportMediumComment: string;
  BrandOfTransportMediumComment: string;
  MaterialCollectedSpecimenTypesComment: string;
  DatabaseUploadedByComment: string;

  CulturingResult: ResultType;
  CulturingResultDate: Date | null;
  QualityControlResult: ResultType;
  QualityControlResultDate: Date | null;
  GSDAnalysisResult: ResultType;
  GSDAnalysisResultDate: Date | null;
  GSDUploadingStatus: GSDUploadingStatus;
  GSDUploadingDate: Date | null;

  LastAliquotsAdditionDate: Date | null;

  CulturingResultValidation: MaterialValidationSelection;
  CulturingResultDateValidation: MaterialValidationSelection;
  QualityControlResultValidation: MaterialValidationSelection;
  QualityControlResultDateValidation: MaterialValidationSelection;
  GSDAnalysisResultValidation: MaterialValidationSelection;
  GSDAnalysisResultDateValidation: MaterialValidationSelection;
  GSDUploadingStatusValidation: MaterialValidationSelection;
  GSDUploadingDateValidation: MaterialValidationSelection;

  CulturingResultComment: string;
  CulturingResultDateComment: string;
  QualityControlResultComment: string;
  QualityControlResultDateComment: string;
  GSDAnalysisResultComment: string;
  GSDAnalysisResultDateComment: string;
  GSDUploadingStatusComment: string;
  GSDUploadingDateComment: string;

  MaterialGSDInfo: MaterialGSDInfo[];
  MaterialGSDInfoValidation: MaterialValidationSelection;
  MaterialGSDInfoComment: string;

  DateOfBMEPPReceiptValidation: MaterialValidationSelection;
  DateOfBMEPPReceiptComment: string;

  OwnerBioHubFacilityValidation: MaterialValidationSelection;
  OwnerBioHubFacilityComment: string;

  ShipmentMaterialCondition: ShipmentMaterialCondition;
  ShipmentMaterialConditionValidation: MaterialValidationSelection;
  ShipmentMaterialConditionComment: string;

  ShipmentMaterialConditionNote: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateMaterialForLaboratoryCompletionResponse {}
