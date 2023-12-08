export interface UpdateCourierCommand {
  Id: string;
  Name: string;
  WHOAccountNumber: string;
  IsActive: boolean;
  Description: string;
  Address: string;
  Email: string;
  Latitude: number;
  Longitude: number;
  CountryId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateCourierResponse {}
