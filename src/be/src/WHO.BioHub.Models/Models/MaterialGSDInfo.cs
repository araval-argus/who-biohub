using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class MaterialGSDInfo : EntityBase
{
    public Guid? MaterialId { get; set; }
    public virtual Material Material { get; set; }
    public GSDType? GSDType { get; set; }
    public string CellLine { get; set; }
    public int? PassageNumber { get; set; }
    public string GSDFasta { get; set; }   
}
