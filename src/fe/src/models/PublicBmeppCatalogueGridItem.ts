export interface PublicBmeppCatalogueGridItem {
  Id: string;
  ReferenceNumber: string;
  Name: string;
  Lineage: string;
  Variant: string;
  ProviderLaboratoryId: string | null;
  ProviderLaboratory: string;
  Country: string;
  Url: string;
  BioHubFacilityDeliveryDateString: string;
}
