using WHO.BioHub.Models.Models.Abstractions;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Models;

public class UserRequestStatus : EntityBase
{
    public string Message { get; set; }
    public string Subject { get; set; }
    public bool IsResponseMessage { get; set; }
    public UserRegistrationStatus Status { get; set; }
}
