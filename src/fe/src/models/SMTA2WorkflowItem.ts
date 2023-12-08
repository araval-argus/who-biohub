import { SMTA2WorkflowStatus } from "./enums/SMTA2WorkflowStatus";
import { RoleType } from "@/models/enums/RoleType";
import { DocumentFileType } from "./enums/DocumentFileType";

export interface SMTA2WorkflowItem {
  Id: string;
  CurrentStatus: SMTA2WorkflowStatus;
  CurrentStatusName: string;
  PreviousStatus: SMTA2WorkflowStatus;
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
  SMTA2DocumentId: string;
  SMTA2DocumentName: string;
  LaboratoryId: string;
  OriginalDocumentTemplateSMTA2DocumentId: string;
  HistoryDto: boolean;
  ReferenceId: string;
  DocumentTemplateFileType: DocumentFileType;
  CanSkipSMTA2Phase: boolean;
  PreviousActionBy: string;
  NextActionBy: string;
  LaboratoryCountry: string;

  IsPast: boolean;
  AssignedOperationDate: Date | null;
}
