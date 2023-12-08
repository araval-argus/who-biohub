import { ResourceFileType } from "./enums/ResourceFileType";
import { ResourceType } from "./enums/ResourceType";

export interface ResourcePublic {
  Id: string;
  FileCompleteName: string;
  Name: string;
  Extension: string;
  Type: ResourceType;
  FileType: ResourceFileType | undefined;
  ParentId: string;
}
