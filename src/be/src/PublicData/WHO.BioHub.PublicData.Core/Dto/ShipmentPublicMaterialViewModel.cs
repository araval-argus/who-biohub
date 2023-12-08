using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.PublicData.Core.Dto
{
    public class ShipmentPublicMaterialViewModel
    {
        public Guid Id { get; set; }
        public Guid? MaterialId { get; set; }
        public string MaterialNumber { get; set; }
        public Guid? MaterialProductId { get; set; }
        public string MaterialName { get; set; }
        public Guid? ProviderId { get; set; }
    }

}
