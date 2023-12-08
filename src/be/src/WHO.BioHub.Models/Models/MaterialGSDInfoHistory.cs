using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class MaterialGSDInfoHistory : EntityBase
{
    public Guid? MaterialHistoryId { get; set; }
    public virtual MaterialHistory MaterialHistory { get; set; }
    public GSDType? GSDType { get; set; }
    public string CellLine { get; set; }
    public int? PassageNumber { get; set; }
    public string GSDFasta { get; set; }
}
