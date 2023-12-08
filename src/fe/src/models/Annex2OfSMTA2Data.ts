import { WorklistFromBioHubItemAnnex2OfSMTA2Condition } from "./WorklistFromBioHubItemAnnex2OfSMTA2Condition";
import { WorklistFromBioHubItemMaterial } from "./WorklistFromBioHubItemMaterial";
import { WorklistItemUser } from "./WorklistItemUser";

export interface Annex2OfSMTA2Data {
  ShipmentRequestNumber: string;
  OriginalDocumentTemplateAnnex2OfSMTA1DocumentId: string;
  Annex2OfSMTA1DocumentId: string;
  OriginalDocumentTemplateAnnex2OfSMTA1DocumentName: string;
  Annex2OfSMTA1SignatureId: string;
  Annex2OfSMTA1SignatureText: string;
  Annex2Comment: string;
  Annex2TermsAndConditions: boolean;
  LaboratoryId: string;
  BioHubFacilityId: string;
  WHODocumentRegistrationNumber: string;
  LaboratoryName: string;
  LaboratoryAddress: string;
  LaboratoryCountry: string;
  BioHubFacilityName: string;
  BioHubFacilityAddress: string;
  BioHubFacilityCountry: string;
  WorklistFromBioHubItemMaterials: Array<WorklistFromBioHubItemMaterial>;
  WorklistFromBioHubItemAnnex2OfSMTA2Conditions: Array<WorklistFromBioHubItemAnnex2OfSMTA2Condition>;
  LaboratoryFocalPoints: Array<WorklistItemUser>;
  ApprovalDate: Date;
  Annex2ApprovalComment: string;
  SMTA2DocumentId: string;
  SMTA2DocumentName: string;
}
