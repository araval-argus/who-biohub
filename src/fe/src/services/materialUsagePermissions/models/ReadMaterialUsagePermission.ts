import { MaterialUsagePermission } from "@/models/MaterialUsagePermission";

export interface ReadMaterialUsagePermissionQuery {
  Id: string;
}

export interface ReadMaterialUsagePermissionResponse {
  MaterialUsagePermission: MaterialUsagePermission;
}
