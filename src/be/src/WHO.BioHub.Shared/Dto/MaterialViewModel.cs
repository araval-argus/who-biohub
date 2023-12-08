using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class MaterialViewModel
    {
        public Guid Id { get; set; }
        public string ReferenceNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? TypeId { get; set; }
        public Guid? SuspectedEpidemiologicalOriginId { get; set; }
        public Guid? OriginalProductTypeId { get; set; }
        public Guid? TransportCategoryId { get; set; }
        public double? Temperature { get; set; }
        public Guid? UnitOfMeasureId { get; set; }
        public Guid? UsagePermissionId { get; set; }
        public string SampleId { get; set; }
        public string Lineage { get; set; }
        public string Variant { get; set; }
        public string VariantAssessment { get; set; }
        public string StrainDesignation { get; set; }
        public string Genotype { get; set; }
        public string Serotype { get; set; }
        public Guid? GeneticSequenceDataId { get; set; }
        public string DatabaseAccessionId { get; set; }
        public string OriginalGeneticSequence { get; set; }
        public string FacilityGSD { get; set; }
        public Guid? InternationalTaxonomyClassificationId { get; set; }
        public bool GMO { get; set; }
        public Guid? IsolationHostTypeId { get; set; }
        public Guid? CultivabilityTypeId { get; set; }
        //public string ProductionCellLine { get; set; }
        public Guid? IsolationTechniqueTypeId { get; set; }
        public string Infectivity { get; set; }
        public string ViralTiter { get; set; }

        public Guid? ProviderLaboratoryId { get; set; }
        public Guid? ProviderBioHubFacilityId { get; set; }
        public DateTime? CollectionDate { get; set; }
        public string Location { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
        public string PatientStatus { get; set; }
        public MaterialStatus Status { get; set; }
        public Guid? ReferenceId { get; set; }

        public Guid? OwnerBioHubFacilityId { get; set; }
        public int? ShipmentNumberOfVials { get; set; }
        public int? CurrentNumberOfVials { get; set; }
        public int? WarningEmailCurrentNumberOfVialsThreshold { get; set; }
        public bool ShipmentInformationVisible { get; set; }

        public DateTime? DateOfBMEPPReceipt { get; set; }

        public Guid? ProductTypeId { get; set; }

        public Readiness? BHFShareReadiness { get; set; }

        public YesNoOption? PublicShare { get; set; }

        public string Pathogen { get; set; }
        public DateTime? FreezingDate { get; set; }
        public string VirusConcentration { get; set; }
        public double? ShipmentAmount { get; set; }
        public double? ShipmentTemperature { get; set; }
        public Guid? ShipmentUnitOfMeasureId { get; set; }
        public string CulturingCellLine { get; set; }
        public int? CulturingPassagesNumber { get; set; }
        public string TypeOfTransportMedium { get; set; }
        public string BrandOfTransportMedium { get; set; }
        public List<Guid?> MaterialCollectedSpecimenTypes { get; set; }
        public DatabaseUploadedBy? DatabaseUploadedBy { get; set; }

        public ResultType? CulturingResult { get; set; }
        public DateTime? CulturingResultDate { get; set; }
        public ResultType? QualityControlResult { get; set; }
        public DateTime? QualityControlResultDate { get; set; }
        public ResultType? GSDAnalysisResult { get; set; }
        public DateTime? GSDAnalysisResultDate { get; set; }
        public GSDUploadingStatus? GSDUploadingStatus { get; set; }
        public DateTime? GSDUploadingDate { get; set; }


        public DateTime? LastAliquotsAdditionDate { get; set; }

        public List<MaterialGSDInfoDto> MaterialGSDInfo { get; set; }

        public bool SharedWithQE { get; set; }

        public ShipmentMaterialCondition? ShipmentMaterialCondition { get; set; }
        public string ShipmentMaterialConditionNote { get; set; }
    }
}
