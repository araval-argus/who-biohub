using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class CollectedSpecimenType : EntityBase
{
    public Guid? SpecimenTypeId { get; set; }
    public virtual SpecimenType SpecimenType { get; set; }
    public Guid? MaterialLaboratoryAnalysisInformationId { get; set; }
    public virtual MaterialLaboratoryAnalysisInformation MaterialLaboratoryAnalysisInformation { get; set; }
}
