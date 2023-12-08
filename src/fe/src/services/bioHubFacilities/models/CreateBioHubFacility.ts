export interface CreateBioHubFacilityCommand {
  Name: string;
  Abbreviation: string;
  IsActive: boolean;
  Description: string;
  Address: string;
  Latitude: number;
  Longitude: number;
  BSLLevelId: string;
  CountryId: string;
}

export interface CreateBioHubFacilityResponse {
  Id: string;
}
