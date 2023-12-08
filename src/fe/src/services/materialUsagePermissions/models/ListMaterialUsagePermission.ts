import { MaterialUsagePermission } from "@/models/MaterialUsagePermission";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListMaterialUsagePermissionQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListMaterialUsagePermissionResponse {
  MaterialUsagePermissions: MaterialUsagePermission[];
}
