using WHO.BioHub.Models.Models.Abstractions;

namespace WHO.BioHub.Models.Models;

public class UserHistory : EntityBase
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Email { get; set; }
    public string BusinessPhone { get; set; }
    public string MobilePhone { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; }
    public Guid? RoleId { get; set; }
    public virtual Role Role { get; set; }
    public bool OperationalFocalPoint { get; set; }    
    public Guid? LaboratoryId { get; set; }  
    public Laboratory Laboratory { get; set; }
    public Guid? BioHubFacilityId { get; set; }  
    public virtual BioHubFacility BioHubFacility { get; set; }
    public Guid? CourierId { get; set; }
    public virtual Courier Courier { get; set; }
    public Guid? LastOperationByUserId { get; set; }    
    public DateTime? LastOperationDate { get; set; }
    public Guid? ExternalId { get; set; }

    public Guid? UserId { get; set; }
    public virtual User User { get; set; }
}
