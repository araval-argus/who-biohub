import { ResourceType } from "./enums/ResourceType";

export interface ResourceGridItem {
  Id: string;
  Name: string;
  Extension: string | undefined;
  Type: ResourceType;
  FileType: string;
  UploadTime: string;
  UploadedBy: string;
  ParentId: string | undefined;
}
