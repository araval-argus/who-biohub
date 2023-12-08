import { TransportMode } from "@/models/TransportMode";

export interface ReadTransportModeQuery {
  Id: string;
}

export interface ReadTransportModeResponse {
  TransportMode: TransportMode;
}
