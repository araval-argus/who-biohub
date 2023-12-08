import { SMTA2WorkflowStatus } from "./enums/SMTA2WorkflowStatus";

export interface SMTA2WorkflowItemGridItem {
  Id: string;
  WorkflowItemTitle: string;
  CurrentStatus: SMTA2WorkflowStatus;
  CurrentStatusName: string;
  OperationDate: string;
  PreviousActionBy: string;
  NextActionBy: string;
  Institution: string;
  LaboratoryCountry: string;
  UserName: string;
}
