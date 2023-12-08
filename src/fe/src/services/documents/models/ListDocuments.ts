import { Document } from "@/models/Document";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListDocumentsQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListDocumentsResponse {
  Documents: Document[];
}
