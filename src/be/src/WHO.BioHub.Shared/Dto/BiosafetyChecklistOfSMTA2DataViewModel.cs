namespace WHO.BioHub.Shared.Dto
{
    public class BiosafetyChecklistOfSMTA2DataViewModel
    {
        public string ShipmentRequestNumber { get; set; }
        public Guid? OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentId { get; set; }        
        public Guid? SMTA2DocumentId { get; set; }
        public string OriginalDocumentTemplateBiosafetyChecklistOfSMTA2DocumentName { get; set; }
        public string SMTA2DocumentName { get; set; }        
        public string BiosafetyChecklistOfSMTA2SignatureText { get; set; }
        public string BiosafetyChecklistComment { get; set; }   
        public Guid? LaboratoryId { get; set; }
        public Guid? BioHubFacilityId { get; set; } 
        public string LaboratoryName { get; set; }
        public string LaboratoryAddress { get; set; }
        public string LaboratoryCountry { get; set; }
        public string BioHubFacilityName { get; set; }
        public string BioHubFacilityAddress { get; set; }
        public string BioHubFacilityCountry { get; set; }       
        public IEnumerable<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Dto> WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s { get; set; }
        public IEnumerable<BiosafetyChecklistThreadCommentDto> BiosafetyChecklistThreadComments { get; set; }
        public string BiosafetyChecklistApprovalComment { get; set; }
        public DateTime? ApprovalDate { get; set; }

    }


}
