import { ShipmentDirection } from "./enums/ShipmentDirection";

export interface SMTARequestGridItem {
  Id: string;
  WorkflowItemTitle: string;
  OperationDate: Date;
  SendDate: string;
  SendBy: string;
  Institution: string;
  SMTAType: string;
}
