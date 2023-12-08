using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class MaterialLaboratoryAnalysisInformationHistory : EntityBase
{
    public Guid? MaterialShippingInformationHistoryId { get; set; }
    public virtual MaterialShippingInformationHistory MaterialShippingInformationHistory { get; set; }
    public string MaterialNumber { get; set; }
    public DateTime? FreezingDate { get; set; }
    public double? Temperature { get; set; }
    public Guid? UnitOfMeasureId { get; set; }
    public virtual TemperatureUnitOfMeasure UnitOfMeasure { get; set; }
    public string VirusConcentration { get; set; }
    public string CulturingCellLine { get; set; }
    public int? CulturingPassagesNumber { get; set; }

    public IEnumerable<CollectedSpecimenTypeHistory> CollectedSpecimenTypes { get; set; }

    public string TypeOfTransportMedium { get; set; }
    public string BrandOfTransportMedium { get; set; }

    public YesNoOption? GSDUploadedToDatabase { get; set; }

    public Guid? DatabaseUsedForGSDUploadingId { get; set; }
    public virtual GeneticSequenceData DatabaseUsedForGSDUploading { get; set; }

    public string AccessionNumberInGSDDatabase { get; set; }
}
