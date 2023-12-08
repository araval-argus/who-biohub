using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubItemLaboratoryFocalPoint : EntityBase
{
    public Guid? WorklistFromBioHubItemId { get; set; }
    public WorklistFromBioHubItem WorklistFromBioHubItem { get; set; }
    public Guid? UserId { get; set; }
    public User User { get; set; }
    public string Other { get; set; }

}
