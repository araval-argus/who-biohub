using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class WorkflowEmailInfoDto
    {
        public Guid Id { get; set; }
        public string LaboratoryName { get; set; }
        public string LaboratoryAbbreviation { get; set; }
        public string LaboratoryAddress { get; set; }
        public string LaboratoryCountry { get; set; }
        public string BioHubFacilityName { get; set; }
        public string BioHubFacilityCountry { get; set; }
        public string BioHubFacilityUserFirstName { get; set; }
        public string BioHubFacilityUserLastName { get; set; }
        public string BioHubFacilityUserEmail { get; set; }

        public string BioHubFacilityUserBusinessPhone { get; set; }
        public string BioHubFacilityUserMobilePhone { get; set; }
        public string BioHubFacilityUserJobTitle { get; set; }
        public string BioHubFacilityUserSignature { get; set; }

        public string BioHubFacilityAddress { get; set; }
        public string LaboratoryUserName { get; set; }
        public string LaboratoryUserRoleName { get; set; }
        public string LaboratoryUserRoleTypeName { get; set; }
        public RoleType LaboratoryUserRoleType { get; set; }
        public string LaboratoryUserBusinessPhone { get; set; }
        public string LaboratoryUserMobilePhone { get; set; }
        public string LaboratoryUserFirstName { get; set; }
        public string LaboratoryUserLastName { get; set; }
        public string LaboratoryUserEmail { get; set; }
        public string LaboratoryUserJobTitle { get; set; }
        public string LaboratoryUserSignature { get; set; }
        public string WHOOperationalFocalPointName { get; set; }
        public string WHOOperationalFocalPointLastname { get; set; }
        public string WHOOperationalFocalPointEmail { get; set; }
        public Guid WHOOperationalFocalPointRoleId { get; set; }
        public string Comment { get; set; }
        public List<ContactUserInfoForEmailDto> LaboratoryFocalPoints { get; set; }
        public List<BookingFormEmailInfoDto> BookingForms { get; set; }
        public List<ShipmentDocumentInfoForEmailDto> ShipmentDocuments { get; set; }
        public FeedbackDto Feedback { get; set; }
        public string WaitForArrivalConditionCheckApprovalComment { get; set; }
        public string WHOAccountNumber { get; set; }
        public List<ContactUserInfoForEmailDto> BioHubFacilityFocalPoints { get; set; }
        public List<MaterialsCurrentNumberOfVialsInfo> MaterialsCurrentNumberOfInfo { get; set; }

    }

}
