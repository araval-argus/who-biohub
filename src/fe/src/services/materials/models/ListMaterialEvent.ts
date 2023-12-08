import { WorklistTimeline } from "@/models/WorklistTimeline";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListMaterialEventQuery {
  Id: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListMaterialEventResponse {
  MaterialTimeline: WorklistTimeline;
}
