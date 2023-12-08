using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class MaterialCollectedSpecimenTypeHistory : EntityBase
{
    public Guid? SpecimenTypeId { get; set; }
    public virtual SpecimenType SpecimenType { get; set; }
    public Guid? MaterialHistoryId { get; set; }
    public virtual MaterialHistory MaterialHistory { get; set; }
}




