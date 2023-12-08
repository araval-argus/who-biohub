import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { WorklistFromBioHubStatus } from "@/models/enums/WorklistFromBioHubStatus";

export interface UpdateWorklistFromBioHubItemCommand {
  Id: string;
  Comment: string;
  LastSubmissionApproved: boolean;
  OriginalDocumentTemplateSMTA2DocumentId: string;
  DocumentTemplateFileType: DocumentFileType | undefined;
  CurrentStatus: WorklistFromBioHubStatus;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateWorklistFromBioHubItemResponse {}
