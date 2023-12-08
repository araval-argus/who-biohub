export interface CreateCourierCommand {
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

export interface CreateCourierResponse {
  Id: string;
}
