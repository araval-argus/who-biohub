using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class BookingFormOfSMTADto
    {
        public Guid Id { get; set; }
        public Guid? WorklistItemId { get; set; }        
        public Guid? TransportCategoryId { get; set; }
        public string TransportCategoryName { get; set; }
        public string TransportCategoryDescription { get; set; }
        public Guid? TransportModeId { get; set; }
        public string TransportModeName { get; set; }
        public string TransportModeDescription { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? RequestDateOfPickup { get; set; }
        public TemperatureTransportCondition? TemperatureTransportCondition { get; set; }
        public int? TotalNumberOfVials { get; set; }
        public decimal? TotalAmount { get; set; }
        public string NumberOfInnerPackagingAndSize { get; set; }
        public virtual ICollection<WorklistItemUserDto> BookingFormPickupUsers { get; set; }
        public virtual ICollection<WorklistItemUserDto> BookingFormCourierUsers { get; set; }
        public Guid? CourierId { get; set; }
        public DateTime? EstimateDateOfPickup { get; set; }
        public DateTime? DateOfPickup { get; set; }
        public string ShipmentReferenceNumber { get; set; }
        public DateTime? DateOfDelivery { get; set; }
    }
}
