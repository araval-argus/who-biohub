import { SMTA1WorkflowItem } from "@/models/SMTA1WorkflowItem";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSMTA1WorkflowHistoryItemQuery {
  SMTA1WorkflowItemId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSMTA1WorkflowHistoryItemResponse {
  SMTA1WorkflowHistoryItems: SMTA1WorkflowItem[];
}
