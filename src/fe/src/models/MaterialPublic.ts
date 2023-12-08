export interface MaterialPublic {
  Id: string;
  ReferenceNumber: string;
  Name: string;
  Temperature: number;
  ProductTypeId: string;
  OriginalProductTypeId: string;
  Lineage: string;
  Variant: string;
  VariantAssessment: string;
  GMO: boolean;
  TypeId: string;
  SuspectedEpidemiologicalOriginId: string;
  UnitOfMeasureId: string;
  UsagePermissionId: string;
  GeneticSequenceDataId: string;
  InternationalTaxonomyClassificationId: string;
  IsolationHostTypeId: string;
  CultivabilityTypeId: string;
  IsolationTechniqueTypeId: string;
  ProviderLaboratoryId: string | null;
  ProviderBioHubFacilityId: string | null;
  BioHubFacilityDeliveryDate: Date;
}
