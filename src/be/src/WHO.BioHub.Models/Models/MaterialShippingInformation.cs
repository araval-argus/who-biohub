using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class MaterialShippingInformation : EntityBase
{
    public string MaterialNumber { get; set; }
    public Guid? MaterialProductId { get; set; }
    public virtual MaterialProduct MaterialProduct { get; set; }
    public Guid? TransportCategoryId { get; set; }
    public virtual TransportCategory TransportCategory { get; set; }
    public int? Quantity { get; set; }
    public double? Amount { get; set; }
    public string Condition { get; set; }
    public string AdditionalInformation { get; set; }
    public Guid? WorklistToBioHubItemId { get; set; }
    public virtual WorklistToBioHubItem WorklistToBioHubItem { get; set; }
    public virtual ICollection<MaterialLaboratoryAnalysisInformation> MaterialLaboratoryAnalysisInformation { get; set; }
    public virtual ICollection<MaterialClinicalDetail> MaterialClinicalDetails { get; set; }
}
