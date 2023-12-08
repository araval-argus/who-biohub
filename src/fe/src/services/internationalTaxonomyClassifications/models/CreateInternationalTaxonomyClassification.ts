export interface CreateInternationalTaxonomyClassificationCommand {
  Name: string;
  Description: string;
  IsActive: boolean;
}

export interface CreateInternationalTaxonomyClassificationResponse {
  Id: string;
}
