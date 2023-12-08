using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class MaterialClinicalDetailDto
    {
        public Guid Id { get; set; }
        public Guid? MaterialShippingInformationId { get; set; }
        public string MaterialNumber { get; set; }
        public Guid? MaterialProductId { get; set; }
        public Guid? TransportCategoryId { get; set; }
        public DateTime? CollectionDate { get; set; }
        public string Location { get; set; }
        public Guid? IsolationHostTypeId { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
        public string PatientStatus { get; set; }
        public ShipmentMaterialCondition? Condition { get; set; }
        public string Note { get; set; }

    }
}
