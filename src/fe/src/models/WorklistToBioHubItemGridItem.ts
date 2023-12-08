import { WorklistToBioHubStatus } from "./enums/WorklistToBioHubStatus";

export interface WorklistToBioHubItemGridItem {
  Id: string;
  WorklistItemTitle: string;
  CurrentStatus: WorklistToBioHubStatus;
  CurrentStatusName: string;
  OperationDate: string;
  PreviousActionBy: string;
  NextActionBy: string;
  Institution: string;
  LaboratoryCountryName: string;
  UserName: string;
}
