import { SMTA1WorkflowStatus } from "./enums/SMTA1WorkflowStatus";
import { RoleType } from "@/models/enums/RoleType";
import { DocumentFileType } from "./enums/DocumentFileType";

export interface SMTA1WorkflowItem {
  Id: string;
  CurrentStatus: SMTA1WorkflowStatus;
  CurrentStatusName: string;
  PreviousStatus: SMTA1WorkflowStatus;
  WorkflowItemTitle: string;
  OperationDate: Date;
  LastSubmissionApproved: boolean;
  LaboratoryName: string;
  LaboratoryAbbreviation: string;
  UserName: string;
  UserRoleName: string;
  UserRoleTypeName: string;
  UserRoleType: RoleType;
  Comment: string;
  SMTA1DocumentId: string;
  SMTA1DocumentName: string;
  LaboratoryId: string;
  OriginalDocumentTemplateSMTA1DocumentId: string;
  HistoryDto: boolean;
  ReferenceId: string;
  DocumentTemplateFileType: DocumentFileType;
  CanSkipSMTA1Phase: boolean;
  PreviousActionBy: string;
  NextActionBy: string;
  LaboratoryCountry: string;
  IsPast: boolean;
  AssignedOperationDate: Date | null;
}
