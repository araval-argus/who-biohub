import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import { WorklistItemUser } from "./WorklistItemUser";

export interface Annex2OfSMTA1Data {
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
  MaterialShippingInformations: Array<MaterialShippingInformation>;
  LaboratoryFocalPoints: Array<WorklistItemUser>;
  ApprovalDate: Date;
  Annex2ApprovalComment: string;
  SMTA1DocumentId: string;
  SMTA1DocumentName: string;
}
