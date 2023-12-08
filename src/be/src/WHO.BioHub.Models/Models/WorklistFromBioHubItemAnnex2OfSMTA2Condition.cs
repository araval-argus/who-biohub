using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubItemAnnex2OfSMTA2Condition : EntityBase
{
    public Guid? WorklistFromBioHubItemId { get; set; }
    public WorklistFromBioHubItem WorklistFromBioHubItem { get; set; }
    public Guid? Annex2OfSMTA2ConditionId { get; set; }
    public Annex2OfSMTA2Condition Annex2OfSMTA2Condition { get; set; }
    public bool? Flag { get; set; }
    public string Comment { get; set; }
}

