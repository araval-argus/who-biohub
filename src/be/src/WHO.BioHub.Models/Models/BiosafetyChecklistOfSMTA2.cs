using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class BiosafetyChecklistOfSMTA2 : EntityBase
{
    public int Order { get; set; }
    public string Condition { get; set; }
    public bool? FlagPresetValue { get; set; }
    public bool Current { get; set; }
    public bool Mandatory { get; set; }
    public bool Selectable { get; set; }
    public bool IsParentCondition { get; set; }
    public Guid? ParentConditionId { get; set; }
    public bool ShowOnParentValue { get; set; }

    public virtual ICollection<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2> WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s { get; set; }

    public virtual ICollection<WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2> WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s { get; set; }

}


