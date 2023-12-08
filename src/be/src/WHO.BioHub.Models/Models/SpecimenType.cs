using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class SpecimenType : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<CollectedSpecimenType> CollectedSpecimenTypes { get; set; }
    public IEnumerable<CollectedSpecimenTypeHistory> CollectedSpecimenTypesHistory { get; set; }
    public IEnumerable<MaterialCollectedSpecimenType> MaterialCollectedSpecimenTypes { get; set; }
    public IEnumerable<MaterialCollectedSpecimenTypeHistory> MaterialCollectedSpecimenTypesHistory { get; set; }
}
