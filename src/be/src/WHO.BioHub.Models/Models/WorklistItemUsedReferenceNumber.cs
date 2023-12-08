using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class WorklistItemUsedReferenceNumber : EntityBase
{
    public string ReferenceNumber { get; set; }
    public bool? IsPast { get; set; }
    //public ICollection<WorklistToBioHubItem> WorklistToBioHubItems { get; set; }
    //public ICollection<WorklistFromBioHubItem> WorklistFromBioHubItems { get; set; }
}

