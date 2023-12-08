import { GSDType } from "./enums/GSDType";

export interface MaterialGSDInfo {
  Id: string;
  MaterialId: string;
  GSDType: GSDType;
  CellLine: string;
  PassageNumber: number;
  GSDFasta: string;
}
