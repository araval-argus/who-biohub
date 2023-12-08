namespace WHO.BioHub.Shared.Dto
{
    public class BookingFormEmailInfoDto
    {
        public string TransportCategoryName { get; set; }
        public string TransportCategoryDescription { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? RequestDateOfPickup { get; set; }
        public string TemperatureTransportCondition { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string NumberOfInnerPackagingAndSize { get; set; }
        public virtual ICollection<ContactUserInfoForEmailDto> BookingFormPickupUsers { get; set; }
        public virtual ICollection<ContactUserInfoForEmailDto> BookingFormCourierUsers { get; set; }
        public virtual ICollection<ContactUserInfoForEmailDto> BookingFormDeliveryUsers { get; set; }
        public string CourierEmail { get; set; }
        public DateTime? EstimateDateOfPickup { get; set; }
        public DateTime? DateOfPickup { get; set; }
        public DateTime? DateOfDelivery { get; set; }
        public string ShipmentReferenceNumber { get; set; }
    }

}
