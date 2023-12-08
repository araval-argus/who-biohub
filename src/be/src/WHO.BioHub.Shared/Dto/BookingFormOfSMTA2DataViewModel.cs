namespace WHO.BioHub.Shared.Dto
{
    public class BookingFormOfSMTA2DataViewModel
    {
        public string ShipmentRequestNumber { get; set; }
        public Guid? OriginalDocumentTemplateBookingFormOfSMTA2DocumentId { get; set; }
        public Guid? SMTA2DocumentId { get; set; }
        public string OriginalDocumentTemplateBookingFormOfSMTA2DocumentName { get; set; }
        public string SMTA2DocumentName { get; set; }
        public string BookingFormOfSMTA2SignatureText { get; set; }
        public IEnumerable<BookingFormOfSMTADto> BookingForms { get; set; }
        public IEnumerable<WorklistFromBioHubItemMaterialDto> WorklistFromBioHubItemMaterials { get; set; }
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
        public IEnumerable<WorklistItemUserDto> LaboratoryFocalPoints  { get; set; }
        public List<CourierViewModel> Couriers { get; set; } 


    }


}
