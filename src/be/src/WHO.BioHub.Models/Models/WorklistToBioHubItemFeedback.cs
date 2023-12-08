using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class WorklistToBioHubItemFeedback : EntityBase
{
    public string Text { get; set; }
    public DateTime? Date { get; set; }
    public Guid? PostedById { get; set; }
    public User PostedBy { get; set; }
    public Guid? WorklistToBioHubItemId { get; set; }
    public WorklistToBioHubItem WorklistToBioHubItem { get; set; }
}

