import { WorklistFromBioHubStatus } from "./enums/WorklistFromBioHubStatus";
import { DocumentFileType } from "./enums/DocumentFileType";
import { RoleType } from "@/models/enums/RoleType";
import { WorklistFillingOption } from "@/models/enums/WorklistFillingOption";
import { ShipmentDocument } from "@/models/ShipmentDocument";
import { WorklistFromBioHubItemFeedback } from "@/models/WorklistFromBioHubItemFeedback";
import { WorklistFromBioHubItemMaterial } from "./WorklistFromBioHubItemMaterial";
import { WorklistFromBioHubItemBiosafetyChecklistOfSMTA2 } from "./WorklistFromBioHubItemBiosafetyChecklistOfSMTA2";
import { WorklistFromBioHubItemAnnex2OfSMTA2Condition } from "./WorklistFromBioHubItemAnnex2OfSMTA2Condition";
import { WorklistFromBioHubItemBiosafetyChecklistThreadComment } from "./WorklistFromBioHubItemBiosafetyChecklistThreadComment";
import { SMTAApprovalStatus } from "./enums/SMTAApprovalStatus";
import { WorklistItemUser } from "./WorklistItemUser";
import { BookingFormOfSMTA } from "./BookingFormOfSMTA";

export enum ShipmentDocumentOperationType {
  Add = 0,
  Update = 1,
  Delete = 2,
}

export interface WorklistFromBioHubItem {
  Id: string;
  CurrentStatus: WorklistFromBioHubStatus;
  CurrentStatusName: string;
  PreviousStatus: WorklistFromBioHubStatus;
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
  SMTA2DocumentId: string;
  SMTA2DocumentName: string;
  LaboratoryId: string;
  BioHubFacilityId: string;
  OriginalDocumentTemplateSMTA2DocumentId: string;
  DocumentTemplateFileType: DocumentFileType;
  HistoryDto: boolean;
  UserRoleName: string;
  UserRoleTypeName: string;
  UserRoleType: RoleType;
  OriginalDocumentTemplateAnnex2OfSMTA2DocumentId: string;
  Annex2OfSMTA2DocumentId: string;
  Annex2OfSMTA2DocumentName: string;
  Annex2OfSMTA2SignatureId: string;
  Annex2OfSMTA2SignatureString: string;
  Annex2Comment: string;
  Annex2TermsAndConditions: boolean;
  Annex2FillingOption: WorklistFillingOption;
  WorklistFromBioHubItemMaterials: Array<WorklistFromBioHubItemMaterial>;
  LaboratoryFocalPoints: Array<WorklistItemUser>;
  WorklistFromBioHubItemAnnex2OfSMTA2Conditions: Array<WorklistFromBioHubItemAnnex2OfSMTA2Condition>;
  WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s: Array<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2>;
  Annex2ApprovalFlag: boolean;
  Annex2ApprovalComment: string;
  IsSaveDraft: boolean;
  BookingForms: Array<BookingFormOfSMTA>;
  BookingFormFillingOption: WorklistFillingOption;
  OriginalDocumentTemplateBookingFormOfSMTA2DocumentId: string;
  BookingFormOfSMTA2DocumentId: string;
  BookingFormOfSMTA2DocumentName: string;
  BookingFormOfSMTA2SignatureId: string;
  BookingFormOfSMTA2SignatureString: string;
  BookingFormApprovalFlag: boolean;
  BookingFormApprovalComment: string;
  BiosafetyChecklistFillingOption: WorklistFillingOption;
  OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentId: string;
  BiosafetyChecklistOfSMTA2DocumentId: string;
  BiosafetyChecklistOfSMTA2DocumentName: string;
  BiosafetyChecklistOfSMTA2SignatureId: string;
  BiosafetyChecklistOfSMTA2SignatureString: string;
  BiosafetyChecklistOfSMTA2ApprovalFlag: boolean;
  BiosafetyChecklistOfSMTA2ApprovalComment: string;
  LastOperationUserFirstName: string;
  LastOperationUserLastName: string;
  LastOperationUserBusinessPhone: string;
  LastOperationUserMobilePhone: string;
  LastOperationUserEmail: string;
  LastOperationUserJobTitle: string;
  ShipmentDocumentOperationType: ShipmentDocumentOperationType;
  ShipmentDocumentId: string;
  ShipmentDocumentNewName: string;
  BHFShipmentDocuments: Array<ShipmentDocument>;
  QEShipmentDocuments: Array<ShipmentDocument>;
  PackagingListDocumentTemplateId: string;
  PackagingListDocumentTemplateName: string;
  NonCommercialInvoiceCatADocumentTemplateId: string;
  NonCommercialInvoiceCatADocumentTemplateName: string;
  NonCommercialInvoiceCatBDocumentTemplateId: string;
  NonCommercialInvoiceCatBDocumentTemplateName: string;
  WaitForArrivalConditionCheckApprovalComment: string;
  WaitForArrivalConditionCheckApprovalFlag: boolean;
  NewFeedback: string;
  Feedbacks: Array<WorklistFromBioHubItemFeedback>;
  WHODocumentRegistrationNumber: string;

  NewBiosafetyChecklistThreadComment: string;
  BiosafetyChecklistThreadComments: Array<WorklistFromBioHubItemBiosafetyChecklistThreadComment>;

  PreviousOperationDate: Date;
  PreviousUserId: string;

  PreviousActionBy: string;
  NextActionBy: string;
  Institution: string;
  LaboratoryCountryName: string;

  CurrentDownloadSMTA2DocumentId: string;
  SMTA2ApprovalStatus: SMTAApprovalStatus;
  SMTA2ApprovalDate: Date | null;
  CurrentDownloadSMTA2DocumentName: string;

  IsPast: boolean;
  AssignedOperationDate: Date | null;

  //# 54317
  Annex2OfSMTA2SignatureText: string;
  BookingFormOfSMTA2SignatureText: string;
  BiosafetyChecklistOfSMTA2SignatureText: string;
  ///////
}
