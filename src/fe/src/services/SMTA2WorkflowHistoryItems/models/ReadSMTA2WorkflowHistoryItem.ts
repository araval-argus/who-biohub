import { SMTA2WorkflowItem } from "@/models/SMTA2WorkflowItem";

export interface ReadSMTA2WorkflowHistoryItemQuery {
  Id: string;
}

export interface ReadSMTA2WorkflowHistoryItemResponse {
  SMTA2WorkflowHistoryItemDto: SMTA2WorkflowItem;
}
