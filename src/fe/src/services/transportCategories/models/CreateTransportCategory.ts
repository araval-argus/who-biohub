export interface CreateTransportCategoryCommand {
  Name: string;
  Description: string;
  HexColor: string;
  IsActive: boolean;
}

export interface CreateTransportCategoryResponse {
  Id: string;
}
