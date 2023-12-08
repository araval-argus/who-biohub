namespace WHO.BioHub.Shared.Dto
{
    public class MaterialShippingInformationDto
    {
        public Guid Id { get; set; }
        public string MaterialNumber { get; set; }
        public Guid? MaterialProductId { get; set; }
        public Guid? TransportCategoryId { get; set; }
        public int? Quantity { get; set; }
        public double? Amount { get; set; }
        public string Condition { get; set; }
        public string AdditionalInformation { get; set; }
        public virtual List<MaterialLaboratoryAnalysisInformationDto> MaterialLaboratoryAnalysisInformation { get; set; }
        public virtual List<MaterialClinicalDetailDto> MaterialClinicalDetails { get; set; }
    }
    
}
