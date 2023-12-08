using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubStatusFlow : EntityBase
{
    public WorklistFromBioHubStatus Status { get; set; }
    public string StatusTitle { get; set; }
    public string WorklistItemApprovedInfo { get; set; }
    public string WorklistItemRejectedInfo { get; set; }
}



