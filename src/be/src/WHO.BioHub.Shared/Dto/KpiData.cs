namespace WHO.BioHub.Shared.Dto
{
    public class KpiData
    {
        public decimal AverageDaysBetweenRequestAndPickup { get; set; }
        public decimal AverageDaysBetweenRequestAndSMTASigning { get; set; }
        public decimal AverageDaysBetweenRequestAndBookingFormSigning { get; set; }
        public decimal AverageDaysBetweenBookingFormCourierReceiptAndPickup { get; set; }
        public decimal AverageTotalTransportDaysOfSamples { get; set; }
        public decimal AverageTotalDaysFromRequestToDelivery { get; set; }
        public decimal AverageGSDUploadingTime { get; set; }
    }
}
