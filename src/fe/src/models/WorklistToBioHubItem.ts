import { WorklistToBioHubStatus } from "./enums/WorklistToBioHubStatus";
import { DocumentFileType } from "./enums/DocumentFileType";
import { RoleType } from "@/models/enums/RoleType";
import { WorklistFillingOption } from "@/models/enums/WorklistFillingOption";
import { MaterialShippingInformation } from "@/models/MaterialShippingInformation";
import { ShipmentDocument } from "@/models/ShipmentDocument";
import { WorklistToBioHubItemFeedback } from "@/models/WorklistToBioHubItemFeedback";
import { WorklistToBioHubItemMaterial } from "./WorklistToBioHubItemMaterial";
import { SMTAApprovalStatus } from "./enums/SMTAApprovalStatus";
import { BookingFormOfSMTA } from "./BookingFormOfSMTA";
import { WorklistItemUser } from "./WorklistItemUser";

export enum ShipmentDocumentOperationType {
  Add = 0,
  Update = 1,
  Delete = 2,
}

export interface WorklistToBioHubItem {
  Id: string;
  CurrentStatus: WorklistToBioHubStatus;
  CurrentStatusName: string;
  PreviousStatus: WorklistToBioHubStatus;
  WorklistItemTitle: string;
  LastSubmissionApproved: boolean;
  LaboratoryName: string;
  LaboratoryAddress: string;
  LaboratoryAbbreviation: string;
  LaboratoryCountry: string;
  BioHubFacilityName: string;
  BioHubFacilityAddress: string;
  BioHubFacilityCountry: string;
  OperationDate: Date;
  UserName: string;
  Comment: string;
  SMTA1DocumentId: string;
  SMTA1DocumentName: string;
  LaboratoryId: string;
  BioHubFacilityId: string;
  OriginalDocumentTemplateSMTA1DocumentId: string;
  DocumentTemplateFileType: DocumentFileType;
  HistoryDto: boolean;
  UserRoleName: string;
  UserRoleTypeName: string;
  UserRoleType: RoleType;
  OriginalDocumentTemplateAnnex2OfSMTA1DocumentId: string;
  Annex2OfSMTA1DocumentId: string;
  Annex2OfSMTA1DocumentName: string;
  Annex2OfSMTA1SignatureId: string;
  Annex2OfSMTA1SignatureString: string;
  Annex2Comment: string;
  Annex2TermsAndConditions: boolean;
  Annex2FillingOption: WorklistFillingOption;
  MaterialShippingInformations: Array<MaterialShippingInformation>;
  LaboratoryFocalPoints: Array<WorklistItemUser>;
  Annex2ApprovalFlag: boolean;
  Annex2ApprovalComment: string;
  IsSaveDraft: boolean;
  BookingForms: Array<BookingFormOfSMTA>;
  BookingFormFillingOption: WorklistFillingOption;
  OriginalDocumentTemplateBookingFormOfSMTA1DocumentId: string;
  BookingFormOfSMTA1DocumentId: string;
  BookingFormOfSMTA1DocumentName: string;
  BookingFormOfSMTA1SignatureId: string;
  BookingFormOfSMTA1SignatureString: string;
  BookingFormApprovalFlag: boolean;
  BookingFormApprovalComment: string;
  LastOperationUserFirstName: string;
  LastOperationUserLastName: string;
  LastOperationUserBusinessPhone: string;
  LastOperationUserMobilePhone: string;
  LastOperationUserEmail: string;
  LastOperationUserJobTitle: string;
  ShipmentDocumentOperationType: ShipmentDocumentOperationType;
  ShipmentDocumentId: string;
  ShipmentDocumentNewName: string;
  ShipmentDocuments: Array<ShipmentDocument>;
  PackagingListDocumentTemplateId: string;
  PackagingListDocumentTemplateName: string;
  NonCommercialInvoiceCatADocumentTemplateId: string;
  NonCommercialInvoiceCatADocumentTemplateName: string;
  NonCommercialInvoiceCatBDocumentTemplateId: string;
  NonCommercialInvoiceCatBDocumentTemplateName: string;
  WaitForArrivalConditionCheckApprovalComment: string;
  WaitForArrivalConditionCheckApprovalFlag: boolean;
  NewFeedback: string;
  Feedbacks: Array<WorklistToBioHubItemFeedback>;
  WHODocumentRegistrationNumber: string;
  WorklistToBioHubItemMaterials: Array<WorklistToBioHubItemMaterial>;
  WorklistToBioHubItemBioHubFacilityFocalPoints: Array<WorklistItemUser>;

  PreviousActionBy: string;
  NextActionBy: string;
  Institution: string;
  LaboratoryCountryName: string;

  CurrentDownloadSMTA1DocumentId: string;
  SMTA1ApprovalStatus: SMTAApprovalStatus;
  SMTA1ApprovalDate: Date | null;
  CurrentDownloadSMTA1DocumentName: string;

  IsPast: boolean;
  AssignedOperationDate: Date | null;

  //# 54317
  Annex2OfSMTA1SignatureText: string;
  BookingFormOfSMTA1SignatureText: string;
  ///////
}
