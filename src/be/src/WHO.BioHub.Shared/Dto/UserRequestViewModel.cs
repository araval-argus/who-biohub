using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class UserRequestViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Purpose { get; set; }
        public UserRegistrationStatus Status { get; set; }
        public bool? TermsAndConditionAccepted { get; set; }
        public DateTime? RequestDate { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? CountryId { get; set; }
        public string Message { get; set; }
        public bool IsConfirmed { get; set; }
        public string InstituteName { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public Guid? LaboratoryId { get; set; }
        public string RecaptchaResponse { get; set; }

    }
}
