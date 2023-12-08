import { InternationalTaxonomyClassification } from "@/models/InternationalTaxonomyClassification";

export interface ReadInternationalTaxonomyClassificationQuery {
  Id: string;
}

export interface ReadInternationalTaxonomyClassificationResponse {
  InternationalTaxonomyClassification: InternationalTaxonomyClassification;
}
