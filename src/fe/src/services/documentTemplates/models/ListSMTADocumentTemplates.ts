import { DocumentTemplate } from "@/models/DocumentTemplate";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSMTADocumentTemplatesQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSMTADocumentTemplatesResponse {
  DocumentTemplates: DocumentTemplate[];
}
