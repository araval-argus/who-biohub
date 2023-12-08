using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class WorklistToBioHubHistoryItemFeedback : EntityBase
{
    public string Text { get; set; }
    public DateTime? Date { get; set; }
    public Guid? PostedById { get; set; }
    public User PostedBy { get; set; }
    public Guid? WorklistToBioHubHistoryItemId { get; set; }
    public WorklistToBioHubHistoryItem WorklistToBioHubHistoryItem { get; set; }
}

