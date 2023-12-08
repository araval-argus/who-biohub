import { DocumentType } from "@/models/enums/DocumentType";

export interface DocumentTemplateGridItem {
  Id: string;
  Name: string;
  Extension: string | undefined;
  Type: DocumentType;
  FileType: string;
  Current: boolean | undefined;
  UploadTime: string;
  UploadedBy: string;
  ParentId: string | undefined;
}
