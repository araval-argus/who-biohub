using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class WorklistToBioHubItemBioHubFacilityFocalPoint : EntityBase
{
    public Guid? WorklistToBioHubItemId { get; set; }
    public WorklistToBioHubItem WorklistToBioHubItem { get; set; }
    public Guid? UserId { get; set; }
    public User User { get; set; }
    public string Other { get; set; }

}
