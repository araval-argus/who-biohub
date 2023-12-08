using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class MaterialLaboratoryAnalysisInformationDto
    {
        public Guid Id { get; set; }
        public Guid? MaterialShippingInformationId { get; set; }
        public string MaterialNumber { get; set; }
        public DateTime? FreezingDate { get; set; }
        public double? Temperature { get; set; }
        public Guid? UnitOfMeasureId { get; set; }
        public string VirusConcentration { get; set; }
        public string CulturingCellLine { get; set; }
        public int? CulturingPassagesNumber { get; set; }
        public List<Guid?> CollectedSpecimenTypes { get; set; }
        public string TypeOfTransportMedium { get; set; }
        public string BrandOfTransportMedium { get; set; }
        public YesNoOption? GSDUploadedToDatabase { get; set; }
        public Guid? DatabaseUsedForGSDUploadingId { get; set; }       
        public string AccessionNumberInGSDDatabase { get; set; }   

    }
}
