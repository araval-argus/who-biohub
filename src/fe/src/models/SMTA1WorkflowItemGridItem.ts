import { SMTA1WorkflowStatus } from "./enums/SMTA1WorkflowStatus";

export interface SMTA1WorkflowItemGridItem {
  Id: string;
  WorkflowItemTitle: string;
  CurrentStatus: SMTA1WorkflowStatus;
  CurrentStatusName: string;
  OperationDate: string;
  PreviousActionBy: string;
  NextActionBy: string;
  Institution: string;
  LaboratoryCountry: string;
  UserName: string;
}
