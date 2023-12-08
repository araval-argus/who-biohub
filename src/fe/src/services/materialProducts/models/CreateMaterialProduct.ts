export interface CreateMaterialProductCommand {
  Name: string;
  Description: string;
  IsActive: boolean;
}

export interface CreateMaterialProductResponse {
  Id: string;
}
