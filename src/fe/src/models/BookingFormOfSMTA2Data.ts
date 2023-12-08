import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import { BookingFormOfSMTA } from "./BookingFormOfSMTA";
import { Courier } from "./Courier";
import { WorklistFromBioHubItemMaterial } from "./WorklistFromBioHubItemMaterial";
import { WorklistItemUser } from "./WorklistItemUser";

export interface BookingFormOfSMTA2Data {
  ShipmentRequestNumber: string;
  OriginalDocumentTemplateBookingFormOfSMTA2DocumentId: string;
  SMTA2DocumentId: string;
  OriginalDocumentTemplateBookingFormOfSMTA2DocumentName: string;
  SMTA2DocumentName: string;
  BookingFormOfSMTA2SignatureText: string;
  RequestUserFirstName: string;
  RequestUserLastName: string;
  RequestUserEmail: string;
  RequestUserJobTitle: string;
  RequestUserBusinessPhone: string;
  RequestUserMobilePhone: string;
  LaboratoryName: string;
  LaboratoryAddress: string;
  LaboratoryCountry: string;
  BioHubFacilityName: string;
  Annex2TermsAndConditions: boolean;
  LaboratoryId: string;
  BioHubFacilityId: string;
  WHODocumentRegistrationNumber: string;
  BioHubFacilityAddress: string;
  BioHubFacilityCountry: string;
  WorklistFromBioHubItemMaterials: Array<WorklistFromBioHubItemMaterial>;
  BioHubFacilityFocalPoints: Array<WorklistItemUser>;
  BookingForms: Array<BookingFormOfSMTA>;
  ApprovalDate: Date;
  Couriers: Array<Courier>;
}
