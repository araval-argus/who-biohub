import { DocumentTemplate } from "@/models/DocumentTemplate";

export interface UpdateNameCommand {
  Id: string;
  Name: string;
  Current: boolean | undefined;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateNameResponse {
  DocumentTemplate: DocumentTemplate;
}
