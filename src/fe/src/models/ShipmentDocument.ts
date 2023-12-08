import { DocumentFileType } from "./enums/DocumentFileType";

export interface ShipmentDocument {
  Id: string;
  Name: string;
  Extension: string | undefined;
  FileType: DocumentFileType;
  UploadTime: string;
  UploadedBy: string;
}
