export interface CreateMaterialUsagePermissionCommand {
  Name: string;
  Description: string;
  IsActive: boolean;
}

export interface CreateMaterialUsagePermissionResponse {
  Id: string;
}
