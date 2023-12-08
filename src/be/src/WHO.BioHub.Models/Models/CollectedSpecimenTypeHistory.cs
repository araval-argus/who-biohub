using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class CollectedSpecimenTypeHistory : EntityBase
{
    public Guid? SpecimenTypeId { get; set; }
    public virtual SpecimenType SpecimenType { get; set; }
    public Guid? MaterialLaboratoryAnalysisInformationHistoryId { get; set; }
    public virtual MaterialLaboratoryAnalysisInformationHistory MaterialLaboratoryAnalysisInformationHistory { get; set; }
}
