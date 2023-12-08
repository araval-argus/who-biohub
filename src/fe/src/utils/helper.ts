import { AuthModule } from "@/features/auth/store";
import { BreakpointName } from "vuetify/types/services/breakpoint";
import { RoleType } from "@/models/enums/RoleType";
import { AreaType } from "@/models/enums/AreaType";
import { FormPopupItem } from "@/models/FormPopupItem";
import { InputType } from "@/models/enums/InputType";
import { SMTA1WorkflowStatus } from "@/models/enums/SMTA1WorkflowStatus";
import { SMTA2WorkflowStatus } from "@/models/enums/SMTA2WorkflowStatus";
import { PermissionType } from "@/models/enums/PermissionType";
import { SMTA1WorkflowReadPermissionsByStatusPastList } from "@/models/constants/SMTA1WorkflowReadPermissionsByStatus";
import { SMTA1WorkflowReadPermissionsByStatusList } from "@/models/constants/SMTA1WorkflowReadPermissionsByStatus";
import { SMTA2WorkflowReadPermissionsByStatusPastList } from "@/models/constants/SMTA2WorkflowReadPermissionsByStatus";
import { SMTA2WorkflowReadPermissionsByStatusList } from "@/models/constants/SMTA2WorkflowReadPermissionsByStatus";

import { SMTA1WorkflowSubmitPermissionsByStatusPastList } from "@/models/constants/SMTA1WorkflowSubmitPermissionsByStatus";
import { SMTA1WorkflowSubmitPermissionsByStatusList } from "@/models/constants/SMTA1WorkflowSubmitPermissionsByStatus";
import { SMTA2WorkflowSubmitPermissionsByStatusPastList } from "@/models/constants/SMTA2WorkflowSubmitPermissionsByStatus";
import { SMTA2WorkflowSubmitPermissionsByStatusList } from "@/models/constants/SMTA2WorkflowSubmitPermissionsByStatus";

import { SMTA1WorkflowDownloadPermissionsByStatusPastList } from "@/models/constants/SMTA1WorkflowDownloadPermissionsByStatus";
import { SMTA1WorkflowDownloadPermissionsByStatusList } from "@/models/constants/SMTA1WorkflowDownloadPermissionsByStatus";
import { SMTA2WorkflowDownloadPermissionsByStatusPastList } from "@/models/constants/SMTA2WorkflowDownloadPermissionsByStatus";
import { SMTA2WorkflowDownloadPermissionsByStatusList } from "@/models/constants/SMTA2WorkflowDownloadPermissionsByStatus";

