import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { SMTA1WorkflowStatus } from "@/models/enums/SMTA1WorkflowStatus";

export interface UpdateSMTA1WorkflowItemCommand {
  Id: string;
  Comment: string;
  LastSubmissionApproved: boolean;
  OriginalDocumentTemplateSMTA1DocumentId: string;
  CurrentStatus: SMTA1WorkflowStatus;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateSMTA1WorkflowItemResponse {}
