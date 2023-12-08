using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class WorklistToBioHubHistoryItemLaboratoryFocalPoint : EntityBase
{
    public Guid? WorklistToBioHubHistoryItemId { get; set; }
    public WorklistToBioHubHistoryItem WorklistToBioHubHistoryItem { get; set; }
    public Guid? UserId { get; set; }
    public User User { get; set; }
    public string Other { get; set; }

}
