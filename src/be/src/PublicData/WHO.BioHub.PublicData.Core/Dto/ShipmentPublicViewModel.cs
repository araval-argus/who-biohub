using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.PublicData.Core.Dto
{
    public class ShipmentPublicViewModel
    {
        public Guid Id { get; set; }
        public string ReferenceNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string StatusOfRequest { get; set; }
        public DateTime? CompletedOn { get; set; }
        public Guid? LaboratoryId { get; set; }
        public Guid? BioHubFacilityId { get; set; }
        public List<ShipmentPublicMaterialViewModel> ShipmentMaterials { get; set; }
        public ShipmentDirection ShipmentDirection { get; set; }
    }

}
