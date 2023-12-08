import { DocumentTemplate } from "@/models/DocumentTemplate";

export interface CreateFolderCommand {
  ParentId: string;
  Name: string;
}

export interface CreateFolderResponse {
  DocumentTemplate: DocumentTemplate;
}
