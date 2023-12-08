import { WorklistTimelineEventsType } from "./WorklistTimelineEventsType";

export interface WorklistTimelineEventsDay {
  DateZeroBased: number;
  Date: Date;
  DateString: string;
  DateDD: string;
  DateMMM: string;
  DateMM: string;
  DateYYYY: string;
  Stage: string;
  WorklistTimelineEventsTypes: WorklistTimelineEventsType[];
}
