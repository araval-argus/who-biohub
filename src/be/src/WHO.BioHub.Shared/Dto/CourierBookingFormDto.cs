using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class CourierBookingFormDto
    {
        public Guid Id { get; set; }
        public Guid? WorklistToBioHubItemId { get; set; }
        public Guid? WorklistFromBioHubItemId { get; set; }
        public string WorklistReferenceNumber { get; set; }
        public string ShipmentDirection { get; set; }
        public Guid? TransportCategoryId { get; set; }
        public string TransportCategoryName { get; set; }
        public string TransportCategoryDescription { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? RequestDateOfPickup { get; set; }
        public TemperatureTransportCondition? TemperatureTransportCondition { get; set; }
        public string NumberOfVialsAndMls { get; set; }
        public string NumberOfInnerPackagingAndSize { get; set; }
        public virtual IEnumerable<CourierBookingFormUserDto> BookingFormPickupUsers { get; set; }
        public virtual IEnumerable<CourierBookingFormUserDto> BookingFormCourierUsers { get; set; }
        public virtual IEnumerable<CourierBookingFormUserDto> BookingFormLaboratoryFocalPoints { get; set; }
        public virtual IEnumerable<CourierBookingFormUserDto> BookingFormBioHubFacilityFocalPoints { get; set; }
        public Guid? CourierId { get; set; }
        public DateTime? EstimateDateOfPickup { get; set; }
        public DateTime? DateOfPickup { get; set; }
        public string ShipmentReferenceNumber { get; set; }
        public DateTime? DateOfDelivery { get; set; }
        public string RequestingUserBusinessPhone { get; set; }
        public string RequestingUserMobilePhone { get; set; }
        public string RequestingUserFirstName { get; set; }
        public string RequestingUserLastName { get; set; }
        public string RequestingUserEmail { get; set; }
        public string RequestingUserJobTitle { get; set; }
        public string LaboratoryName { get; set; }
        public string LaboratoryAbbreviation { get; set; }
        public string LaboratoryAddress { get; set; }
        public string LaboratoryCountry { get; set; }
        public string BioHubFacilityName { get; set; }
        public string BioHubFacilityCountry { get; set; }
        public string BioHubFacilityAddress { get; set; }
        public List<MaterialShippingInformationDto> MaterialShippingInformations { get; set; }
        public List<WorklistFromBioHubItemMaterialDto> WorklistFromBioHubItemMaterials { get; set; }
        public int? TotalNumberOfVials { get; set; }
        public decimal? TotalAmount { get; set; }
        public string TransportMode { get; set; }        
    }
}
