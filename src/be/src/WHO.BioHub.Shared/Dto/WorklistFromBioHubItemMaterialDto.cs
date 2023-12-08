﻿using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class WorklistFromBioHubItemMaterialDto
    {
        public Guid Id { get; set; }
        public Guid WorklistFromBioHubItemId { get; set; }
        public Guid? MaterialId { get; set; }
        public int? Quantity { get; set; }
        public int? AvailableQuantity { get; set; }
        public double? Amount { get; set; }
        public string MaterialNumber { get; set; }
        public Guid? MaterialProductId { get; set; }
        public Guid? TransportCategoryId { get; set; }
        public string MaterialName { get; set; }
        public DateTime? CollectionDate { get; set; }
        public string Location { get; set; }
        public Guid? IsolationHostTypeId { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
        public ShipmentMaterialCondition? Condition { get; set; }
        public string Note { get; set; }
        public MaterialStatus Status { get; set; }
    }
}
