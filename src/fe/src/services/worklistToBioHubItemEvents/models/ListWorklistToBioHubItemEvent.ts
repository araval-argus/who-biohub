import { WorklistTimeline } from "@/models/WorklistTimeline";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListWorklistToBioHubItemEventQuery {
  WorklistToBioHubItemId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListWorklistToBioHubItemEventResponse {
  WorklistTimelines: WorklistTimeline[];
}
