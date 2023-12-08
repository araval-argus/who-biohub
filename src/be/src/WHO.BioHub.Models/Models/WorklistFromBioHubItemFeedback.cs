using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubItemFeedback : EntityBase
{
    public string Text { get; set; }
    public DateTime? Date { get; set; }
    public Guid? PostedById { get; set; }
    public User PostedBy { get; set; }
    public Guid? WorklistFromBioHubItemId { get; set; }
    public WorklistFromBioHubItem WorklistFromBioHubItem { get; set; }
}

