using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class ShipmentMaterialViewModel
    {
        public Guid Id { get; set; }
        public Guid? MaterialId { get; set; }
        public string MaterialNumber { get; set; }
        public Guid? MaterialProductId { get; set; }
        public string MaterialName { get; set; }
        public DateTime? CollectionDate { get; set; }
        public string Location { get; set; }
        public Guid? IsolationHostTypeId { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
        public MaterialStatus Status { get; set; }
        public ShipmentMaterialCondition? ShipmentMaterialCondition { get; set; }
    }
}
