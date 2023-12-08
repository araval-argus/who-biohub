import { SMTA2WorkflowItem } from "@/models/SMTA2WorkflowItem";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSMTA2WorkflowHistoryItemQuery {
  SMTA2WorkflowItemId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSMTA2WorkflowHistoryItemResponse {
  SMTA2WorkflowHistoryItems: SMTA2WorkflowItem[];
}
