import { EFormItemType } from "./enums/EFormItemType";

export interface EFormGridItem {
  Id: string;
  Name: string;
  Url: string;
  Type: EFormItemType;
  EFormType: string;
  Current: boolean | undefined;
  ApprovedTime: string;
  ParentId: string | undefined;
}
