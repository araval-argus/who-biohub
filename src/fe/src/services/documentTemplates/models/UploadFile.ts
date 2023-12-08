import { DocumentTemplate } from "@/models/DocumentTemplate";
import { DocumentFileType } from "@/models/enums/DocumentFileType";

export interface UploadFileCommand {
  ParentId: string;
  DocumentTemplateFileType: DocumentFileType | undefined;
}

export interface UploadFileCommandResponse {
  DocumentTemplate: DocumentTemplate;
}
