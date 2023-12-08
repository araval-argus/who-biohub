export interface CreateMaterialTypeCommand {
  Name: string;
  Description: string;
  IsActive: boolean;
}

export interface CreateMaterialTypeResponse {
  Id: string;
}
