import { Gender } from "@/models/enums/Gender";
import { Readiness } from "@/models/enums/Readiness";
import { YesNoOption } from "@/models/enums/YesNoOption";

export interface CreateMaterialCommand {
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
  OwnerBioHubFacilityId: string | null;
  CollectionDate: Date | null;
  Location: string;
  PatientStatus: string;
  Age: number | null;
  Gender: Gender | null;
  DateOfBMEPPReceipt: Date | null;
  OriginalProductTypeId: string;
  GSDCulturedMaterialCellLine3: string;
  CulturingCellLine1: string;
  CulturingCellLine2: string;
  CulturingCellLine3: string;
  BHFShareReadiness: Readiness | null;
  PublicShare: YesNoOption;
}

export interface CreateMaterialResponse {
  Id: string;
}
