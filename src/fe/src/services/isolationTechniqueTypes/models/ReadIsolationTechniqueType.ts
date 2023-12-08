import { IsolationTechniqueType } from "@/models/IsolationTechniqueType";

export interface ReadIsolationTechniqueTypeQuery {
  Id: string;
}

export interface ReadIsolationTechniqueTypeResponse {
  IsolationTechniqueType: IsolationTechniqueType;
}
