import { TransportMode } from "@/models/TransportMode";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListTransportModeQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListTransportModeResponse {
  TransportModes: TransportMode[];
}
