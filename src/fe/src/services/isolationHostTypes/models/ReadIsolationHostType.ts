import { IsolationHostType } from "@/models/IsolationHostType";

export interface ReadIsolationHostTypeQuery {
  Id: string;
}

export interface ReadIsolationHostTypeResponse {
  IsolationHostType: IsolationHostType;
}
