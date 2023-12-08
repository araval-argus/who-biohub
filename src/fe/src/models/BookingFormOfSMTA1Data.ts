import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import { BookingFormOfSMTA } from "./BookingFormOfSMTA";
import { Courier } from "./Courier";
import { WorklistItemUser } from "./WorklistItemUser";

export interface BookingFormOfSMTA1Data {
  ShipmentRequestNumber: string;
  OriginalDocumentTemplateBookingFormOfSMTA1DocumentId: string;
  SMTA1DocumentId: string;
  OriginalDocumentTemplateBookingFormOfSMTA1DocumentName: string;
  SMTA1DocumentName: string;
  BookingFormOfSMTA1SignatureText: string;
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
  MaterialShippingInformations: Array<MaterialShippingInformation>;
  BioHubFacilityFocalPoints: Array<WorklistItemUser>;
  BookingForms: Array<BookingFormOfSMTA>;
  ApprovalDate: Date;
  Couriers: Array<Courier>;
}
