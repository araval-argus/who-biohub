import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { SMTA2WorkflowStatus } from "@/models/enums/SMTA2WorkflowStatus";

export interface UpdateSMTA2WorkflowItemCommand {
  Id: string;
  Comment: string;
  LastSubmissionApproved: boolean;
  OriginalDocumentTemplateSMTA2DocumentId: string;
  CurrentStatus: SMTA2WorkflowStatus;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateSMTA2WorkflowItemResponse {}
