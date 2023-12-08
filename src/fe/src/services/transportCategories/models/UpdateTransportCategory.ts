export interface UpdateTransportCategoryCommand {
  Id: string;
  Name: string;
  Description: string;
  HexColor: string;
  IsActive: boolean;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateTransportCategoryResponse {}
