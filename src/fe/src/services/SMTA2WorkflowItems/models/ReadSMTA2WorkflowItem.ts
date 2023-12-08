import { SMTA2WorkflowItem } from "@/models/SMTA2WorkflowItem";

export interface ReadSMTA2WorkflowItemQuery {
  Id: string;
}

export interface ReadSMTA2WorkflowItemResponse {
  SMTA2WorkflowItemDto: SMTA2WorkflowItem;
}
