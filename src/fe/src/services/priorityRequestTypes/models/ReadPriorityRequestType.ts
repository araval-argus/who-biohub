import { PriorityRequestType } from "@/models/PriorityRequestType";

export interface ReadPriorityRequestTypeQuery {
  Id: string;
}

export interface ReadPriorityRequestTypeResponse {
  PriorityRequestType: PriorityRequestType;
}
