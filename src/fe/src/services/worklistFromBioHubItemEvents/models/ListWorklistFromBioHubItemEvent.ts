import { WorklistTimeline } from "@/models/WorklistTimeline";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListWorklistFromBioHubItemEventQuery {
  WorklistFromBioHubItemId: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListWorklistFromBioHubItemEventResponse {
  WorklistTimelines: WorklistTimeline[];
}
