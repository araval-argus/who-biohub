using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class Role : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string PublicName { get; set; }
    public RoleType RoleType { get; set; }
    public bool AddToRegistration { get; set; }
    public bool OnBehalfOf { get; set; }
    public virtual ICollection<UserRequest> UserRequests { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
    public virtual ICollection<WorklistToBioHubEmail> WorklistToBioHubEmails { get; set; }
    public virtual ICollection<WorklistFromBioHubEmail> WorklistFromBioHubEmails { get; set; }
    public virtual ICollection<SMTA1WorkflowEmail> SMTA1WorkflowEmails { get; set; }
    public virtual ICollection<SMTA2WorkflowEmail> SMTA2WorkflowEmails { get; set; }
    public virtual ICollection<UserHistory> UsersHistory { get; set; }
}

