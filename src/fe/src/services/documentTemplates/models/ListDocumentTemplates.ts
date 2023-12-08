import { DocumentTemplate } from "@/models/DocumentTemplate";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListDocumentTemplatesQuery {
  Id: string | undefined;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListDocumentTemplatesResponse {
  DocumentTemplates: DocumentTemplate[];
}
