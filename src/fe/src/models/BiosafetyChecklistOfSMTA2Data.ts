import { WorklistFromBioHubItemBiosafetyChecklistOfSMTA2 } from "./WorklistFromBioHubItemBiosafetyChecklistOfSMTA2";
import { WorklistFromBioHubItemBiosafetyChecklistThreadComment } from "./WorklistFromBioHubItemBiosafetyChecklistThreadComment";

export interface BiosafetyChecklistOfSMTA2Data {
  ShipmentRequestNumber: string;
  OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentId: string;
  BiosafetyChecklistOfSMTA2DocumentId: string;
  OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentName: string;
  BiosafetyChecklistOfSMTA2SignatureId: string;
  BiosafetyChecklistOfSMTA2SignatureText: string;
  BiosafetyChecklistComment: string;
  LaboratoryId: string;
  BioHubFacilityId: string;
  LaboratoryName: string;
  LaboratoryAddress: string;
  LaboratoryCountry: string;
  BioHubFacilityName: string;
  BioHubFacilityAddress: string;
  BioHubFacilityCountry: string;
  WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s: Array<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2>;
  BiosafetyChecklistThreadComments: Array<WorklistFromBioHubItemBiosafetyChecklistThreadComment>;
  ApprovalDate: Date;
  BiosafetyChecklistApprovalComment: string;
  SMTA2DocumentId: string;
  SMTA2DocumentName: string;
}
