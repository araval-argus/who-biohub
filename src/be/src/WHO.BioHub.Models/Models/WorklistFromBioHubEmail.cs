using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class WorklistFromBioHubEmail : EntityBase
{
    public WorklistFromBioHubStatus FromStatus { get; set; }
    public WorklistFromBioHubStatus ToStatus { get; set; }
    public bool ApprovedSubmission { get; set; }
    public string EmailSubject { get; set; }
    public string EmailBody { get; set; }
    public Guid? RoleId { get; set; }
    public virtual Role Role { get; set; }
    public bool IsCourier { get; set; }
    public bool IsNumberOfVialsWarning { get; set; }
}



