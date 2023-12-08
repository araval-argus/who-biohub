import { Document } from "@/models/Document";
import { DocumentFileType } from "@/models/enums/DocumentFileType";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface CheckDocumentQuery {
  Type: DocumentFileType;
  LaboratoryId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface CheckDocumentResponse {
  IsSigned: boolean;
}
