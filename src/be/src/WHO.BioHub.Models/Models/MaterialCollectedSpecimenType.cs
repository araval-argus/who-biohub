using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class MaterialCollectedSpecimenType : EntityBase
{
    public Guid? SpecimenTypeId { get; set; }
    public virtual SpecimenType SpecimenType { get; set; }
    public Guid? MaterialId { get; set; }
    public virtual Material Material { get; set; }
}




