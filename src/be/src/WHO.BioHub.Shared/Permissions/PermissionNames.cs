using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHO.BioHub.Shared.Permissions
{
    public static class PermissionNames
    {
        public const string CanReadBioHubFacility = nameof(CanReadBioHubFacility);
        public const string CanReadBSLLevel = nameof(CanReadBSLLevel);
        public const string CanReadCountry = nameof(CanReadCountry);
        public const string CanReadCultivabilityType = nameof(CanReadCultivabilityType);
        public const string CanReadGeneticSequenceData = nameof(CanReadGeneticSequenceData);
        public const string CanReadInternationalTaxonomyClassification = nameof(CanReadInternationalTaxonomyClassification);
        public const string CanReadIsolationHostType = nameof(CanReadIsolationHostType);
        public const string CanReadIsolationTechniqueType = nameof(CanReadIsolationTechniqueType);
        public const string CanReadLaboratory = nameof(CanReadLaboratory);
        public const string CanReadMaterialProduct = nameof(CanReadMaterialProduct);
        public const string CanReadMaterial = nameof(CanReadMaterial);
        public const string CanReadMaterialType = nameof(CanReadMaterialType);
        public const string CanReadMaterialUsage = nameof(CanReadMaterialUsage);
        public const string CanReadPriorityRequestType = nameof(CanReadPriorityRequestType);
        public const string CanReadRole = nameof(CanReadRole);
        public const string CanReadShipment = nameof(CanReadShipment);
        public const string CanReadTemperatureUnitOfMeasure = nameof(CanReadTemperatureUnitOfMeasure);
        public const string CanReadTransportCategory = nameof(CanReadTransportCategory);
        public const string CanReadTransportMode = nameof(CanReadTransportMode);
        public const string CanReadUserRequest = nameof(CanReadUserRequest);
        public const string CanReadUserRequestStatus = nameof(CanReadUserRequestStatus);
        public const string CanReadUser = nameof(CanReadUser);

        public const string CanEditBioHubFacility = nameof(CanEditBioHubFacility);
        public const string CanEditBSLLevel = nameof(CanEditBSLLevel);
        public const string CanEditCountry = nameof(CanEditCountry);
        public const string CanEditCultivabilityType = nameof(CanEditCultivabilityType);
        public const string CanEditGeneticSequenceData = nameof(CanEditGeneticSequenceData);
        public const string CanEditInternationalTaxonomyClassification = nameof(CanEditInternationalTaxonomyClassification);
        public const string CanEditIsolationHostType = nameof(CanEditIsolationHostType);
        public const string CanEditIsolationTechniqueType = nameof(CanEditIsolationTechniqueType);
        public const string CanEditLaboratory = nameof(CanEditLaboratory);
        public const string CanEditMaterialProduct = nameof(CanEditMaterialProduct);
        public const string CanEditMaterial = nameof(CanEditMaterial);
        public const string CanEditMaterialType = nameof(CanEditMaterialType);
        public const string CanEditMaterialUsage = nameof(CanEditMaterialUsage);
        public const string CanEditPriorityRequestType = nameof(CanEditPriorityRequestType);
        public const string CanEditRole = nameof(CanEditRole);
        public const string CanEditShipment = nameof(CanEditShipment);
        public const string CanEditTemperatureUnitOfMeasure = nameof(CanEditTemperatureUnitOfMeasure);
        public const string CanEditTransportCategory = nameof(CanEditTransportCategory);
        public const string CanEditTransportMode = nameof(CanEditTransportMode);
        public const string CanEditUserRequest = nameof(CanEditUserRequest);
        public const string CanEditUserRequestStatus = nameof(CanEditUserRequestStatus);
        public const string CanEditUser = nameof(CanEditUser);

        public const string CanDeleteBioHubFacility = nameof(CanDeleteBioHubFacility);
        public const string CanDeleteBSLLevel = nameof(CanDeleteBSLLevel);
        public const string CanDeleteCountry = nameof(CanDeleteCountry);
        public const string CanDeleteCultivabilityType = nameof(CanDeleteCultivabilityType);
        public const string CanDeleteGeneticSequenceData = nameof(CanDeleteGeneticSequenceData);
        public const string CanDeleteInternationalTaxonomyClassification = nameof(CanDeleteInternationalTaxonomyClassification);
        public const string CanDeleteIsolationHostType = nameof(CanDeleteIsolationHostType);
        public const string CanDeleteIsolationTechniqueType = nameof(CanDeleteIsolationTechniqueType);
        public const string CanDeleteLaboratory = nameof(CanDeleteLaboratory);
        public const string CanDeleteMaterialProduct = nameof(CanDeleteMaterialProduct);
        public const string CanDeleteMaterial = nameof(CanDeleteMaterial);
        public const string CanDeleteMaterialType = nameof(CanDeleteMaterialType);
        public const string CanDeleteMaterialUsage = nameof(CanDeleteMaterialUsage);
        public const string CanDeletePriorityRequestType = nameof(CanDeletePriorityRequestType);
        public const string CanDeleteRole = nameof(CanDeleteRole);
        public const string CanDeleteShipment = nameof(CanDeleteShipment);
        public const string CanDeleteTemperatureUnitOfMeasure = nameof(CanDeleteTemperatureUnitOfMeasure);
        public const string CanDeleteTransportCategory = nameof(CanDeleteTransportCategory);
        public const string CanDeleteTransportMode = nameof(CanDeleteTransportMode);
        public const string CanDeleteUserRequest = nameof(CanDeleteUserRequest);
        public const string CanDeleteUserRequestStatus = nameof(CanDeleteUserRequestStatus);
        public const string CanDeleteUser = nameof(CanDeleteUser);

        public const string CanCreateBioHubFacility = nameof(CanCreateBioHubFacility);
        public const string CanCreateBSLLevel = nameof(CanCreateBSLLevel);
        public const string CanCreateCountry = nameof(CanCreateCountry);
        public const string CanCreateCultivabilityType = nameof(CanCreateCultivabilityType);
        public const string CanCreateGeneticSequenceData = nameof(CanCreateGeneticSequenceData);
        public const string CanCreateInternationalTaxonomyClassification = nameof(CanCreateInternationalTaxonomyClassification);
        public const string CanCreateIsolationHostType = nameof(CanCreateIsolationHostType);
        public const string CanCreateIsolationTechniqueType = nameof(CanCreateIsolationTechniqueType);
        public const string CanCreateLaboratory = nameof(CanCreateLaboratory);
        public const string CanCreateMaterialProduct = nameof(CanCreateMaterialProduct);
        public const string CanCreateMaterial = nameof(CanCreateMaterial);
        public const string CanCreateMaterialType = nameof(CanCreateMaterialType);
        public const string CanCreateMaterialUsage = nameof(CanCreateMaterialUsage);
        public const string CanCreatePriorityRequestType = nameof(CanCreatePriorityRequestType);
        public const string CanCreateRole = nameof(CanCreateRole);
        public const string CanCreateShipment = nameof(CanCreateShipment);
        public const string CanCreateTemperatureUnitOfMeasure = nameof(CanCreateTemperatureUnitOfMeasure);
        public const string CanCreateTransportCategory = nameof(CanCreateTransportCategory);
        public const string CanCreateTransportMode = nameof(CanCreateTransportMode);
        public const string CanCreateUserRequest = nameof(CanCreateUserRequest);
        public const string CanCreateUserRequestStatus = nameof(CanCreateUserRequestStatus);
        public const string CanCreateUser = nameof(CanCreateUser);

        public const string CanAccessWHODashboard = nameof(CanAccessWHODashboard);
        public const string CanAccessWHOBMEPP = nameof(CanAccessWHOBMEPP);
        public const string CanAccessWHOBioHubFacilities = nameof(CanAccessWHOBioHubFacilities);
        public const string CanAccessWHOLaboratories = nameof(CanAccessWHOLaboratories);
        public const string CanAccessWHOTemplates = nameof(CanAccessWHOTemplates);
        public const string CanAccessWHOShipments = nameof(CanAccessWHOShipments);
        public const string CanAccessWHOPendingRequest = nameof(CanAccessWHOPendingRequest);
        public const string CanAccessWHOUsers = nameof(CanAccessWHOUsers);

        public const string CanAccessLaboratoryDashboard = nameof(CanAccessLaboratoryDashboard);
        public const string CanAccessLaboratoryUserProfile = nameof(CanAccessLaboratoryUserProfile);
        public const string CanAccessLaboratoryStaff = nameof(CanAccessLaboratoryStaff);
        public const string CanAccessLaboratoryFacilityInstituteProfile = nameof(CanAccessLaboratoryFacilityInstituteProfile);
        public const string CanAccessLaboratoryBMEPP = nameof(CanAccessLaboratoryBMEPP);
        public const string CanAccessLaboratoryBMEPPCatalogue = nameof(CanAccessLaboratoryBMEPPCatalogue);
        public const string CanAccessLaboratoryTemplates = nameof(CanAccessLaboratoryTemplates);

        public const string CanAccessBioHubFacilityDashboard = nameof(CanAccessBioHubFacilityDashboard);
        public const string CanAccessBioHubFacilityUserProfile = nameof(CanAccessBioHubFacilityUserProfile);
        public const string CanAccessBioHubFacilityFacilityLaboratories = nameof(CanAccessBioHubFacilityFacilityLaboratories);
        public const string CanAccessBioHubFacilityBMEPP = nameof(CanAccessBioHubFacilityBMEPP);
        public const string CanAccessBioHubFacilityTemplates = nameof(CanAccessBioHubFacilityTemplates);

        public const string CanReadLaboratoryStaff = nameof(CanReadLaboratoryStaff);
        public const string CanEditLaboratoryStaff = nameof(CanEditLaboratoryStaff);
        public const string CanDeleteLaboratoryStaff = nameof(CanDeleteLaboratoryStaff);

        public const string CanApproveOrRejectUserRequest = nameof(CanApproveOrRejectUserRequest);

        public const string CanReadDocumentTemplate = nameof(CanReadDocumentTemplate);
        public const string CanCreateDocumentTemplate = nameof(CanCreateDocumentTemplate);
        public const string CanEditDocumentTemplate = nameof(CanEditDocumentTemplate);
        public const string CanDeleteDocumentTemplate = nameof(CanDeleteDocumentTemplate);


        public const string CanAccessRequestIniziation = nameof(CanAccessRequestIniziation);

        public const string CanAccessWorklist = nameof(CanAccessWorklist);

        public const string CanReadSubmitSMTA1 = nameof(CanReadSubmitSMTA1);
        public const string CanDownloadFileSubmitSMTA1 = nameof(CanDownloadFileSubmitSMTA1);
        public const string CanSubmitSMTA1 = nameof(CanSubmitSMTA1);

        public const string CanReadWaitingForSMTA1BHFsApproval = nameof(CanReadWaitingForSMTA1BHFsApproval);
        public const string CanSubmitWaitingForSMTA1BHFsApproval = nameof(CanSubmitWaitingForSMTA1BHFsApproval);
        public const string CanDownloadFileWaitingForSMTA1BHFsApproval = nameof(CanDownloadFileWaitingForSMTA1BHFsApproval);

        public const string CanReadWaitingForSMTA1SECsApproval = nameof(CanReadWaitingForSMTA1SECsApproval);
        public const string CanSubmitWaitingForSMTA1SECsApproval = nameof(CanSubmitWaitingForSMTA1SECsApproval);
        public const string CanDownloadFileWaitingForSMTA1SECsApproval = nameof(CanDownloadFileWaitingForSMTA1SECsApproval);

        public const string CanReadSubmitAnnex2OfSMTA1 = nameof(CanReadSubmitAnnex2OfSMTA1);
        public const string CanSubmitAnnex2OfSMTA1 = nameof(CanSubmitAnnex2OfSMTA1);
        public const string CanDownloadFileSubmitAnnex2OfSMTA1 = nameof(CanDownloadFileSubmitAnnex2OfSMTA1);

        public const string CanReceiveEmailsOnRequestInitiation = nameof(CanReceiveEmailsOnRequestInitiation);
        public const string CanReceiveEmailsOnSubmitSMTA1 = nameof(CanReceiveEmailsOnSubmitSMTA1);
        public const string CanReceiveEmailsOnSubmitWaitingForSMTA1BHFsApprovalApproval = nameof(CanReceiveEmailsOnSubmitWaitingForSMTA1BHFsApprovalApproval);
        public const string CanReceiveEmailsOnSubmitWaitingForSMTA1BHFsApprovalReject = nameof(CanReceiveEmailsOnSubmitWaitingForSMTA1BHFsApprovalReject);
        public const string CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalApproval = nameof(CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalApproval);
        public const string CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalReject = nameof(CanReceiveEmailsOnSubmitWaitingForSMTA1SECsApprovalReject);

        public const string CanReceiveEmailsOnSubmitAnnex2OfSMTA1Approval = nameof(CanReceiveEmailsOnSubmitAnnex2OfSMTA1Approval);
        public const string CanReceiveEmailsOnSubmitAnnex2OfSMTA1Reject = nameof(CanReceiveEmailsOnSubmitAnnex2OfSMTA1Reject);



        public const string CanReadWaitingForAnnex2OfSMTA1SECsApproval = nameof(CanReadWaitingForAnnex2OfSMTA1SECsApproval);
        public const string CanSubmitWaitingForAnnex2OfSMTA1SECsApproval = nameof(CanSubmitWaitingForAnnex2OfSMTA1SECsApproval);
        public const string CanDownloadFileWaitingForAnnex2OfSMTA1SECsApproval = nameof(CanDownloadFileWaitingForAnnex2OfSMTA1SECsApproval);
        public const string CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalApproval = nameof(CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalApproval);
        public const string CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalReject = nameof(CanReceiveEmailsOnWaitingForAnnex2OfSMTA1SECsApprovalReject);



        public const string CanReadSubmitBookingFormOfSMTA1 = nameof(CanReadSubmitBookingFormOfSMTA1);
        public const string CanSubmitBookingFormOfSMTA1 = nameof(CanSubmitBookingFormOfSMTA1);
        public const string CanDownloadFileSubmitBookingFormOfSMTA1 = nameof(CanDownloadFileSubmitBookingFormOfSMTA1);

        public const string CanReceiveEmailsOnSubmitBookingFormOfSMTA1Approval = nameof(CanReceiveEmailsOnSubmitBookingFormOfSMTA1Approval);
        public const string CanReceiveEmailsOnSubmitBookingFormOfSMTA1Reject = nameof(CanReceiveEmailsOnSubmitBookingFormOfSMTA1Reject);

        public const string CanReadWaitForBookingFormSMTA1OPSApproval = nameof(CanReadWaitForBookingFormSMTA1OPSApproval);
        public const string CanReadSMTA1ShipmentDocuments = nameof(CanReadSMTA1ShipmentDocuments);
        public const string CanSubmitWaitForBookingFormSMTA1OPSApproval = nameof(CanSubmitWaitForBookingFormSMTA1OPSApproval);
        public const string CanSubmitSMTA1ShipmentDocuments = nameof(CanSubmitSMTA1ShipmentDocuments);
        public const string CanDownloadFileWaitForBookingFormSMTA1OPSApproval = nameof(CanDownloadFileWaitForBookingFormSMTA1OPSApproval);
        public const string CanDownloadSMTA1ShipmentDocuments = nameof(CanDownloadSMTA1ShipmentDocuments);

        public const string CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalApproval = nameof(CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalApproval);
        public const string CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalReject = nameof(CanReceiveEmailsOnWaitForBookingFormSMTA1OPSApprovalReject);
        public const string CanReceiveEmailsOnSMTA1ShipmentDocumentsApproval = nameof(CanReceiveEmailsOnSMTA1ShipmentDocumentsApproval);
        public const string CanReceiveEmailsOnSMTA1ShipmentDocumentsReject = nameof(CanReceiveEmailsOnSMTA1ShipmentDocumentsReject);


        public const string CanCreateCourier = nameof(CanCreateCourier);
        public const string CanReadCourier = nameof(CanReadCourier);
        public const string CanEditCourier = nameof(CanEditCourier);
        public const string CanDeleteCourier = nameof(CanDeleteCourier);

        public const string CanCreateCourierStaff = nameof(CanCreateCourierStaff);
        public const string CanReadCourierStaff = nameof(CanReadCourierStaff);
        public const string CanEditCourierStaff = nameof(CanEditCourierStaff);
        public const string CanDeleteCourierStaff = nameof(CanDeleteCourierStaff);


        public const string CanReadWaitForPickUpCompleted = nameof(CanReadWaitForPickUpCompleted);
        public const string CanReadWaitForDeliveryCompleted = nameof(CanReadWaitForDeliveryCompleted);
        public const string CanReadWaitForArrivalConditionCheck = nameof(CanReadWaitForArrivalConditionCheck);
        public const string CanReadWaitForCommentBHFSendFeedback = nameof(CanReadWaitForCommentBHFSendFeedback);
        public const string CanReadWaitForFinalApproval = nameof(CanReadWaitForFinalApproval);
        public const string CanReadShipmentCompleted = nameof(CanReadShipmentCompleted);

        public const string CanSubmitWaitForPickUpCompleted = nameof(CanSubmitWaitForPickUpCompleted);
        public const string CanSubmitWaitForDeliveryCompleted = nameof(CanSubmitWaitForDeliveryCompleted);
        public const string CanSubmitWaitForArrivalConditionCheck = nameof(CanSubmitWaitForArrivalConditionCheck);
        public const string CanSubmitWaitForCommentBHFSendFeedback = nameof(CanSubmitWaitForCommentBHFSendFeedback);
        public const string CanSubmitWaitForFinalApproval = nameof(CanSubmitWaitForFinalApproval);
        public const string CanSubmitShipmentCompleted = nameof(CanSubmitShipmentCompleted);

        public const string CanDownloadFileWaitForPickUpCompleted = nameof(CanDownloadFileWaitForPickUpCompleted);
        public const string CanDownloadFileWaitForDeliveryCompleted = nameof(CanDownloadFileWaitForDeliveryCompleted);
        public const string CanDownloadFileWaitForArrivalConditionCheck = nameof(CanDownloadFileWaitForArrivalConditionCheck);
        public const string CanDownloadFileWaitForCommentBHFSendFeedback = nameof(CanDownloadFileWaitForCommentBHFSendFeedback);
        public const string CanDownloadFileWaitForFinalApproval = nameof(CanDownloadFileWaitForFinalApproval);
        public const string CanDownloadFileShipmentCompleted = nameof(CanDownloadFileShipmentCompleted);


        public const string CanReceiveEmailsOnWaitForPickUpCompletedApproval = nameof(CanReceiveEmailsOnWaitForPickUpCompletedApproval);
        public const string CanReceiveEmailsOnWaitForDeliveryCompletedApproval = nameof(CanReceiveEmailsOnWaitForDeliveryCompletedApproval);
        public const string CanReceiveEmailsOnWaitForArrivalConditionCheckApproval = nameof(CanReceiveEmailsOnWaitForArrivalConditionCheckApproval);
        public const string CanReceiveEmailsOnWaitForCommentBHFSendFeedbackApproval = nameof(CanReceiveEmailsOnWaitForCommentBHFSendFeedbackApproval);
        public const string CanReceiveEmailsOnWaitForFinalApprovalApproval = nameof(CanReceiveEmailsOnWaitForFinalApprovalApproval);
        public const string CanReceiveEmailsOnShipmentCompletedApproval = nameof(CanReceiveEmailsOnShipmentCompletedApproval);

        public const string CanReceiveEmailsOnWaitForPickUpCompletedReject = nameof(CanReceiveEmailsOnWaitForPickUpCompletedReject);
        public const string CanReceiveEmailsOnWaitForDeliveryCompletedReject = nameof(CanReceiveEmailsOnWaitForDeliveryCompletedReject);
        public const string CanReceiveEmailsOnWaitForArrivalConditionCheckReject = nameof(CanReceiveEmailsOnWaitForArrivalConditionCheckReject);
        public const string CanReceiveEmailsOnWaitForCommentBHFSendFeedbackReject = nameof(CanReceiveEmailsOnWaitForCommentBHFSendFeedbackReject);
        public const string CanReceiveEmailsOnWaitForFinalApprovalReject = nameof(CanReceiveEmailsOnWaitForFinalApprovalReject);
        public const string CanReceiveEmailsOnShipmentCompletedReject = nameof(CanReceiveEmailsOnShipmentCompletedReject);



        public const string CanReadSubmitSMTA2 = nameof(CanReadSubmitSMTA2);
        public const string CanSubmitSMTA2 = nameof(CanSubmitSMTA2);
        public const string CanDownloadFileSubmitSMTA2 = nameof(CanDownloadFileSubmitSMTA2);
        public const string CanReadWaitingForSMTA2SECsApproval = nameof(CanReadWaitingForSMTA2SECsApproval);
        public const string CanSubmitWaitingForSMTA2SECsApproval = nameof(CanSubmitWaitingForSMTA2SECsApproval);
        public const string CanDownloadFileWaitingForSMTA2SECsApproval = nameof(CanDownloadFileWaitingForSMTA2SECsApproval);
        public const string CanReadSubmitAnnex2OfSMTA2 = nameof(CanReadSubmitAnnex2OfSMTA2);
        public const string CanSubmitAnnex2OfSMTA2 = nameof(CanSubmitAnnex2OfSMTA2);
        public const string CanDownloadFileSubmitAnnex2OfSMTA2 = nameof(CanDownloadFileSubmitAnnex2OfSMTA2);
        public const string CanReadWaitingForAnnex2OfSMTA2SECsApproval = nameof(CanReadWaitingForAnnex2OfSMTA2SECsApproval);
        public const string CanSubmitWaitingForAnnex2OfSMTA2SECsApproval = nameof(CanSubmitWaitingForAnnex2OfSMTA2SECsApproval);
        public const string CanDownloadFileWaitingForAnnex2OfSMTA2SECsApproval = nameof(CanDownloadFileWaitingForAnnex2OfSMTA2SECsApproval);
        public const string CanReadSubmitBiosafetyChecklistFormOfSMTA2 = nameof(CanReadSubmitBiosafetyChecklistFormOfSMTA2);
        public const string CanSubmitBiosafetyChecklistFormOfSMTA2 = nameof(CanSubmitBiosafetyChecklistFormOfSMTA2);
        public const string CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2 = nameof(CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2);
        public const string CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApproval = nameof(CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApproval);
        public const string CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApproval = nameof(CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApproval);
        public const string CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApproval = nameof(CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApproval);
        public const string CanReadSubmitBookingFormOfSMTA2 = nameof(CanReadSubmitBookingFormOfSMTA2);
        public const string CanSubmitBookingFormOfSMTA2 = nameof(CanSubmitBookingFormOfSMTA2);
        public const string CanDownloadFileSubmitBookingFormOfSMTA2 = nameof(CanDownloadFileSubmitBookingFormOfSMTA2);
        public const string CanReadWaitForBookingFormSMTA2OPSsApproval = nameof(CanReadWaitForBookingFormSMTA2OPSsApproval);
        public const string CanSubmitWaitForBookingFormSMTA2OPSsApproval = nameof(CanSubmitWaitForBookingFormSMTA2OPSsApproval);
        public const string CanDownloadFileWaitForBookingFormSMTA2OPSsApproval = nameof(CanDownloadFileWaitForBookingFormSMTA2OPSsApproval);
        public const string CanReadBHFSMTA2ShipmentDocuments = nameof(CanReadBHFSMTA2ShipmentDocuments);
        public const string CanSubmitBHFSMTA2ShipmentDocuments = nameof(CanSubmitBHFSMTA2ShipmentDocuments);
        public const string CanDownloadBHFSMTA2ShipmentDocuments = nameof(CanDownloadBHFSMTA2ShipmentDocuments);
        public const string CanReadQESMTA2ShipmentDocuments = nameof(CanReadQESMTA2ShipmentDocuments);
        public const string CanSubmitQESMTA2ShipmentDocuments = nameof(CanSubmitQESMTA2ShipmentDocuments);
        public const string CanDownloadQESMTA2ShipmentDocuments = nameof(CanDownloadQESMTA2ShipmentDocuments);
        public const string CanReadWaitForPickUpFromBioHubCompleted = nameof(CanReadWaitForPickUpFromBioHubCompleted);
        public const string CanSubmitWaitForPickUpFromBioHubCompleted = nameof(CanSubmitWaitForPickUpFromBioHubCompleted);
        public const string CanDownloadFileWaitForPickUpFromBioHubCompleted = nameof(CanDownloadFileWaitForPickUpFromBioHubCompleted);
        public const string CanReadWaitForDeliveryFromBioHubCompleted = nameof(CanReadWaitForDeliveryFromBioHubCompleted);
        public const string CanSubmitWaitForDeliveryFromBioHubCompleted = nameof(CanSubmitWaitForDeliveryFromBioHubCompleted);
        public const string CanDownloadFileWaitForDeliveryFromBioHubCompleted = nameof(CanDownloadFileWaitForDeliveryFromBioHubCompleted);
        public const string CanReadWaitForArrivalConditionFromBioHubCheck = nameof(CanReadWaitForArrivalConditionFromBioHubCheck);
        public const string CanSubmitWaitForArrivalConditionFromBioHubCheck = nameof(CanSubmitWaitForArrivalConditionFromBioHubCheck);
        public const string CanDownloadFileWaitForArrivalConditionFromBioHubCheck = nameof(CanDownloadFileWaitForArrivalConditionFromBioHubCheck);
        public const string CanReadWaitForCommentQESendFeedback = nameof(CanReadWaitForCommentQESendFeedback);
        public const string CanSubmitWaitForCommentQESendFeedback = nameof(CanSubmitWaitForCommentQESendFeedback);
        public const string CanDownloadFileWaitForCommentQESendFeedback = nameof(CanDownloadFileWaitForCommentQESendFeedback);
        public const string CanReadWaitForFinalApprovalFromBioHub = nameof(CanReadWaitForFinalApprovalFromBioHub);
        public const string CanSubmitWaitForFinalApprovalFromBioHub = nameof(CanSubmitWaitForFinalApprovalFromBioHub);
        public const string CanDownloadFileWaitForFinalApprovalFromBioHub = nameof(CanDownloadFileWaitForFinalApprovalFromBioHub);
        public const string CanReadShipmentFromBioHubCompleted = nameof(CanReadShipmentFromBioHubCompleted);
        public const string CanSubmitShipmentFromBioHubCompleted = nameof(CanSubmitShipmentFromBioHubCompleted);
        public const string CanDownloadFileShipmentFromBioHubCompleted = nameof(CanDownloadFileShipmentFromBioHubCompleted);

        public const string CanReceiveEmailsOnRequestInitiationApproval = nameof(CanReceiveEmailsOnRequestInitiationApproval);
        public const string CanReceiveEmailsOnSubmitSMTA2Approval = nameof(CanReceiveEmailsOnSubmitSMTA2Approval);
        public const string CanReceiveEmailsOnWaitingForSMTA2SECsApprovalApproval = nameof(CanReceiveEmailsOnWaitingForSMTA2SECsApprovalApproval);
        public const string CanReceiveEmailsOnSubmitAnnex2OfSMTA2Approval = nameof(CanReceiveEmailsOnSubmitAnnex2OfSMTA2Approval);
        public const string CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalApproval = nameof(CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalApproval);
        public const string CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2Approval = nameof(CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2Approval);
        public const string CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApproval = nameof(CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalApproval);
        public const string CanReceiveEmailsOnSubmitBookingFormOfSMTA2Approval = nameof(CanReceiveEmailsOnSubmitBookingFormOfSMTA2Approval);
        public const string CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalApproval = nameof(CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalApproval);
        public const string CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsApproval = nameof(CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsApproval);
        public const string CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsApproval = nameof(CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsApproval);
        public const string CanReceiveEmailsOnWaitForPickUpFromBioHubCompletedApproval = nameof(CanReceiveEmailsOnWaitForPickUpFromBioHubCompletedApproval);
        public const string CanReceiveEmailsOnWaitForDeliveryFromBioHubCompletedApproval = nameof(CanReceiveEmailsOnWaitForDeliveryFromBioHubCompletedApproval);
        public const string CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckApproval = nameof(CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckApproval);
        public const string CanReceiveEmailsOnWaitForCommentQESendFeedbackApproval = nameof(CanReceiveEmailsOnWaitForCommentQESendFeedbackApproval);
        public const string CanReceiveEmailsOnWaitForFinalApprovalFromBioHubApproval = nameof(CanReceiveEmailsOnWaitForFinalApprovalFromBioHubApproval);
        public const string CanReceiveEmailsOnShipmentFromBioHubCompletedApproval = nameof(CanReceiveEmailsOnShipmentFromBioHubCompletedApproval);



        public const string CanReceiveEmailsOnRequestInitiationReject = nameof(CanReceiveEmailsOnRequestInitiationReject);
        public const string CanReceiveEmailsOnSubmitSMTA2Reject = nameof(CanReceiveEmailsOnSubmitSMTA2Reject);
        public const string CanReceiveEmailsOnWaitingForSMTA2SECsApprovalReject = nameof(CanReceiveEmailsOnWaitingForSMTA2SECsApprovalReject);
        public const string CanReceiveEmailsOnSubmitAnnex2OfSMTA2Reject = nameof(CanReceiveEmailsOnSubmitAnnex2OfSMTA2Reject);
        public const string CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalReject = nameof(CanReceiveEmailsOnWaitingForAnnex2OfSMTA2SECsApprovalReject);
        public const string CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2Reject = nameof(CanReceiveEmailsOnSubmitBiosafetyChecklistFormOfSMTA2Reject);
        public const string CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalReject = nameof(CanReceiveEmailsOnWaitForBiosafetyChecklistFormSMTA2BSFsApprovalReject);
        public const string CanReceiveEmailsOnSubmitBookingFormOfSMTA2Reject = nameof(CanReceiveEmailsOnSubmitBookingFormOfSMTA2Reject);
        public const string CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalReject = nameof(CanReceiveEmailsOnWaitForBookingFormSMTA2OPSsApprovalReject);
        public const string CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsReject = nameof(CanReceiveEmailsOnSubmitBHFSMTA2ShipmentDocumentsReject);
        public const string CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsReject = nameof(CanReceiveEmailsOnSubmitQESMTA2ShipmentDocumentsReject);
        public const string CanReceiveEmailsOnWaitForPickUpFromBioHubCompletedReject = nameof(CanReceiveEmailsOnWaitForPickUpFromBioHubCompletedReject);
        public const string CanReceiveEmailsOnWaitForDeliveryFromBioHubCompletedReject = nameof(CanReceiveEmailsOnWaitForDeliveryFromBioHubCompletedReject);
        public const string CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckReject = nameof(CanReceiveEmailsOnWaitForSMTA2ArrivalConditionCheckReject);
        public const string CanReceiveEmailsOnWaitForSMTA2FinalApprovalReject = nameof(CanReceiveEmailsOnWaitForSMTA2FinalApprovalReject);
        public const string CanReceiveEmailsOnWaitForFinalApprovalFromBioHubReject = nameof(CanReceiveEmailsOnWaitForFinalApprovalFromBioHubReject);
        public const string CanReceiveEmailsOnShipmentFromBioHubCompletedReject = nameof(CanReceiveEmailsOnShipmentFromBioHubCompletedReject);

        public const string CanReadDocument = nameof(CanReadDocument);

        public const string CanApproveBioHubFacilityCompletion = nameof(CanApproveBioHubFacilityCompletion);
        public const string CanApproveLaboratoryCompletion = nameof(CanApproveLaboratoryCompletion);
        public const string CanVerifyMaterial = nameof(CanVerifyMaterial);
        public const string CanSetMaterialReadyToShare = nameof(CanSetMaterialReadyToShare);
        public const string CanSetMaterialPublic = nameof(CanSetMaterialPublic);

        public const string CanReceiveEmailsOnRequestAccess = nameof(CanReceiveEmailsOnRequestAccess);

        public const string CanReadKpiData = nameof(CanReadKpiData);

        public const string CanReceiveEmailsOnSubmitSMTA2 = nameof(CanReceiveEmailsOnSubmitSMTA2);
        public const string CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalApproval = nameof(CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalApproval);
        public const string CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalReject = nameof(CanReceiveEmailsOnSubmitWaitingForSMTA2SECsApprovalReject);
        public const string CanReceiveEmailsOnSMTA2BHFShipmentDocumentsApproval = nameof(CanReceiveEmailsOnSMTA2BHFShipmentDocumentsApproval);
        public const string CanReceiveEmailsOnWaitForSMTA2PickUpCompletedApproval = nameof(CanReceiveEmailsOnWaitForSMTA2PickUpCompletedApproval);
        public const string CanReceiveEmailsOnWaitForSMTA2PickUpCompletedReject = nameof(CanReceiveEmailsOnWaitForSMTA2PickUpCompletedReject);
        public const string CanReceiveEmailsOnSMTA2QEShipmentDocumentsApproval = nameof(CanReceiveEmailsOnSMTA2QEShipmentDocumentsApproval);
        public const string CanReceiveEmailsOnWaitForSMTA2DeliveryCompletedApproval = nameof(CanReceiveEmailsOnWaitForSMTA2DeliveryCompletedApproval);
        public const string CanReceiveEmailsOnWaitForSMTA2FinalApprovalApproval = nameof(CanReceiveEmailsOnWaitForSMTA2FinalApprovalApproval);

        public const string CanReadSMTA1WorkflowComplete = nameof(CanReadSMTA1WorkflowComplete);
        public const string CanSubmitSMTA1WorkflowComplete = nameof(CanSubmitSMTA1WorkflowComplete);
        public const string CanDownloadFileSMTA1WorkflowComplete = nameof(CanDownloadFileSMTA1WorkflowComplete);
        public const string CanReceiveEmailsOnSMTA1WorkflowCompleteApproval = nameof(CanReceiveEmailsOnSMTA1WorkflowCompleteApproval);

        public const string CanReadSMTA2WorkflowComplete = nameof(CanReadSMTA2WorkflowComplete);
        public const string CanSubmitSMTA2WorkflowComplete = nameof(CanSubmitSMTA2WorkflowComplete);
        public const string CanDownloadFileSMTA2WorkflowComplete = nameof(CanDownloadFileSMTA2WorkflowComplete);
        public const string CanReceiveEmailsOnSMTA2WorkflowCompleteApproval = nameof(CanReceiveEmailsOnSMTA2WorkflowCompleteApproval);

        public const string CanAccessSMTAWorkflow = nameof(CanAccessSMTAWorkflow);

        public const string CanAccessPastSMTAWorkflow = nameof(CanAccessPastSMTAWorkflow);

        public const string CanAddMaterialNewVials = nameof(CanAddMaterialNewVials);

        public const string CanEditMaterialOwnerBioHubFacility = nameof(CanEditMaterialOwnerBioHubFacility);
        public const string CanEditMaterialShipmentNumberOfVials = nameof(CanEditMaterialShipmentNumberOfVials);
        public const string CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold = nameof(CanEditMaterialWarningEmailCurrentNumberOfVialsThreshold);


        public const string CanCreateResource = nameof(CanCreateResource);
        public const string CanEditResource = nameof(CanEditResource);
        public const string CanReadResource = nameof(CanReadResource);
        public const string CanDeleteResource = nameof(CanDeleteResource);

        public const string CanCreateSpecimenType = nameof(CanCreateSpecimenType);
        public const string CanEditSpecimenType = nameof(CanEditSpecimenType);
        public const string CanReadSpecimenType = nameof(CanReadSpecimenType);
        public const string CanDeleteSpecimenType = nameof(CanDeleteSpecimenType);


        public const string CanEditMaterialShipmentInformation = nameof(CanEditMaterialShipmentInformation);





        public const string CanReadSubmitSMTA1Past = nameof(CanReadSubmitSMTA1Past);
        public const string CanReadWaitingForSMTA1SECsApprovalPast = nameof(CanReadWaitingForSMTA1SECsApprovalPast);
        public const string CanReadSMTA1WorkflowCompletePast = nameof(CanReadSMTA1WorkflowCompletePast);
        public const string CanSubmitSMTA1Past = nameof(CanSubmitSMTA1Past);
        public const string CanSubmitWaitingForSMTA1SECsApprovalPast = nameof(CanSubmitWaitingForSMTA1SECsApprovalPast);
        public const string CanSubmitSMTA1WorkflowCompletePast = nameof(CanSubmitSMTA1WorkflowCompletePast);
        public const string CanDownloadFileSubmitSMTA1Past = nameof(CanDownloadFileSubmitSMTA1Past);
        public const string CanDownloadFileWaitingForSMTA1SECsApprovalPast = nameof(CanDownloadFileWaitingForSMTA1SECsApprovalPast);
        public const string CanDownloadFileSMTA1WorkflowCompletePast = nameof(CanDownloadFileSMTA1WorkflowCompletePast);
        public const string CanReadSubmitAnnex2OfSMTA1Past = nameof(CanReadSubmitAnnex2OfSMTA1Past);
        public const string CanReadWaitingForAnnex2OfSMTA1SECsApprovalPast = nameof(CanReadWaitingForAnnex2OfSMTA1SECsApprovalPast);
        public const string CanReadSubmitBookingFormOfSMTA1Past = nameof(CanReadSubmitBookingFormOfSMTA1Past);
        public const string CanReadWaitForBookingFormSMTA1OPSApprovalPast = nameof(CanReadWaitForBookingFormSMTA1OPSApprovalPast);
        public const string CanReadSMTA1ShipmentDocumentsPast = nameof(CanReadSMTA1ShipmentDocumentsPast);
        public const string CanReadWaitForPickUpCompletedPast = nameof(CanReadWaitForPickUpCompletedPast);
        public const string CanReadWaitForDeliveryCompletedPast = nameof(CanReadWaitForDeliveryCompletedPast);
        public const string CanReadWaitForArrivalConditionCheckPast = nameof(CanReadWaitForArrivalConditionCheckPast);
        public const string CanReadWaitForCommentBHFSendFeedbackPast = nameof(CanReadWaitForCommentBHFSendFeedbackPast);
        public const string CanReadWaitForFinalApprovalPast = nameof(CanReadWaitForFinalApprovalPast);
        public const string CanReadShipmentCompletedPast = nameof(CanReadShipmentCompletedPast);
        public const string CanSubmitAnnex2OfSMTA1Past = nameof(CanSubmitAnnex2OfSMTA1Past);
        public const string CanSubmitWaitingForAnnex2OfSMTA1SECsApprovalPast = nameof(CanSubmitWaitingForAnnex2OfSMTA1SECsApprovalPast);
        public const string CanSubmitBookingFormOfSMTA1Past = nameof(CanSubmitBookingFormOfSMTA1Past);
        public const string CanSubmitWaitForBookingFormSMTA1OPSApprovalPast = nameof(CanSubmitWaitForBookingFormSMTA1OPSApprovalPast);
        public const string CanSubmitSMTA1ShipmentDocumentsPast = nameof(CanSubmitSMTA1ShipmentDocumentsPast);
        public const string CanSubmitWaitForPickUpCompletedPast = nameof(CanSubmitWaitForPickUpCompletedPast);
        public const string CanSubmitWaitForDeliveryCompletedPast = nameof(CanSubmitWaitForDeliveryCompletedPast);
        public const string CanSubmitWaitForArrivalConditionCheckPast = nameof(CanSubmitWaitForArrivalConditionCheckPast);
        public const string CanSubmitWaitForCommentBHFSendFeedbackPast = nameof(CanSubmitWaitForCommentBHFSendFeedbackPast);
        public const string CanSubmitWaitForFinalApprovalPast = nameof(CanSubmitWaitForFinalApprovalPast);
        public const string CanSubmitShipmentCompletedPast = nameof(CanSubmitShipmentCompletedPast);
        public const string CanDownloadFileSubmitAnnex2OfSMTA1Past = nameof(CanDownloadFileSubmitAnnex2OfSMTA1Past);
        public const string CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPast = nameof(CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPast);
        public const string CanDownloadFileSubmitBookingFormOfSMTA1Past = nameof(CanDownloadFileSubmitBookingFormOfSMTA1Past);
        public const string CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPast = nameof(CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPast);
        public const string CanDownloadSMTA1ShipmentDocumentsPast = nameof(CanDownloadSMTA1ShipmentDocumentsPast);
        public const string CanDownloadFileWaitForPickUpCompletedPast = nameof(CanDownloadFileWaitForPickUpCompletedPast);
        public const string CanDownloadFileWaitForDeliveryCompletedPast = nameof(CanDownloadFileWaitForDeliveryCompletedPast);
        public const string CanDownloadFileWaitForArrivalConditionCheckPast = nameof(CanDownloadFileWaitForArrivalConditionCheckPast);
        public const string CanDownloadFileWaitForCommentBHFSendFeedbackPast = nameof(CanDownloadFileWaitForCommentBHFSendFeedbackPast);
        public const string CanDownloadFileWaitForFinalApprovalPast = nameof(CanDownloadFileWaitForFinalApprovalPast);
        public const string CanDownloadFileShipmentCompletedPast = nameof(CanDownloadFileShipmentCompletedPast);
        public const string CanReadSubmitSMTA2Past = nameof(CanReadSubmitSMTA2Past);
        public const string CanReadWaitingForSMTA2SECsApprovalPast = nameof(CanReadWaitingForSMTA2SECsApprovalPast);
        public const string CanReadSMTA2WorkflowCompletePast = nameof(CanReadSMTA2WorkflowCompletePast);
        public const string CanSubmitSMTA2Past = nameof(CanSubmitSMTA2Past);
        public const string CanSubmitWaitingForSMTA2SECsApprovalPast = nameof(CanSubmitWaitingForSMTA2SECsApprovalPast);
        public const string CanSubmitSMTA2WorkflowCompletePast = nameof(CanSubmitSMTA2WorkflowCompletePast);
        public const string CanDownloadFileSubmitSMTA2Past = nameof(CanDownloadFileSubmitSMTA2Past);
        public const string CanDownloadFileWaitingForSMTA2SECsApprovalPast = nameof(CanDownloadFileWaitingForSMTA2SECsApprovalPast);
        public const string CanDownloadFileSMTA2WorkflowCompletePast = nameof(CanDownloadFileSMTA2WorkflowCompletePast);
        public const string CanReadSubmitAnnex2OfSMTA2Past = nameof(CanReadSubmitAnnex2OfSMTA2Past);
        public const string CanReadWaitingForAnnex2OfSMTA2SECsApprovalPast = nameof(CanReadWaitingForAnnex2OfSMTA2SECsApprovalPast);
        public const string CanReadSubmitBiosafetyChecklistFormOfSMTA2Past = nameof(CanReadSubmitBiosafetyChecklistFormOfSMTA2Past);
        public const string CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast = nameof(CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast);
        public const string CanReadSubmitBookingFormOfSMTA2Past = nameof(CanReadSubmitBookingFormOfSMTA2Past);
        public const string CanReadWaitForBookingFormSMTA2OPSsApprovalPast = nameof(CanReadWaitForBookingFormSMTA2OPSsApprovalPast);
        public const string CanReadBHFSMTA2ShipmentDocumentsPast = nameof(CanReadBHFSMTA2ShipmentDocumentsPast);
        public const string CanReadQESMTA2ShipmentDocumentsPast = nameof(CanReadQESMTA2ShipmentDocumentsPast);
        public const string CanReadWaitForPickUpFromBioHubCompletedPast = nameof(CanReadWaitForPickUpFromBioHubCompletedPast);
        public const string CanReadWaitForDeliveryFromBioHubCompletedPast = nameof(CanReadWaitForDeliveryFromBioHubCompletedPast);
        public const string CanReadWaitForArrivalConditionFromBioHubCheckPast = nameof(CanReadWaitForArrivalConditionFromBioHubCheckPast);
        public const string CanReadWaitForCommentQESendFeedbackPast = nameof(CanReadWaitForCommentQESendFeedbackPast);
        public const string CanReadWaitForFinalApprovalFromBioHubPast = nameof(CanReadWaitForFinalApprovalFromBioHubPast);
        public const string CanReadShipmentFromBioHubCompletedPast = nameof(CanReadShipmentFromBioHubCompletedPast);
        public const string CanSubmitAnnex2OfSMTA2Past = nameof(CanSubmitAnnex2OfSMTA2Past);
        public const string CanSubmitWaitingForAnnex2OfSMTA2SECsApprovalPast = nameof(CanSubmitWaitingForAnnex2OfSMTA2SECsApprovalPast);
        public const string CanSubmitBiosafetyChecklistFormOfSMTA2Past = nameof(CanSubmitBiosafetyChecklistFormOfSMTA2Past);
        public const string CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast = nameof(CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast);
        public const string CanSubmitBookingFormOfSMTA2Past = nameof(CanSubmitBookingFormOfSMTA2Past);
        public const string CanSubmitWaitForBookingFormSMTA2OPSsApprovalPast = nameof(CanSubmitWaitForBookingFormSMTA2OPSsApprovalPast);
        public const string CanSubmitBHFSMTA2ShipmentDocumentsPast = nameof(CanSubmitBHFSMTA2ShipmentDocumentsPast);
        public const string CanSubmitQESMTA2ShipmentDocumentsPast = nameof(CanSubmitQESMTA2ShipmentDocumentsPast);
        public const string CanSubmitWaitForPickUpFromBioHubCompletedPast = nameof(CanSubmitWaitForPickUpFromBioHubCompletedPast);
        public const string CanSubmitWaitForDeliveryFromBioHubCompletedPast = nameof(CanSubmitWaitForDeliveryFromBioHubCompletedPast);
        public const string CanSubmitWaitForArrivalConditionFromBioHubCheckPast = nameof(CanSubmitWaitForArrivalConditionFromBioHubCheckPast);
        public const string CanSubmitWaitForCommentQESendFeedbackPast = nameof(CanSubmitWaitForCommentQESendFeedbackPast);
        public const string CanSubmitWaitForFinalApprovalFromBioHubPast = nameof(CanSubmitWaitForFinalApprovalFromBioHubPast);
        public const string CanSubmitShipmentFromBioHubCompletedPast = nameof(CanSubmitShipmentFromBioHubCompletedPast);
        public const string CanDownloadFileSubmitAnnex2OfSMTA2Past = nameof(CanDownloadFileSubmitAnnex2OfSMTA2Past);
        public const string CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPast = nameof(CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPast);
        public const string CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2Past = nameof(CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2Past);
        public const string CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast = nameof(CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast);
        public const string CanDownloadFileSubmitBookingFormOfSMTA2Past = nameof(CanDownloadFileSubmitBookingFormOfSMTA2Past);
        public const string CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPast = nameof(CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPast);
        public const string CanDownloadBHFSMTA2ShipmentDocumentsPast = nameof(CanDownloadBHFSMTA2ShipmentDocumentsPast);
        public const string CanDownloadQESMTA2ShipmentDocumentsPast = nameof(CanDownloadQESMTA2ShipmentDocumentsPast);
        public const string CanDownloadFileWaitForPickUpFromBioHubCompletedPast = nameof(CanDownloadFileWaitForPickUpFromBioHubCompletedPast);
        public const string CanDownloadFileWaitForDeliveryFromBioHubCompletedPast = nameof(CanDownloadFileWaitForDeliveryFromBioHubCompletedPast);
        public const string CanDownloadFileWaitForArrivalConditionFromBioHubCheckPast = nameof(CanDownloadFileWaitForArrivalConditionFromBioHubCheckPast);
        public const string CanDownloadFileWaitForCommentQESendFeedbackPast = nameof(CanDownloadFileWaitForCommentQESendFeedbackPast);
        public const string CanDownloadFileWaitForFinalApprovalFromBioHubPast = nameof(CanDownloadFileWaitForFinalApprovalFromBioHubPast);
        public const string CanDownloadFileShipmentFromBioHubCompletedPast = nameof(CanDownloadFileShipmentFromBioHubCompletedPast);


        public const string CanAccessPastRequestIniziation = nameof(CanAccessPastRequestIniziation);
        public const string CanAccessPastWorklist = nameof(CanAccessPastWorklist);
        
        public const string CanReadOnBehalfOfRoles = nameof(CanReadOnBehalfOfRoles);

        public const string CanReadEForm = nameof(CanReadEForm);

        public const string CanReceiveEmailOnNumberOfVialsWarning = nameof(CanReceiveEmailOnNumberOfVialsWarning);
    }
}
