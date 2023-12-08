export interface CreateGeneticSequenceDataCommand {
  Name: string;
  Code: string;
  Description: string;
  IsActive: boolean;
}

export interface CreateGeneticSequenceDataResponse {
  Id: string;
}
