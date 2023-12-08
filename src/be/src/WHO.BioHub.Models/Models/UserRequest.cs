using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class UserRequest : EntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Purpose { get; set; }
    public UserRegistrationStatus Status { get; set; }
    public bool? TermsAndConditionAccepted { get; set; }
    public Guid? RoleId { get; set; }
    public virtual Role Role { get; set; }
    public Guid? CountryId { get; set; }
    public virtual Country Country { get; set; }
    public string Message { get; set; }
    public bool IsConfirmed { get; set; }
    public string InstituteName { get; set; }
    public DateTime? ConfirmationDate { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public Guid? LaboratoryId { get; set; }
    public virtual Laboratory Laboratory { get; set; }
}


