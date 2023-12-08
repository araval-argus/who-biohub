import { MaterialStatus } from "./enums/MaterialStatus";
import { ShipmentMaterialCondition } from "./enums/ShipmentMaterialCondition";

export interface MaterialGridItem {
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
  OwnerBioHubFacilityId: string | null;
  OwnerBioHubFacility: string;
  Status: MaterialStatus;
  SharedWithQE: Boolean;
  ShipmentMaterialCondition: ShipmentMaterialCondition;
}
