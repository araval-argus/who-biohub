import { WorklistEventType } from "./enums/WorklistEventType";
import { WorklistEventDetailItem } from "./WorklistEventDetailItem";

export interface WorklistTimelineEventsType {
  Id: string;
  Label: string;
  Position: string;
  EventsNumber: string;
  EventType: WorklistEventType;
  Title: string;
  Date: string;
  EventDetailItems: WorklistEventDetailItem[];

  // DateZeroBased: number;
  // Date: Date;
  // DateString: string;
  // DateDD: string;
  // DateMMM: string;
  // DateYYYY: string;
}
