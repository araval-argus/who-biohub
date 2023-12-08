namespace WHO.BioHub.Shared.Dto
{
    public class Annex2OfSMTA1DataViewModel
    {
        public string ShipmentRequestNumber { get; set; }
        public Guid? OriginalDocumentTemplateAnnex2OfSMTA1DocumentId { get; set; }        
        public Guid? SMTA1DocumentId { get; set; }
        public string OriginalDocumentTemplateAnnex2OfSMTA1DocumentName { get; set; }
        public string SMTA1DocumentName { get; set; }        
        public string Annex2OfSMTA1SignatureText { get; set; }
        public string Annex2Comment { get; set; }
        public bool? Annex2TermsAndConditions { get; set; }
        public Guid? LaboratoryId { get; set; }
        public Guid? BioHubFacilityId { get; set; }
        public string WHODocumentRegistrationNumber { get; set; }
        public string LaboratoryName { get; set; }
        public string LaboratoryAddress { get; set; }
        public string LaboratoryCountry { get; set; }
        public string BioHubFacilityName { get; set; }
        public string BioHubFacilityAddress { get; set; }
        public string BioHubFacilityCountry { get; set; }
        public IEnumerable<MaterialShippingInformationDto> MaterialShippingInformations { get; set; }
        public IEnumerable<WorklistItemUserDto> LaboratoryFocalPoints { get; set; }
        public string Annex2ApprovalComment { get; set; }
        public DateTime? ApprovalDate { get; set; }

    }


}
