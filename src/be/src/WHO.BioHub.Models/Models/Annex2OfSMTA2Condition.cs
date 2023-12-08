using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class Annex2OfSMTA2Condition : EntityBase
{
    public int Order { get; set; }
    public string PointNumber { get; set; }
    public string Condition { get; set; }
    public bool Current { get; set; }
    public bool Mandatory { get; set; }
    public bool Selectable { get; set; }
    public bool? FlagPresetValue { get; set; }
    public virtual ICollection<WorklistFromBioHubItemAnnex2OfSMTA2Condition> WorklistFromBioHubItemAnnex2OfSMTA2Conditions { get; set; }

    public virtual ICollection<WorklistFromBioHubHistoryItemAnnex2OfSMTA2Condition> WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions { get; set; }

}


