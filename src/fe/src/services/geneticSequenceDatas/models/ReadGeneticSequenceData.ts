import { GeneticSequenceData } from "@/models/GeneticSequenceData";

export interface ReadGeneticSequenceDataQuery {
  Id: string;
}

export interface ReadGeneticSequenceDataResponse {
  GeneticSequenceData: GeneticSequenceData;
}
