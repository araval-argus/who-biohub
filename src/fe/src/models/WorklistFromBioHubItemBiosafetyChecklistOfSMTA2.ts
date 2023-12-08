export interface WorklistFromBioHubItemBiosafetyChecklistOfSMTA2 {
  Id: string;
  WorklistFromBioHubItemId: string;
  BiosafetyChecklistId: string;
  Order: number;
  Condition: string;
  Mandatory: boolean;
  Selectable: boolean;
  Flag: boolean | null;
  ParentConditionId: string;
  ShowOnParentValue: boolean;
  IsParentCondition: boolean;
  IsVisible: boolean;
}
