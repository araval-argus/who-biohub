import { SMTARequest } from "@/models/SMTARequest";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSMTARequestQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListSMTARequestResponse {
  SMTARequests: SMTARequest[];
}
