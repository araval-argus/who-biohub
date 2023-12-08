import { WorklistTimeline } from "@/models/WorklistTimeline";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSMTA2WorkflowItemEventQuery {
  SMTA2WorkflowItemId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSMTA2WorkflowItemEventResponse {
  WorklistTimelines: WorklistTimeline[];
}
