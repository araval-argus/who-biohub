using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Permissions
{
    public static class StatusPermissionMapper
    {

        public static readonly Dictionary<SMTA1WorkflowStatus, string> SMTA1WorkflowStatusReadPermissionMapper = new Dictionary<SMTA1WorkflowStatus, string>()
        {
            { SMTA1WorkflowStatus.SubmitSMTA1, PermissionNames.CanReadSubmitSMTA1 },
            { SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval, PermissionNames.CanReadWaitingForSMTA1SECsApproval },
            { SMTA1WorkflowStatus.SMTA1WorkflowComplete, PermissionNames.CanReadSMTA1WorkflowComplete },

        };

        public static readonly Dictionary<SMTA1WorkflowStatus, string> SMTA1WorkflowStatusUpdatePermissionMapper = new Dictionary<SMTA1WorkflowStatus, string>()
        {
            { SMTA1WorkflowStatus.SubmitSMTA1, PermissionNames.CanSubmitSMTA1 },
            { SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval, PermissionNames.CanSubmitWaitingForSMTA1SECsApproval },
            { SMTA1WorkflowStatus.SMTA1WorkflowComplete, PermissionNames.CanSubmitSMTA1WorkflowComplete },
        };

        public static readonly Dictionary<SMTA1WorkflowStatus, string> SMTA1WorkflowStatusDownloadFilePermissionMapper = new Dictionary<SMTA1WorkflowStatus, string>()
        {
            { SMTA1WorkflowStatus.SubmitSMTA1, PermissionNames.CanDownloadFileSubmitSMTA1 },
            { SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval, PermissionNames.CanDownloadFileWaitingForSMTA1SECsApproval },
            { SMTA1WorkflowStatus.SMTA1WorkflowComplete, PermissionNames.CanDownloadFileSMTA1WorkflowComplete },
        };

        public static readonly Dictionary<WorklistToBioHubStatus, string> WorklistToBioHubStatusReadPermissionMapper = new Dictionary<WorklistToBioHubStatus, string>()
        {
            { WorklistToBioHubStatus.SubmitAnnex2OfSMTA1, PermissionNames.CanReadSubmitAnnex2OfSMTA1 },
            { WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval, PermissionNames.CanReadWaitingForAnnex2OfSMTA1SECsApproval },
            { WorklistToBioHubStatus.SubmitBookingFormOfSMTA1, PermissionNames.CanReadSubmitBookingFormOfSMTA1 },
            { WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval, PermissionNames.CanReadWaitForBookingFormSMTA1OPSApproval },
            { WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments, PermissionNames.CanReadSMTA1ShipmentDocuments },
            { WorklistToBioHubStatus.WaitForPickUpCompleted, PermissionNames.CanReadWaitForPickUpCompleted },
            { WorklistToBioHubStatus.WaitForDeliveryCompleted, PermissionNames.CanReadWaitForDeliveryCompleted },
            { WorklistToBioHubStatus.WaitForArrivalConditionCheck, PermissionNames.CanReadWaitForArrivalConditionCheck },
            { WorklistToBioHubStatus.WaitForCommentBHFSendFeedback, PermissionNames.CanReadWaitForCommentBHFSendFeedback },
            { WorklistToBioHubStatus.WaitForFinalApproval, PermissionNames.CanReadWaitForFinalApproval },
            { WorklistToBioHubStatus.ShipmentCompleted, PermissionNames.CanReadShipmentCompleted },

        };

        public static readonly Dictionary<WorklistToBioHubStatus, string> WorklistToBioHubStatusUpdatePermissionMapper = new Dictionary<WorklistToBioHubStatus, string>()
        {
            { WorklistToBioHubStatus.SubmitAnnex2OfSMTA1, PermissionNames.CanSubmitAnnex2OfSMTA1 },
            { WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval, PermissionNames.CanSubmitWaitingForAnnex2OfSMTA1SECsApproval },
            { WorklistToBioHubStatus.SubmitBookingFormOfSMTA1, PermissionNames.CanSubmitBookingFormOfSMTA1 },
            { WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval, PermissionNames.CanSubmitWaitForBookingFormSMTA1OPSApproval },
            { WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments, PermissionNames.CanSubmitSMTA1ShipmentDocuments },
            { WorklistToBioHubStatus.WaitForPickUpCompleted, PermissionNames.CanSubmitWaitForPickUpCompleted },
            { WorklistToBioHubStatus.WaitForDeliveryCompleted, PermissionNames.CanSubmitWaitForDeliveryCompleted },
            { WorklistToBioHubStatus.WaitForArrivalConditionCheck, PermissionNames.CanSubmitWaitForArrivalConditionCheck },
            { WorklistToBioHubStatus.WaitForCommentBHFSendFeedback, PermissionNames.CanSubmitWaitForCommentBHFSendFeedback },
            { WorklistToBioHubStatus.WaitForFinalApproval, PermissionNames.CanSubmitWaitForFinalApproval },
            { WorklistToBioHubStatus.ShipmentCompleted, PermissionNames.CanSubmitShipmentCompleted },

        };

        public static readonly Dictionary<WorklistToBioHubStatus, string> WorklistToBioHubStatusDownloadFilePermissionMapper = new Dictionary<WorklistToBioHubStatus, string>()
        {
            { WorklistToBioHubStatus.SubmitAnnex2OfSMTA1, PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA1 },
            { WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval, PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA1SECsApproval },
            { WorklistToBioHubStatus.SubmitBookingFormOfSMTA1, PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA1 },
            { WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval, PermissionNames.CanDownloadFileWaitForBookingFormSMTA1OPSApproval },
            { WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments, PermissionNames.CanDownloadSMTA1ShipmentDocuments },
            { WorklistToBioHubStatus.WaitForPickUpCompleted, PermissionNames.CanDownloadFileWaitForPickUpCompleted },
            { WorklistToBioHubStatus.WaitForDeliveryCompleted, PermissionNames.CanDownloadFileWaitForDeliveryCompleted },
            { WorklistToBioHubStatus.WaitForArrivalConditionCheck, PermissionNames.CanDownloadFileWaitForArrivalConditionCheck },
            { WorklistToBioHubStatus.WaitForCommentBHFSendFeedback, PermissionNames.CanDownloadFileWaitForCommentBHFSendFeedback },
            { WorklistToBioHubStatus.WaitForFinalApproval, PermissionNames.CanDownloadFileWaitForFinalApproval },
            { WorklistToBioHubStatus.ShipmentCompleted, PermissionNames.CanDownloadFileShipmentCompleted },
        };

        public static readonly Dictionary<SMTA2WorkflowStatus, string> SMTA2WorkflowStatusReadPermissionMapper = new Dictionary<SMTA2WorkflowStatus, string>()
        {
            { SMTA2WorkflowStatus.SubmitSMTA2, PermissionNames.CanReadSubmitSMTA2 },
            { SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval, PermissionNames.CanReadWaitingForSMTA2SECsApproval },
            { SMTA2WorkflowStatus.SMTA2WorkflowComplete, PermissionNames.CanReadSMTA2WorkflowComplete },

        };

        public static readonly Dictionary<SMTA2WorkflowStatus, string> SMTA2WorkflowStatusUpdatePermissionMapper = new Dictionary<SMTA2WorkflowStatus, string>()
        {
            { SMTA2WorkflowStatus.SubmitSMTA2, PermissionNames.CanSubmitSMTA2 },
            { SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval, PermissionNames.CanSubmitWaitingForSMTA2SECsApproval },
            { SMTA2WorkflowStatus.SMTA2WorkflowComplete, PermissionNames.CanSubmitSMTA2WorkflowComplete },
        };

        public static readonly Dictionary<SMTA2WorkflowStatus, string> SMTA2WorkflowStatusDownloadFilePermissionMapper = new Dictionary<SMTA2WorkflowStatus, string>()
        {
            { SMTA2WorkflowStatus.SubmitSMTA2, PermissionNames.CanDownloadFileSubmitSMTA2 },
            { SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval, PermissionNames.CanDownloadFileWaitingForSMTA2SECsApproval },
            { SMTA2WorkflowStatus.SMTA2WorkflowComplete, PermissionNames.CanDownloadFileSMTA2WorkflowComplete },
        };

        public static readonly Dictionary<WorklistFromBioHubStatus, string> WorklistFromBioHubStatusReadPermissionMapper = new Dictionary<WorklistFromBioHubStatus, string>()
        {
            { WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2, PermissionNames.CanReadSubmitAnnex2OfSMTA2 },
            { WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval, PermissionNames.CanReadWaitingForAnnex2OfSMTA2SECsApproval },
            { WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2, PermissionNames.CanReadSubmitBiosafetyChecklistFormOfSMTA2 },
            { WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval, PermissionNames.CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApproval },
            { WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2, PermissionNames.CanReadSubmitBookingFormOfSMTA2 },
            { WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval, PermissionNames.CanReadWaitForBookingFormSMTA2OPSsApproval },
            { WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments, PermissionNames.CanReadBHFSMTA2ShipmentDocuments },
            { WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments, PermissionNames.CanReadQESMTA2ShipmentDocuments },
            { WorklistFromBioHubStatus.WaitForPickUpCompleted, PermissionNames.CanReadWaitForPickUpFromBioHubCompleted },
            { WorklistFromBioHubStatus.WaitForDeliveryCompleted, PermissionNames.CanReadWaitForDeliveryFromBioHubCompleted },
            { WorklistFromBioHubStatus.WaitForArrivalConditionCheck, PermissionNames.CanReadWaitForArrivalConditionFromBioHubCheck },
            { WorklistFromBioHubStatus.WaitForCommentQESendFeedback, PermissionNames.CanReadWaitForCommentQESendFeedback },
            { WorklistFromBioHubStatus.WaitForFinalApproval, PermissionNames.CanReadWaitForFinalApprovalFromBioHub },
            { WorklistFromBioHubStatus.ShipmentCompleted, PermissionNames.CanReadShipmentFromBioHubCompleted },
        };

        public static readonly Dictionary<WorklistFromBioHubStatus, string> WorklistFromBioHubStatusUpdatePermissionMapper = new Dictionary<WorklistFromBioHubStatus, string>()
        {
            { WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2, PermissionNames.CanSubmitAnnex2OfSMTA2 },
            { WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval, PermissionNames.CanSubmitWaitingForAnnex2OfSMTA2SECsApproval },
            { WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2, PermissionNames.CanSubmitBiosafetyChecklistFormOfSMTA2 },
            { WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval, PermissionNames.CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApproval },
            { WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2, PermissionNames.CanSubmitBookingFormOfSMTA2 },
            { WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval, PermissionNames.CanSubmitWaitForBookingFormSMTA2OPSsApproval },
            { WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments, PermissionNames.CanSubmitBHFSMTA2ShipmentDocuments },
            { WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments, PermissionNames.CanSubmitQESMTA2ShipmentDocuments },
            { WorklistFromBioHubStatus.WaitForPickUpCompleted, PermissionNames.CanSubmitWaitForPickUpFromBioHubCompleted },
            { WorklistFromBioHubStatus.WaitForDeliveryCompleted, PermissionNames.CanSubmitWaitForDeliveryFromBioHubCompleted },
            { WorklistFromBioHubStatus.WaitForArrivalConditionCheck, PermissionNames.CanSubmitWaitForArrivalConditionFromBioHubCheck },
            { WorklistFromBioHubStatus.WaitForCommentQESendFeedback, PermissionNames.CanSubmitWaitForCommentQESendFeedback },
            { WorklistFromBioHubStatus.WaitForFinalApproval, PermissionNames.CanSubmitWaitForFinalApprovalFromBioHub },
            { WorklistFromBioHubStatus.ShipmentCompleted, PermissionNames.CanSubmitShipmentFromBioHubCompleted },

        };

        public static readonly Dictionary<WorklistFromBioHubStatus, string> WorklistFromBioHubStatusDownloadFilePermissionMapper = new Dictionary<WorklistFromBioHubStatus, string>()
        {
            { WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2, PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA2 },
            { WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval, PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA2SECsApproval },
            { WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2, PermissionNames.CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2 },
            { WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval, PermissionNames.CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApproval },
            { WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2, PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA2 },
            { WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval, PermissionNames.CanDownloadFileWaitForBookingFormSMTA2OPSsApproval },
            { WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments, PermissionNames.CanDownloadBHFSMTA2ShipmentDocuments },
            { WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments, PermissionNames.CanDownloadQESMTA2ShipmentDocuments },
            { WorklistFromBioHubStatus.WaitForPickUpCompleted, PermissionNames.CanDownloadFileWaitForPickUpFromBioHubCompleted },
            { WorklistFromBioHubStatus.WaitForDeliveryCompleted, PermissionNames.CanDownloadFileWaitForDeliveryFromBioHubCompleted },
            { WorklistFromBioHubStatus.WaitForArrivalConditionCheck, PermissionNames.CanDownloadFileWaitForArrivalConditionFromBioHubCheck },
            { WorklistFromBioHubStatus.WaitForCommentQESendFeedback, PermissionNames.CanDownloadFileWaitForCommentQESendFeedback },
            { WorklistFromBioHubStatus.WaitForFinalApproval, PermissionNames.CanDownloadFileWaitForFinalApprovalFromBioHub },
            { WorklistFromBioHubStatus.ShipmentCompleted, PermissionNames.CanDownloadFileShipmentFromBioHubCompleted },
        };


        public static readonly Dictionary<SMTA1WorkflowStatus, string> SMTA1WorkflowStatusReadPermissionPastMapper = new Dictionary<SMTA1WorkflowStatus, string>()
        {
            { SMTA1WorkflowStatus.SubmitSMTA1, PermissionNames.CanReadSubmitSMTA1Past },
            { SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval, PermissionNames.CanReadWaitingForSMTA1SECsApprovalPast },
            { SMTA1WorkflowStatus.SMTA1WorkflowComplete, PermissionNames.CanReadSMTA1WorkflowCompletePast },

        };

        public static readonly Dictionary<SMTA1WorkflowStatus, string> SMTA1WorkflowStatusUpdatePermissionPastMapper = new Dictionary<SMTA1WorkflowStatus, string>()
        {
            { SMTA1WorkflowStatus.SubmitSMTA1, PermissionNames.CanSubmitSMTA1Past },
            { SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval, PermissionNames.CanSubmitWaitingForSMTA1SECsApprovalPast },
            { SMTA1WorkflowStatus.SMTA1WorkflowComplete, PermissionNames.CanSubmitSMTA1WorkflowCompletePast },
        };

        public static readonly Dictionary<SMTA1WorkflowStatus, string> SMTA1WorkflowStatusDownloadFilePermissionPastMapper = new Dictionary<SMTA1WorkflowStatus, string>()
        {
            { SMTA1WorkflowStatus.SubmitSMTA1, PermissionNames.CanDownloadFileSubmitSMTA1Past },
            { SMTA1WorkflowStatus.WaitingForSMTA1SECsApproval, PermissionNames.CanDownloadFileWaitingForSMTA1SECsApprovalPast },
            { SMTA1WorkflowStatus.SMTA1WorkflowComplete, PermissionNames.CanDownloadFileSMTA1WorkflowCompletePast },
        };

        public static readonly Dictionary<WorklistToBioHubStatus, string> WorklistToBioHubStatusReadPermissionPastMapper = new Dictionary<WorklistToBioHubStatus, string>()
        {
            { WorklistToBioHubStatus.SubmitAnnex2OfSMTA1, PermissionNames.CanReadSubmitAnnex2OfSMTA1Past },
            { WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval, PermissionNames.CanReadWaitingForAnnex2OfSMTA1SECsApprovalPast },
            { WorklistToBioHubStatus.SubmitBookingFormOfSMTA1, PermissionNames.CanReadSubmitBookingFormOfSMTA1Past },
            { WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval, PermissionNames.CanReadWaitForBookingFormSMTA1OPSApprovalPast },
            { WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments, PermissionNames.CanReadSMTA1ShipmentDocumentsPast },
            { WorklistToBioHubStatus.WaitForPickUpCompleted, PermissionNames.CanReadWaitForPickUpCompletedPast },
            { WorklistToBioHubStatus.WaitForDeliveryCompleted, PermissionNames.CanReadWaitForDeliveryCompletedPast },
            { WorklistToBioHubStatus.WaitForArrivalConditionCheck, PermissionNames.CanReadWaitForArrivalConditionCheckPast },
            { WorklistToBioHubStatus.WaitForCommentBHFSendFeedback, PermissionNames.CanReadWaitForCommentBHFSendFeedbackPast },
            { WorklistToBioHubStatus.WaitForFinalApproval, PermissionNames.CanReadWaitForFinalApprovalPast },
            { WorklistToBioHubStatus.ShipmentCompleted, PermissionNames.CanReadShipmentCompletedPast },

        };

        public static readonly Dictionary<WorklistToBioHubStatus, string> WorklistToBioHubStatusUpdatePermissionPastMapper = new Dictionary<WorklistToBioHubStatus, string>()
        {
            { WorklistToBioHubStatus.SubmitAnnex2OfSMTA1, PermissionNames.CanSubmitAnnex2OfSMTA1Past },
            { WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval, PermissionNames.CanSubmitWaitingForAnnex2OfSMTA1SECsApprovalPast },
            { WorklistToBioHubStatus.SubmitBookingFormOfSMTA1, PermissionNames.CanSubmitBookingFormOfSMTA1Past },
            { WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval, PermissionNames.CanSubmitWaitForBookingFormSMTA1OPSApprovalPast },
            { WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments, PermissionNames.CanSubmitSMTA1ShipmentDocumentsPast },
            { WorklistToBioHubStatus.WaitForPickUpCompleted, PermissionNames.CanSubmitWaitForPickUpCompletedPast },
            { WorklistToBioHubStatus.WaitForDeliveryCompleted, PermissionNames.CanSubmitWaitForDeliveryCompletedPast },
            { WorklistToBioHubStatus.WaitForArrivalConditionCheck, PermissionNames.CanSubmitWaitForArrivalConditionCheckPast },
            { WorklistToBioHubStatus.WaitForCommentBHFSendFeedback, PermissionNames.CanSubmitWaitForCommentBHFSendFeedbackPast },
            { WorklistToBioHubStatus.WaitForFinalApproval, PermissionNames.CanSubmitWaitForFinalApprovalPast },
            { WorklistToBioHubStatus.ShipmentCompleted, PermissionNames.CanSubmitShipmentCompletedPast },

        };

        public static readonly Dictionary<WorklistToBioHubStatus, string> WorklistToBioHubStatusDownloadFilePermissionPastMapper = new Dictionary<WorklistToBioHubStatus, string>()
        {
            { WorklistToBioHubStatus.SubmitAnnex2OfSMTA1, PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA1Past },
            { WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval, PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA1SECsApprovalPast },
            { WorklistToBioHubStatus.SubmitBookingFormOfSMTA1, PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA1Past },
            { WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval, PermissionNames.CanDownloadFileWaitForBookingFormSMTA1OPSApprovalPast },
            { WorklistToBioHubStatus.SubmitSMTA1ShipmentDocuments, PermissionNames.CanDownloadSMTA1ShipmentDocumentsPast },
            { WorklistToBioHubStatus.WaitForPickUpCompleted, PermissionNames.CanDownloadFileWaitForPickUpCompletedPast },
            { WorklistToBioHubStatus.WaitForDeliveryCompleted, PermissionNames.CanDownloadFileWaitForDeliveryCompletedPast },
            { WorklistToBioHubStatus.WaitForArrivalConditionCheck, PermissionNames.CanDownloadFileWaitForArrivalConditionCheckPast },
            { WorklistToBioHubStatus.WaitForCommentBHFSendFeedback, PermissionNames.CanDownloadFileWaitForCommentBHFSendFeedbackPast },
            { WorklistToBioHubStatus.WaitForFinalApproval, PermissionNames.CanDownloadFileWaitForFinalApprovalPast },
            { WorklistToBioHubStatus.ShipmentCompleted, PermissionNames.CanDownloadFileShipmentCompletedPast },
        };

        public static readonly Dictionary<SMTA2WorkflowStatus, string> SMTA2WorkflowStatusReadPermissionPastMapper = new Dictionary<SMTA2WorkflowStatus, string>()
        {
            { SMTA2WorkflowStatus.SubmitSMTA2, PermissionNames.CanReadSubmitSMTA2Past },
            { SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval, PermissionNames.CanReadWaitingForSMTA2SECsApprovalPast },
            { SMTA2WorkflowStatus.SMTA2WorkflowComplete, PermissionNames.CanReadSMTA2WorkflowCompletePast },

        };

        public static readonly Dictionary<SMTA2WorkflowStatus, string> SMTA2WorkflowStatusUpdatePermissionPastMapper = new Dictionary<SMTA2WorkflowStatus, string>()
        {
            { SMTA2WorkflowStatus.SubmitSMTA2, PermissionNames.CanSubmitSMTA2Past },
            { SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval, PermissionNames.CanSubmitWaitingForSMTA2SECsApprovalPast },
            { SMTA2WorkflowStatus.SMTA2WorkflowComplete, PermissionNames.CanSubmitSMTA2WorkflowCompletePast },
        };

        public static readonly Dictionary<SMTA2WorkflowStatus, string> SMTA2WorkflowStatusDownloadFilePermissionPastMapper = new Dictionary<SMTA2WorkflowStatus, string>()
        {
            { SMTA2WorkflowStatus.SubmitSMTA2, PermissionNames.CanDownloadFileSubmitSMTA2Past },
            { SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval, PermissionNames.CanDownloadFileWaitingForSMTA2SECsApprovalPast },
            { SMTA2WorkflowStatus.SMTA2WorkflowComplete, PermissionNames.CanDownloadFileSMTA2WorkflowCompletePast },
        };

        public static readonly Dictionary<WorklistFromBioHubStatus, string> WorklistFromBioHubStatusReadPermissionPastMapper = new Dictionary<WorklistFromBioHubStatus, string>()
        {
            { WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2, PermissionNames.CanReadSubmitAnnex2OfSMTA2Past },
            { WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval, PermissionNames.CanReadWaitingForAnnex2OfSMTA2SECsApprovalPast },
            { WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2, PermissionNames.CanReadSubmitBiosafetyChecklistFormOfSMTA2Past },
            { WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval, PermissionNames.CanReadWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast },
            { WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2, PermissionNames.CanReadSubmitBookingFormOfSMTA2Past },
            { WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval, PermissionNames.CanReadWaitForBookingFormSMTA2OPSsApprovalPast },
            { WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments, PermissionNames.CanReadBHFSMTA2ShipmentDocumentsPast },
            { WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments, PermissionNames.CanReadQESMTA2ShipmentDocumentsPast },
            { WorklistFromBioHubStatus.WaitForPickUpCompleted, PermissionNames.CanReadWaitForPickUpFromBioHubCompletedPast },
            { WorklistFromBioHubStatus.WaitForDeliveryCompleted, PermissionNames.CanReadWaitForDeliveryFromBioHubCompletedPast },
            { WorklistFromBioHubStatus.WaitForArrivalConditionCheck, PermissionNames.CanReadWaitForArrivalConditionFromBioHubCheckPast },
            { WorklistFromBioHubStatus.WaitForCommentQESendFeedback, PermissionNames.CanReadWaitForCommentQESendFeedbackPast },
            { WorklistFromBioHubStatus.WaitForFinalApproval, PermissionNames.CanReadWaitForFinalApprovalFromBioHubPast },
            { WorklistFromBioHubStatus.ShipmentCompleted, PermissionNames.CanReadShipmentFromBioHubCompletedPast },
        };

        public static readonly Dictionary<WorklistFromBioHubStatus, string> WorklistFromBioHubStatusUpdatePermissionPastMapper = new Dictionary<WorklistFromBioHubStatus, string>()
        {
            { WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2, PermissionNames.CanSubmitAnnex2OfSMTA2Past },
            { WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval, PermissionNames.CanSubmitWaitingForAnnex2OfSMTA2SECsApprovalPast },
            { WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2, PermissionNames.CanSubmitBiosafetyChecklistFormOfSMTA2Past },
            { WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval, PermissionNames.CanSubmitWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast },
            { WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2, PermissionNames.CanSubmitBookingFormOfSMTA2Past },
            { WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval, PermissionNames.CanSubmitWaitForBookingFormSMTA2OPSsApprovalPast },
            { WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments, PermissionNames.CanSubmitBHFSMTA2ShipmentDocumentsPast },
            { WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments, PermissionNames.CanSubmitQESMTA2ShipmentDocumentsPast },
            { WorklistFromBioHubStatus.WaitForPickUpCompleted, PermissionNames.CanSubmitWaitForPickUpFromBioHubCompletedPast },
            { WorklistFromBioHubStatus.WaitForDeliveryCompleted, PermissionNames.CanSubmitWaitForDeliveryFromBioHubCompletedPast },
            { WorklistFromBioHubStatus.WaitForArrivalConditionCheck, PermissionNames.CanSubmitWaitForArrivalConditionFromBioHubCheckPast },
            { WorklistFromBioHubStatus.WaitForCommentQESendFeedback, PermissionNames.CanSubmitWaitForCommentQESendFeedbackPast },
            { WorklistFromBioHubStatus.WaitForFinalApproval, PermissionNames.CanSubmitWaitForFinalApprovalFromBioHubPast },
            { WorklistFromBioHubStatus.ShipmentCompleted, PermissionNames.CanSubmitShipmentFromBioHubCompletedPast },

        };

        public static readonly Dictionary<WorklistFromBioHubStatus, string> WorklistFromBioHubStatusDownloadFilePermissionPastMapper = new Dictionary<WorklistFromBioHubStatus, string>()
        {
            { WorklistFromBioHubStatus.SubmitAnnex2OfSMTA2, PermissionNames.CanDownloadFileSubmitAnnex2OfSMTA2Past },
            { WorklistFromBioHubStatus.WaitingForAnnex2OfSMTA2SECsApproval, PermissionNames.CanDownloadFileWaitingForAnnex2OfSMTA2SECsApprovalPast },
            { WorklistFromBioHubStatus.SubmitBiosafetyChecklistFormOfSMTA2, PermissionNames.CanDownloadFileSubmitBiosafetyChecklistFormOfSMTA2Past },
            { WorklistFromBioHubStatus.WaitForBiosafetyChecklistFormSMTA2BSFsApproval, PermissionNames.CanDownloadFileWaitForBiosafetyChecklistFormSMTA2BSFsApprovalPast },
            { WorklistFromBioHubStatus.SubmitBookingFormOfSMTA2, PermissionNames.CanDownloadFileSubmitBookingFormOfSMTA2Past },
            { WorklistFromBioHubStatus.WaitForBookingFormSMTA2OPSsApproval, PermissionNames.CanDownloadFileWaitForBookingFormSMTA2OPSsApprovalPast },
            { WorklistFromBioHubStatus.SubmitBHFSMTA2ShipmentDocuments, PermissionNames.CanDownloadBHFSMTA2ShipmentDocumentsPast },
            { WorklistFromBioHubStatus.SubmitQESMTA2ShipmentDocuments, PermissionNames.CanDownloadQESMTA2ShipmentDocumentsPast },
            { WorklistFromBioHubStatus.WaitForPickUpCompleted, PermissionNames.CanDownloadFileWaitForPickUpFromBioHubCompletedPast },
            { WorklistFromBioHubStatus.WaitForDeliveryCompleted, PermissionNames.CanDownloadFileWaitForDeliveryFromBioHubCompletedPast },
            { WorklistFromBioHubStatus.WaitForArrivalConditionCheck, PermissionNames.CanDownloadFileWaitForArrivalConditionFromBioHubCheckPast },
            { WorklistFromBioHubStatus.WaitForCommentQESendFeedback, PermissionNames.CanDownloadFileWaitForCommentQESendFeedbackPast },
            { WorklistFromBioHubStatus.WaitForFinalApproval, PermissionNames.CanDownloadFileWaitForFinalApprovalFromBioHubPast },
            { WorklistFromBioHubStatus.ShipmentCompleted, PermissionNames.CanDownloadFileShipmentFromBioHubCompletedPast },
        };

        public static string GetSMTA1WorkflowStatusPermission(SMTA1WorkflowStatus status, PermissionType type, bool? isPast = false)
        {
            switch (type)
            {
                case PermissionType.Read:
                    return isPast == true ? SMTA1WorkflowStatusReadPermissionPastMapper[status] : SMTA1WorkflowStatusReadPermissionMapper[status];

                case PermissionType.Update:
                    return isPast == true ? SMTA1WorkflowStatusUpdatePermissionPastMapper[status] : SMTA1WorkflowStatusUpdatePermissionMapper[status];

                case PermissionType.DownloadFile:
                    return isPast == true ? SMTA1WorkflowStatusDownloadFilePermissionPastMapper[status] : SMTA1WorkflowStatusDownloadFilePermissionMapper[status];

                default:
                    return string.Empty;
            }
        }

        public static string GetSMTA2WorkflowStatusPermission(SMTA2WorkflowStatus status, PermissionType type, bool? isPast = false)
        {
            switch (type)
            {
                case PermissionType.Read:
                    return isPast == true ? SMTA2WorkflowStatusReadPermissionPastMapper[status] : SMTA2WorkflowStatusReadPermissionMapper[status];

                case PermissionType.Update:
                    return isPast == true ? SMTA2WorkflowStatusUpdatePermissionPastMapper[status] : SMTA2WorkflowStatusUpdatePermissionMapper[status];

                case PermissionType.DownloadFile:
                    return isPast == true ? SMTA2WorkflowStatusDownloadFilePermissionPastMapper[status] : SMTA2WorkflowStatusDownloadFilePermissionMapper[status];

                default:
                    return string.Empty;
            }
        }

        public static string GetWorklistToBioHubStatusPermission(WorklistToBioHubStatus status, PermissionType type, bool? isPast = false)
        {
            switch (type)
            {
                case PermissionType.Read:
                    return isPast == true ? WorklistToBioHubStatusReadPermissionPastMapper[status] : WorklistToBioHubStatusReadPermissionMapper[status];

                case PermissionType.Update:
                    return isPast == true ? WorklistToBioHubStatusUpdatePermissionPastMapper[status] : WorklistToBioHubStatusUpdatePermissionMapper[status];

                case PermissionType.DownloadFile:
                    return isPast == true ? WorklistToBioHubStatusDownloadFilePermissionPastMapper[status] : WorklistToBioHubStatusDownloadFilePermissionMapper[status];

                default:
                    return string.Empty;
            }
        }

        public static string GetWorklistFromBioHubStatusPermission(WorklistFromBioHubStatus status, PermissionType type, bool? isPast = false)
        {
            switch (type)
            {
                case PermissionType.Read:
                    return isPast == true ? WorklistFromBioHubStatusReadPermissionPastMapper[status] : WorklistFromBioHubStatusReadPermissionMapper[status];

                case PermissionType.Update:
                    return isPast == true ? WorklistFromBioHubStatusUpdatePermissionPastMapper[status] : WorklistFromBioHubStatusUpdatePermissionMapper[status];

                case PermissionType.DownloadFile:
                    return isPast == true ? WorklistFromBioHubStatusDownloadFilePermissionPastMapper[status] : WorklistFromBioHubStatusDownloadFilePermissionMapper[status];

                default:
                    return string.Empty;
            }
        }
    }


    public enum PermissionType
    {
        Read,
        Update,
        DownloadFile
    }

}
