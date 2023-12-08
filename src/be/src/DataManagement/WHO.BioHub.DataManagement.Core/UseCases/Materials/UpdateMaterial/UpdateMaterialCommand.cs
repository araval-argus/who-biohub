using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.UpdateMaterial;

public record struct UpdateMaterialCommand(Guid Id,
    string ReferenceNumber,
    string Name,
    string Description,
    double? Temperature,
    string SampleId,
    string Lineage,
    string Variant,
    string VariantAssessment,
    string StrainDesignation,
    string Genotype,
    string Serotype,
    string DatabaseAccessionId,
    string OriginalGeneticSequence,
    string FacilityGSD,
    bool GMO,
    //string ProductionCellLine,
    string Infectivity,
    string ViralTiter,
bool IsNew,
    Guid? TypeId,
    Guid? SuspectedEpidemiologicalOriginId,
    Guid? OriginalProductTypeId,
    Guid? TransportCategoryId,
    Guid? UnitOfMeasureId,
    Guid? UsagePermissionId,
    Guid? GeneticSequenceDataId,
    Guid? InternationalTaxonomyClassificationId,
    Guid? IsolationHostTypeId,
    Guid? CultivabilityTypeId,
    Guid? IsolationTechniqueTypeId,
    Guid? ProviderLaboratoryId,
    Guid? ProviderBioHubFacilityId,
    DateTime? CollectionDate,
    string Location,
    string PatientStatus,
    int? Age,
    Gender? Gender,
    Guid? UserId,
    Guid? ReferenceId,
    IEnumerable<string> UserPermissions,
    Guid? OwnerBioHubFacilityId,
    int? NumberOfVialsToAdd,
    int? CurrentNumberOfVials,
    int? WarningEmailCurrentNumberOfVialsThreshold,
    Guid? ProductTypeId,
    string Pathogen,
    DateTime? FreezingDate,
    string VirusConcentration,
    int? ShipmentNumberOfVials,
    double? ShipmentAmount,
    double? ShipmentTemperature,
    Guid? ShipmentUnitOfMeasureId,
    string CulturingCellLine,
    int? CulturingPassagesNumber,
    string TypeOfTransportMedium,
    string BrandOfTransportMedium,
    List<Guid?> MaterialCollectedSpecimenTypes,
    DatabaseUploadedBy? DatabaseUploadedBy,

    DateTime? DateOfBMEPPReceipt,

     ResultType? CulturingResult,
     DateTime? CulturingResultDate,
     ResultType? QualityControlResult,
     DateTime? QualityControlResultDate,
     ResultType? GSDAnalysisResult,
     DateTime? GSDAnalysisResultDate,
     GSDUploadingStatus? GSDUploadingStatus,
     DateTime? GSDUploadingDate,

     List<MaterialGSDInfoDto> MaterialGSDInfo,

    DateTime? LastAliquotsAdditionDate,

    Readiness? BHFShareReadiness,
    YesNoOption? PublicShare,
    ShipmentMaterialCondition? ShipmentMaterialCondition,
    string ShipmentMaterialConditionNote);