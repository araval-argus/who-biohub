import { IsolationHostType } from "@/models/IsolationHostType";

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListIsolationHostTypeQuery {}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface ListIsolationHostTypeResponse {
  IsolationHostTypes: IsolationHostType[];
}
