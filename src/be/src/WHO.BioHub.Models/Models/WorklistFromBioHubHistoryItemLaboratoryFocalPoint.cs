using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubHistoryItemLaboratoryFocalPoint : EntityBase
{
    public Guid? WorklistFromBioHubHistoryItemId { get; set; }
    public WorklistFromBioHubHistoryItem WorklistFromBioHubHistoryItem { get; set; }
    public Guid? UserId { get; set; }
    public User User { get; set; }
    public string Other { get; set; }

}
