using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class ShipmentViewModel
    {
        public Guid Id { get; set; }
        public string ReferenceNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string StatusOfRequest { get; set; }
        public DateTime? CompletedOn { get; set; }
        public Guid? LaboratoryId { get; set; }
        public Guid? BioHubFacilityId { get; set; }
        public List<ShipmentMaterialViewModel> ShipmentMaterials { get; set; }
        public ShipmentDirection ShipmentDirection { get; set; }
        public Guid? WorklistToBioHubItemId { get; set; }
        public Guid? WorklistFromBioHubItemId { get; set; }
        public List<DocumentItemDto> Documents { get; set; }
        public List<EFormItemDto> EForms { get; set; }
        public bool CanEditReferenceNumber { get; set; }
        public List<BookingFormOfSMTADto> BookingForms { get; set; }

    }
}
