import { GSDType } from "./enums/GSDType";

export interface MaterialGSDInfoGridItem {
  Id: string;
  MaterialId: string;
  GSDType: GSDType;
  GSDTypeString: string;
  CellLine: string;
  PassageNumber: number;
  GSDFasta: string;
}
