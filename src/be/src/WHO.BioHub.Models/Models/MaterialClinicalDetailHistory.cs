﻿using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class MaterialClinicalDetailHistory : EntityBase
{
    public Guid? MaterialShippingInformationHistoryId { get; set; }
    public virtual MaterialShippingInformationHistory MaterialShippingInformationHistory { get; set; }
    public string MaterialNumber { get; set; }
    public DateTime? CollectionDate { get; set; }
    public string Location { get; set; }
    public Guid? IsolationHostTypeId { get; set; }
    public virtual IsolationHostType IsolationHostType { get; set; }
    public Gender? Gender { get; set; }
    public int? Age { get; set; }
    public string PatientStatus { get; set; }
    public ShipmentMaterialCondition? Condition { get; set; }
    public string Note { get; set; }

}
