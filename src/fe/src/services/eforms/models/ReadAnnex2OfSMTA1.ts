import { Annex2OfSMTA1Data } from "@/models/Annex2OfSMTA1Data";

export interface ReadAnnex2OfSMTA1Query {
  Id: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ReadAnnex2OfSMTA1Response {
  Annex2OfSMTA1Data: Annex2OfSMTA1Data;
}
