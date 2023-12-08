import { GeneticSequenceData } from "@/models/GeneticSequenceData";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListGeneticSequenceDataQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListGeneticSequenceDataResponse {
  GeneticSequenceDatas: GeneticSequenceData[];
}
