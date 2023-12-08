import { WorklistFromBioHubStatus } from "./enums/WorklistFromBioHubStatus";

export interface WorklistFromBioHubItemGridItem {
  Id: string;
  WorklistItemTitle: string;
  CurrentStatus: WorklistFromBioHubStatus;
  CurrentStatusName: string;
  OperationDate: string;
  PreviousActionBy: string;
  NextActionBy: string;
  Institution: string;
  LaboratoryCountryName: string;
  UserName: string;
}
