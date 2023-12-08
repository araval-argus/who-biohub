export interface CreateLaboratoryCommand {
  Name: string;
  Abbreviation: string;
  IsActive: boolean;
  Description: string;
  Address: string;
  Latitude: number;
  Longitude: number;
  BSLLevelId: string;
  CountryId: string;
  IsPublicFacing: boolean;
}

export interface CreateLaboratoryResponse {
  Id: string;
}
