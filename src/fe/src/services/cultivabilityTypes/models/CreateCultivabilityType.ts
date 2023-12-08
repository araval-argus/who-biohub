export interface CreateCultivabilityTypeCommand {
  Name: string;
  Description: string;
  IsActive: boolean;
}

export interface CreateCultivabilityTypeResponse {
  Id: string;
}
