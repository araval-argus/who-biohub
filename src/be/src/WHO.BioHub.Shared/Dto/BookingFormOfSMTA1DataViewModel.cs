namespace WHO.BioHub.Shared.Dto
{
    public class BookingFormOfSMTA1DataViewModel
    {
        public string ShipmentRequestNumber { get; set; }
        public Guid? OriginalDocumentTemplateBookingFormOfSMTA1DocumentId { get; set; }
        public Guid? SMTA1DocumentId { get; set; }
        public string OriginalDocumentTemplateBookingFormOfSMTA1DocumentName { get; set; }
        public string SMTA1DocumentName { get; set; }
        public string BookingFormOfSMTA1SignatureText { get; set; }
        public IEnumerable<BookingFormOfSMTADto> BookingForms { get; set; }
        public IEnumerable<MaterialShippingInformationDto> MaterialShippingInformations { get; set; }
        public string RequestUserFirstName { get; set; }
        public string RequestUserLastName { get; set; }
        public string RequestUserEmail { get; set; }
        public string RequestUserJobTitle { get; set; }
        public string RequestUserBusinessPhone { get; set; }
        public string RequestUserMobilePhone { get; set; }
        public string LaboratoryName { get; set; }
        public string LaboratoryAddress { get; set; }
        public string LaboratoryCountry { get; set; }
        public string BioHubFacilityName { get; set; }
        public string BioHubFacilityAddress { get; set; }
        public string BioHubFacilityCountry { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public IEnumerable<WorklistItemUserDto> BioHubFacilityFocalPoints  { get; set; }
        public List<CourierViewModel> Couriers { get; set; }


    }


}
