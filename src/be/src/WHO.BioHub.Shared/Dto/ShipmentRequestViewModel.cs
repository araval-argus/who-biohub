using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class ShipmentRequestViewModel
    {
        public Guid Id { get; set; }
        public string WorklistItemTitle { get; set; }
        public DateTime? OperationDate { get; set; }
        public string SendBy { get; set; }
        public string Institution { get; set; }
        public ShipmentDirection ShipmentDirection { get; set; }
        public string LaboratoryCountryIso { get; set; }
        public string CurrentStatusName { get; set; }

    }
}
