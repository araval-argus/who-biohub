import { DocumentType } from "@/models/enums/DocumentType";

export interface ShipmentDocumentGridItem {
  Id: string;
  Name: string;
  Extension: string | undefined;
  FileType: string;
  UploadTime: string;
  UploadedBy: string;
}
