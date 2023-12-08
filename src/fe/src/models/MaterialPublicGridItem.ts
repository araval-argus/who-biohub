import { MaterialStatus } from "./enums/MaterialStatus";

export interface MaterialPublicGridItem {
  Id: string;
  ReferenceNumber: string;
  Name: string;
  Lineage: string;
  Variant: string;
  InternationalTaxonomyClassificationId: string;
  InternationalTaxonomyClassification: string;
  ProviderLaboratoryId: string | null;
  ProviderLaboratory: string;
  ProviderBioHubFacilityId: string | null;
  ProviderBioHubFacility: string;
}
