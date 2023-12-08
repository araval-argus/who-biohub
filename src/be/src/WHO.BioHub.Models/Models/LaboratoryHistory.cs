﻿using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class LaboratoryHistory : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Abbreviation { get; set; }
    public string Address { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public bool IsActive { get; set; }
    public Guid? BSLLevelId { get; set; }
    public virtual BSLLevel? BSLLevel { get; set; }
    public Guid? CountryId { get; set; }
    public virtual Country Country { get; set; }
    public bool IsPublicFacing { get; set; }
    public Guid? LastOperationByUserId { get; set; }
    public DateTime? LastOperationDate { get; set; }
    public Guid? LaboratoryId { get; set; }
    public virtual Laboratory Laboratory { get; set; }
}