import { WorklistFromBioHubReadPermissionsByStatusPastList } from "@/models/constants/WorklistFromBioHubReadPermissionsByStatus";
import { WorklistFromBioHubReadPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubReadPermissionsByStatus";
import { WorklistToBioHubReadPermissionsByStatusPastList } from "@/models/constants/WorklistToBioHubReadPermissionsByStatus";
import { WorklistToBioHubReadPermissionsByStatusList } from "@/models/constants/WorklistToBioHubReadPermissionsByStatus";

import { WorklistFromBioHubSubmitPermissionsByStatusPastList } from "@/models/constants/WorklistFromBioHubSubmitPermissionsByStatus";
import { WorklistFromBioHubSubmitPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubSubmitPermissionsByStatus";
import { WorklistToBioHubSubmitPermissionsByStatusPastList } from "@/models/constants/WorklistToBioHubSubmitPermissionsByStatus";
import { WorklistToBioHubSubmitPermissionsByStatusList } from "@/models/constants/WorklistToBioHubSubmitPermissionsByStatus";

import { WorklistFromBioHubDownloadPermissionsByStatusPastList } from "@/models/constants/WorklistFromBioHubDownloadPermissionsByStatus";
import { WorklistFromBioHubDownloadPermissionsByStatusList } from "@/models/constants/WorklistFromBioHubDownloadPermissionsByStatus";
import { WorklistToBioHubDownloadPermissionsByStatusPastList } from "@/models/constants/WorklistToBioHubDownloadPermissionsByStatus";
import { WorklistToBioHubDownloadPermissionsByStatusList } from "@/models/constants/WorklistToBioHubDownloadPermissionsByStatus";

import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";

/* TODO: this is a temporary function, probably in the future it will be removed or changed */
export function getMaterialProviderId(routeProviderId: string): string {
  let providerId = "";
  if (
    routeProviderId == undefined ||
    routeProviderId == null ||
    routeProviderId == ""
  ) {
    if (
      AuthModule.UserLoginInfo != undefined &&
      AuthModule.RoleType == RoleType.Laboratory
    ) {
      if (AuthModule.LaboratoryId != undefined) {
        providerId = AuthModule.LaboratoryId ?? "";
      }
    } else if (
      AuthModule.UserLoginInfo != undefined &&
      AuthModule.RoleType == RoleType.BioHubFacility
    ) {
      providerId = AuthModule.BioHubFacilityId ?? "";
    }
  } else {
    providerId = routeProviderId;
  }
  if (providerId == null || providerId == undefined) {
    providerId = "";
  }
  return providerId;
}

/* TODO: this is a temporary function, probably in the future it will be removed or changed */
export function getUserBioHubFacilityOrLaboratoryOrCourierId(
  routeLabId: string | undefined
): string {
  if (routeLabId === undefined || routeLabId === null || routeLabId === "") {
    if (
      AuthModule.UserLoginInfo != undefined &&
      AuthModule.RoleType == RoleType.Laboratory
    ) {
      if (AuthModule.LaboratoryId != undefined) {
        return AuthModule.LaboratoryId;
      }
      return "";
    } else if (
      AuthModule.UserLoginInfo != undefined &&
      AuthModule.RoleType == RoleType.BioHubFacility
    ) {
      return AuthModule.UserLoginInfo.BioHubFacilityId;
    }
  } else if (
    AuthModule.UserLoginInfo != undefined &&
    AuthModule.RoleType == RoleType.Courier
  ) {
    return AuthModule.UserLoginInfo.CourierId;
  }
  return routeLabId ?? "";
}

export function canAccessThisArea(routeName: string): boolean {
  const areaType = getAreaFromRouteName(routeName);
  const roleType = AuthModule.RoleType;

  if (areaType != AreaType.Public) {
    if (roleType == undefined) {
      return false;
    }
    if (areaType == AreaType.WHO && roleType != RoleType.WHO) {
      return false;
    }
    if (areaType == AreaType.Laboratory && roleType != RoleType.Laboratory) {
      return false;
    }
    if (
      areaType == AreaType.BioHubFacility &&
      roleType != RoleType.BioHubFacility
    ) {
      return false;
    }
  }
  return true;
}

export function getAreaFromRouteName(routeName: string): AreaType {
  if (routeName.startsWith("whoarea")) {
    return AreaType.WHO;
  } else if (routeName.startsWith("laboratoryarea")) {
    return AreaType.Laboratory;
  } else if (routeName.startsWith("biohubfacilityarea")) {
    return AreaType.BioHubFacility;
  }

  return AreaType.Public;
}

export function IsMobile(sizeWindow: BreakpointName) {
  switch (sizeWindow) {
    case "xs":
    case "sm":
    case "md":
      return true;
    default:
      return false;
  }
}

export function customTableSearch(
  value: any,
  search: string | null,
  item: any
): boolean {
  let result: boolean = value != null && search != null;

  if (value != null && search != null) {
    if (typeof value === "string") {
      result = value.toString().toLowerCase().includes(search.toLowerCase());
    } else if (typeof value === "boolean") {
      const valueBoolToString = value ? "yes" : "no";
      result = valueBoolToString.includes(search.toLowerCase());
    }
  }

  return result;
}

export function createFormPopupItem(
  type: InputType,
  label: string,
  propertyName: string,
  required = false,
  readonly = false,
  hide = false,
  value?: unknown,
  items: Array<any> = []
): FormPopupItem {
  const formPopupItem = {
    Type: type,
    Label: label,
    PropertyName: propertyName,
    Items: items,
    Required: required,
    Readonly: readonly,
    Hide: hide,
  } as FormPopupItem;

  if (value) {
    formPopupItem.Value = value;
  } else {
    if (
      type == InputType.String ||
      type == InputType.TextArea ||
      type == InputType.TextEditor
    ) {
      formPopupItem.Value = "";
    } else if (type == InputType.Number) {
      formPopupItem.Value = "";
    } else if (type == InputType.File) {
      formPopupItem.Value = undefined;
    } else if (type == InputType.Files) {
      formPopupItem.Value = [];
    }
  }

  return formPopupItem;
}

export function hasPermission(permissionName: string): boolean {
  const permission = AuthModule.Permissions?.filter((p) => {
    return p == permissionName;
  });
  if (permission !== undefined) {
    return permission.length > 0;
  }
  return false;
}

export function saveDownloadedFile(
  buffer: ArrayBuffer,
  filename: string
): void {
  const url = window.URL.createObjectURL(new Blob([buffer]));
  const link = document.createElement("a");
  link.href = url;
  link.setAttribute("download", filename);
  document.body.appendChild(link);
  link.click();
}

export function getAreaFromRoleType(): string {
  if (AuthModule.RoleType == RoleType.BioHubFacility) {
    return "biohubfacilityarea";
  } else if (AuthModule.RoleType == RoleType.Laboratory) {
    return "laboratoryarea";
  } else if (AuthModule.RoleType == RoleType.WHO) {
    return "whoarea";
  }

  return "public";
}

export function GetSMTA1WorkflowStatusPermission(
  status: SMTA1WorkflowStatus,
  type: PermissionType,
  isPast: boolean
): string {
  if (type == PermissionType.Read) {
    if (isPast == true) {
      return SMTA1WorkflowReadPermissionsByStatusPastList.get(status) ?? "";
    } else {
      return SMTA1WorkflowReadPermissionsByStatusList.get(status) ?? "";
    }
  } else if (type == PermissionType.Update) {
    if (isPast == true) {
      return SMTA1WorkflowSubmitPermissionsByStatusPastList.get(status) ?? "";
    } else {
      return SMTA1WorkflowSubmitPermissionsByStatusList.get(status) ?? "";
    }
  } else if (type == PermissionType.DownloadFile) {
    if (isPast == true) {
      return SMTA1WorkflowDownloadPermissionsByStatusPastList.get(status) ?? "";
    } else {
      return SMTA1WorkflowDownloadPermissionsByStatusList.get(status) ?? "";
    }
  } else {
    return "";
  }
}

export function GetSMTA2WorkflowStatusPermission(
  status: SMTA2WorkflowStatus,
  type: PermissionType,
  isPast: boolean
): string {
  if (type == PermissionType.Read) {
    if (isPast == true) {
      return SMTA2WorkflowReadPermissionsByStatusPastList.get(status) ?? "";
    } else {
      return SMTA2WorkflowReadPermissionsByStatusList.get(status) ?? "";
    }
  } else if (type == PermissionType.Update) {
    if (isPast == true) {
      return SMTA2WorkflowSubmitPermissionsByStatusPastList.get(status) ?? "";
    } else {
      return SMTA2WorkflowSubmitPermissionsByStatusList.get(status) ?? "";
    }
  } else if (type == PermissionType.DownloadFile) {
    if (isPast == true) {
      return SMTA2WorkflowDownloadPermissionsByStatusPastList.get(status) ?? "";
    } else {
      return SMTA2WorkflowDownloadPermissionsByStatusList.get(status) ?? "";
    }
  } else {
    return "";
  }
}

export function GetWorklistFromBioHubStatusPermission(
  status: WorklistFromBioHubStatus,
  type: PermissionType,
  isPast: boolean
): string {
  if (type == PermissionType.Read) {
    if (isPast == true) {
      return (
        WorklistFromBioHubReadPermissionsByStatusPastList.get(status) ?? ""
      );
    } else {
      return WorklistFromBioHubReadPermissionsByStatusList.get(status) ?? "";
    }
  } else if (type == PermissionType.Update) {
    if (isPast == true) {
      return (
        WorklistFromBioHubSubmitPermissionsByStatusPastList.get(status) ?? ""
      );
    } else {
      return WorklistFromBioHubSubmitPermissionsByStatusList.get(status) ?? "";
    }
  } else if (type == PermissionType.DownloadFile) {
    if (isPast == true) {
      return (
        WorklistFromBioHubDownloadPermissionsByStatusPastList.get(status) ?? ""
      );
    } else {
      return (
        WorklistFromBioHubDownloadPermissionsByStatusList.get(status) ?? ""
      );
    }
  } else {
    return "";
  }
}

export function GetWorklistToBioHubStatusPermission(
  status: WorklistToBioHubStatus,
  type: PermissionType,
  isPast: boolean
): string {
  if (type == PermissionType.Read) {
    if (isPast == true) {
      return WorklistToBioHubReadPermissionsByStatusPastList.get(status) ?? "";
    } else {
      return WorklistToBioHubReadPermissionsByStatusList.get(status) ?? "";
    }
  } else if (type == PermissionType.Update) {
    if (isPast == true) {
      return (
        WorklistToBioHubSubmitPermissionsByStatusPastList.get(status) ?? ""
      );
    } else {
      return WorklistToBioHubSubmitPermissionsByStatusList.get(status) ?? "";
    }
  } else if (type == PermissionType.DownloadFile) {
    if (isPast == true) {
      return (
        WorklistToBioHubDownloadPermissionsByStatusPastList.get(status) ?? ""
      );
    } else {
      return WorklistToBioHubDownloadPermissionsByStatusList.get(status) ?? "";
    }
  } else {
    return "";
  }
}
