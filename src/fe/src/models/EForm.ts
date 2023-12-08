import { EFormType } from "@/models/enums/EFormType";
import { EFormItemType } from "./enums/EFormItemType";

export interface EForm {
  Id: string;
  Name: string;
  Url: string;
  Type: EFormItemType;
  EFormType: EFormType | undefined;
  ApprovedTime: Date;
  UploadedBy: string;
  ParentId: string;
}
