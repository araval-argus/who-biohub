import { SMTA1WorkflowItem } from "@/models/SMTA1WorkflowItem";

export interface ReadSMTA1WorkflowHistoryItemQuery {
  Id: string;
}

export interface ReadSMTA1WorkflowHistoryItemResponse {
  SMTA1WorkflowHistoryItemDto: SMTA1WorkflowItem;
}
