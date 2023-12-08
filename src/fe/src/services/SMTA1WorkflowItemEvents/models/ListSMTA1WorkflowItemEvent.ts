import { WorklistTimeline } from "@/models/WorklistTimeline";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSMTA1WorkflowItemEventQuery {
  SMTA1WorkflowItemId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSMTA1WorkflowItemEventResponse {
  WorklistTimelines: WorklistTimeline[];
}
