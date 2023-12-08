import { Document } from "@/models/Document";
import { DocumentFileType } from "@/models/enums/DocumentFileType";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface CanStartSMTARequestQuery {
  Type: DocumentFileType;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface CanStartSMTARequestResponse {
  CanStartSMTARequest: boolean;
}
