export interface UpdateLaboratoryCommand {
  Id: string;
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

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateLaboratoryResponse {}
