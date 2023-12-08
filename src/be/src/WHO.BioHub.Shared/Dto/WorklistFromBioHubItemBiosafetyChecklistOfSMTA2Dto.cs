namespace WHO.BioHub.Shared.Dto
{
    public class WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto
    {
        public Guid Id { get; set; }
        public Guid WorklistFromBioHubItemId { get; set; }
        public Guid? BiosafetyChecklistId { get; set; }
        public bool? Flag { get; set; }
        public bool Selectable { get; set; }
        public int Order { get; set; }
        public string Condition { get; set; }
        public bool Mandatory { get; set; }
        public bool IsParentCondition { get; set; }
        public Guid? ParentConditionId { get; set; }
        public bool ShowOnParentValue { get; set; }
        public bool IsVisible { get; set; }
    }
}
