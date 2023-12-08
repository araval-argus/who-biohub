import { Document } from "@/models/Document";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSignedSMTADocumentsQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSignedSMTADocumentsResponse {
  Documents: Document[];
}
