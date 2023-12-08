import { DocumentType } from "@/models/enums/DocumentType";
import { DocumentFileType } from "./enums/DocumentFileType";

export interface Document {
  Id: string;
  Name: string;
  Extension: string | undefined;
  Type: DocumentType;
  FileType: DocumentFileType | undefined;
  Current: boolean | undefined;
  UploadTime: Date;
  UploadedBy: string;
  ParentId: string;
}
