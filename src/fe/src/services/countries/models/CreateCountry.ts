export interface CreateCountryCommand {
  Uncode: string;
  Name: string;
  FullName: string;
  Iso2: string;
  Iso3: string;
  Latitude: number;
  Longitude: number;
  GmtHour: number;
  GmtMin: number;
  IsActive: boolean;
}

export interface CreateCountryResponse {
  Id: string;
}
