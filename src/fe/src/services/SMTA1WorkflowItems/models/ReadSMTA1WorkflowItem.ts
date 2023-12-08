import { SMTA1WorkflowItem } from "@/models/SMTA1WorkflowItem";

export interface ReadSMTA1WorkflowItemQuery {
  Id: string;
}

export interface ReadSMTA1WorkflowItemResponse {
  SMTA1WorkflowItemDto: SMTA1WorkflowItem;
}
