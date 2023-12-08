import { ResourceType } from "./enums/ResourceType";

export interface ResourcePublicGridItem {
  Id: string;
  FileCompleteName: string;
  Name: string;
  Extension: string;
  Type: ResourceType;
  FileType: string;
  ParentId: string | undefined;
}
