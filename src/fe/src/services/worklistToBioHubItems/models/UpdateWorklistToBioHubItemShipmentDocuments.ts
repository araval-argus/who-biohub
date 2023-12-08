import { DocumentFileType } from "@/models/enums/DocumentFileType";
import { WorklistToBioHubStatus } from "@/models/enums/WorklistToBioHubStatus";

export interface UpdateWorklistToBioHubItemShipmentDocumentsCommand {
  Id: string;
  Comment: string;
  LastSubmissionApproved: boolean;
  OriginalDocumentTemplateSMTA1DocumentId: string;
  DocumentTemplateFileType: DocumentFileType | undefined;
  CurrentStatus: WorklistToBioHubStatus;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface UpdateWorklistToBioHubItemShipmentDocumentsResponse {}
