namespace WHO.BioHub.Shared.Worklists
{
    public static class WorklistEventNames
    {
        public const string SMTA1Request = "SMTA 1 Request";
        public const string SubmitSMTA1 = "SMTA 1 Submission";
        public const string ReturnedSMTA1 = "Returned SMTA 1";
        public const string ReSubmitSMTA1 = "SMTA 1 Re-Submission";
        public const string ApprovedSMTA1 = "Approved SMTA 1";
        public const string SMTA1Completed = "SMTA 1 Completed";


        public const string SMTA2Request = "SMTA 2 Request";
        public const string SubmitSMTA2 = "SMTA 2 Submission";
        public const string ReturnedSMTA2 = "Returned SMTA 2";
        public const string ReSubmitSMTA2 = "SMTA 2 Re-Submission";
        public const string ApprovedSMTA2 = "Approved SMTA 2";
        public const string SMTA2Completed = "SMTA 2 Completed";

        public const string ShipmentRequest = "Shipment Request";
        public const string SubmitAnnex2OfSMTA1 = "Annex 2 Submission";
        public const string ReturnedAnnex2OfSMTA1 = "Returned Annex 2";
        public const string ReSubmitAnnex2OfSMTA1 = "Annex 2 Re-Submission";
        public const string ApprovedAnnex2OfSMTA1 = "Approved Annex 2";
        public const string SubmitBookingFormOfSMTA1 = "Booking Form Submission";
        public const string ReturnedBookingFormOfSMTA1 = "Returned Booking Form";
        public const string ReSubmitBookingFormOfSMTA1 = "Booking Form Re-Submission";
        public const string ApprovedBookingFormOfSMTA1 = "Approved Booking Form";

        public const string SubmitAnnex2OfSMTA2 = "Annex 2 Submission";
        public const string ReturnedAnnex2OfSMTA2 = "Returned Annex 2";
        public const string ReSubmitAnnex2OfSMTA2 = "Annex 2 Re-Submission";
        public const string ApprovedAnnex2OfSMTA2 = "Approved Annex 2";
        public const string SubmitBookingFormOfSMTA2 = "Booking Form Submission";
        public const string ReturnedBookingFormOfSMTA2 = "Returned Booking Form";
        public const string ReSubmitBookingFormOfSMTA2 = "Booking Form Re-Submission";
        public const string ApprovedBookingFormOfSMTA2 = "Approved Booking Form";



        public const string SubmitBiosafetyChecklist = "Biosafety Checklist Submission";
        public const string ReturnedBiosafetyChecklist = "Returned Biosafety Checklist";
        public const string ReSubmitBiosafetyChecklist = "Biosafety Checklist Re-Submission";
        public const string ApprovedBiosafetyChecklist = "Approved Biosafety Checklist";




        public const string PickupCompleted = "Pickup Completed";
        public const string DeliveryCompleted = "Delivery Completed";
        public const string ShipmentCompleted = "Shipment Completed";

        public const string PickupDate = "Pickup Date";
        public const string DeliveryDate = "Delivery Date";


        public const string MaterialCreation = "Creation";
        public const string NumberOfVialsProduced = "Number of Vials produced";
        public const string CulturingResult = "Culturing Result";
        public const string QualityControlResult = "Quality Control Result";
        public const string GSDAnalysisResult = "GSD Analysis Result";
        public const string GSDUploadingStatus = "GSD Uploading Status";


    }

    public static class WorklistEventPositionNames
    {
        public const string Up = "up";
        public const string Down = "down";
    }

    public static class WorklistEventStageNames
    {
        public const string SMTA1 = "smta1";
        public const string SMTA2 = "smta2";
        public const string PreShipment = "stage-pre-shipment";
        public const string Shipment = "stage-shipment";
        public const string PostShipment = "stage-post-shipment";
    }
}
