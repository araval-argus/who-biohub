using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubHistoryItemAnnex2OfSMTA2Condition : EntityBase
{
    public Guid? WorklistFromBioHubHistoryItemId { get; set; }
    public WorklistFromBioHubHistoryItem WorklistFromBioHubHistoryItem { get; set; }
    public Guid? Annex2OfSMTA2ConditionId { get; set; }
    public Annex2OfSMTA2Condition Annex2OfSMTA2Condition { get; set; }
    public bool? Flag { get; set; }
    public string Comment { get; set; }
}

