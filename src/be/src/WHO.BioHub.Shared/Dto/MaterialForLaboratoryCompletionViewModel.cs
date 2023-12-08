using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Shared.Dto
{
    public class MaterialForLaboratoryCompletionViewModel
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
        public MaterialValidationSelection? ReferenceNumberValidation { get; set; }
        public MaterialValidationSelection? NameValidation { get; set; }
        public MaterialValidationSelection? DescriptionValidation { get; set; }
        public MaterialValidationSelection? TypeValidation { get; set; }
        public MaterialValidationSelection? SuspectedEpidemiologicalOriginValidation { get; set; }
        public MaterialValidationSelection? OriginalProductTypeValidation { get; set; }
        public MaterialValidationSelection? TransportCategoryValidation { get; set; }
        public MaterialValidationSelection? TemperatureValidation { get; set; }
        public MaterialValidationSelection? UnitOfMeasureValidation { get; set; }
        public MaterialValidationSelection? UsagePermissionValidation { get; set; }
        public MaterialValidationSelection? SampleIdValidation { get; set; }
        public MaterialValidationSelection? LineageValidation { get; set; }
        public MaterialValidationSelection? VariantValidation { get; set; }
        public MaterialValidationSelection? VariantAssessmentValidation { get; set; }
        public MaterialValidationSelection? StrainDesignationValidation { get; set; }
        public MaterialValidationSelection? GenotypeValidation { get; set; }
        public MaterialValidationSelection? SerotypeValidation { get; set; }
        public MaterialValidationSelection? GeneticSequenceDataValidation { get; set; }
        public MaterialValidationSelection? DatabaseAccessionIdValidation { get; set; }
        public MaterialValidationSelection? OriginalGeneticSequenceValidation { get; set; }
        public MaterialValidationSelection? FacilityGSDValidation { get; set; }
        public MaterialValidationSelection? InternationalTaxonomyClassificationValidation { get; set; }
        public MaterialValidationSelection? GMOValidation { get; set; }
        public MaterialValidationSelection? IsolationHostTypeValidation { get; set; }
        public MaterialValidationSelection? CultivabilityTypeValidation { get; set; }
        //public MaterialValidationSelection? ProductionCellLineValidation { get; set; }
        public MaterialValidationSelection? IsolationTechniqueTypeValidation { get; set; }
        public MaterialValidationSelection? InfectivityValidation { get; set; }
        public MaterialValidationSelection? ViralTiterValidation { get; set; }
        public MaterialValidationSelection? CollectionDateValidation { get; set; }
        public MaterialValidationSelection? LocationValidation { get; set; }
        public MaterialValidationSelection? GenderValidation { get; set; }
        public MaterialValidationSelection? AgeValidation { get; set; }
        public MaterialValidationSelection? PatientStatusValidation { get; set; }
        public string ReferenceNumberComment { get; set; }
        public string NameComment { get; set; }
        public string DescriptionComment { get; set; }
        public string TypeComment { get; set; }
        public string SuspectedEpidemiologicalOriginComment { get; set; }
        public string OriginalProductTypeComment { get; set; }
        public string TransportCategoryComment { get; set; }
        public string TemperatureComment { get; set; }
        public string UnitOfMeasureComment { get; set; }
        public string UsagePermissionComment { get; set; }
        public string SampleIdComment { get; set; }
        public string LineageComment { get; set; }
        public string VariantComment { get; set; }
        public string VariantAssessmentComment { get; set; }
        public string StrainDesignationComment { get; set; }
        public string GenotypeComment { get; set; }
        public string SerotypeComment { get; set; }
        public string GeneticSequenceDataComment { get; set; }
        public string DatabaseAccessionIdComment { get; set; }
        public string OriginalGeneticSequenceComment { get; set; }
        public string FacilityGSDComment { get; set; }
        public string InternationalTaxonomyClassificationComment { get; set; }
        public string GMOComment { get; set; }
        public string IsolationHostTypeComment { get; set; }
        public string CultivabilityTypeComment { get; set; }
        //public string ProductionCellLineComment { get; set; }
        public string IsolationTechniqueTypeComment { get; set; }
        public string InfectivityComment { get; set; }
        public string ViralTiterComment { get; set; }
        public string CollectionDateComment { get; set; }
        public string LocationComment { get; set; }
        public string GenderComment { get; set; }
        public string AgeComment { get; set; }
        public string PatientStatusComment { get; set; }

        public DateTime? DateOfBMEPPReceipt { get; set; }

        public Guid? ProductTypeId { get; set; }
        public Readiness? BHFShareReadiness { get; set; }
        public YesNoOption? PublicShare { get; set; }

        public MaterialValidationSelection? GSDCulturedMaterialCellLine3Validation { get; set; }
        public MaterialValidationSelection? ProductTypeValidation { get; set; }


        public string ProductTypeComment { get; set; }


        ////
        ///
        public string Pathogen { get; set; }
        public MaterialValidationSelection? PathogenValidation { get; set; }
        public string PathogenComment { get; set; }
        public DateTime? FreezingDate { get; set; }
        public string VirusConcentration { get; set; }
        public MaterialValidationSelection? FreezingDateValidation { get; set; }
        public MaterialValidationSelection? VirusConcentrationValidation { get; set; }
        public string FreezingDateComment { get; set; }
        public string VirusConcentrationComment { get; set; }

        public MaterialValidationSelection? OwnerBioHubFacilityValidation { get; set; }
        public string OwnerBioHubFacilityComment { get; set; }

        public double? ShipmentAmount { get; set; }
        public double? ShipmentTemperature { get; set; }
        public Guid? ShipmentUnitOfMeasureId { get; set; }
        public string CulturingCellLine { get; set; }
        public int? CulturingPassagesNumber { get; set; }
        public string TypeOfTransportMedium { get; set; }
        public string BrandOfTransportMedium { get; set; }
        public List<Guid?> MaterialCollectedSpecimenTypes { get; set; }
        public DatabaseUploadedBy? DatabaseUploadedBy { get; set; }
        public MaterialValidationSelection? ShipmentAmountValidation { get; set; }
        public MaterialValidationSelection? ShipmentTemperatureValidation { get; set; }
        public MaterialValidationSelection? ShipmentUnitOfMeasureValidation { get; set; }
        public MaterialValidationSelection? CulturingCellLineValidation { get; set; }
        public MaterialValidationSelection? CulturingPassagesNumberValidation { get; set; }
        public MaterialValidationSelection? TypeOfTransportMediumValidation { get; set; }
        public MaterialValidationSelection? BrandOfTransportMediumValidation { get; set; }
        public MaterialValidationSelection? MaterialCollectedSpecimenTypesValidation { get; set; }
        public MaterialValidationSelection? DatabaseUploadedByValidation { get; set; }

        public string ShipmentAmountComment { get; set; }
        public string ShipmentTemperatureComment { get; set; }
        public string ShipmentUnitOfMeasureComment { get; set; }
        public string CulturingCellLineComment { get; set; }
        public string CulturingPassagesNumberComment { get; set; }
        public string TypeOfTransportMediumComment { get; set; }
        public string BrandOfTransportMediumComment { get; set; }
        public string MaterialCollectedSpecimenTypesComment { get; set; }
        public string DatabaseUploadedByComment { get; set; }


        public ResultType? CulturingResult { get; set; }
        public DateTime? CulturingResultDate { get; set; }
        public ResultType? QualityControlResult { get; set; }
        public DateTime? QualityControlResultDate { get; set; }
        public ResultType? GSDAnalysisResult { get; set; }
        public DateTime? GSDAnalysisResultDate { get; set; }
        public GSDUploadingStatus? GSDUploadingStatus { get; set; }
        public DateTime? GSDUploadingDate { get; set; }

        public MaterialValidationSelection? CulturingResultValidation { get; set; }
        public MaterialValidationSelection? CulturingResultDateValidation { get; set; }
        public MaterialValidationSelection? QualityControlResultValidation { get; set; }
        public MaterialValidationSelection? QualityControlResultDateValidation { get; set; }
        public MaterialValidationSelection? GSDAnalysisResultValidation { get; set; }
        public MaterialValidationSelection? GSDAnalysisResultDateValidation { get; set; }
        public MaterialValidationSelection? GSDUploadingStatusValidation { get; set; }
        public MaterialValidationSelection? GSDUploadingDateValidation { get; set; }

        public string CulturingResultComment { get; set; }
        public string CulturingResultDateComment { get; set; }
        public string QualityControlResultComment { get; set; }
        public string QualityControlResultDateComment { get; set; }
        public string GSDAnalysisResultComment { get; set; }
        public string GSDAnalysisResultDateComment { get; set; }
        public string GSDUploadingStatusComment { get; set; }
        public string GSDUploadingDateComment { get; set; }


        public MaterialValidationSelection? DateOfBMEPPReceiptValidation { get; set; }
        public string DateOfBMEPPReceiptComment { get; set; }

        public MaterialValidationSelection? ShipmentNumberOfVialsValidation { get; set; }
        public string ShipmentNumberOfVialsComment { get; set; }

        public DateTime? LastAliquotsAdditionDate { get; set; }

        public List<MaterialGSDInfoDto> MaterialGSDInfo { get; set; }
        public MaterialValidationSelection? MaterialGSDInfoValidation { get; set; }
        public string MaterialGSDInfoComment { get; set; }
        public bool SharedWithQE { get; set; }
        public ShipmentMaterialCondition? ShipmentMaterialCondition { get; set; }
        public string ShipmentMaterialConditionNote { get; set; }
        public MaterialValidationSelection? ShipmentMaterialConditionValidation { get; set; }
        public string ShipmentMaterialConditionComment { get; set; }
    }
}
