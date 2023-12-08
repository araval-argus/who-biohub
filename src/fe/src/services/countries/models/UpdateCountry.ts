export interface UpdateCountryCommand {
  Id: string;
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

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateCountryResponse {}
