import { ResourceFileType } from "./enums/ResourceFileType";
import { ResourceType } from "./enums/ResourceType";

export interface Resource {
  Id: string;
  Name: string;
  Extension: string | undefined;
  Type: ResourceType;
  FileType: ResourceFileType | undefined;
  Current: boolean | undefined;
  UploadTime: Date;
  UploadedBy: string;
  ParentId: string;
}
