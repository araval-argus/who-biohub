using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.UpdateMaterialForLaboratoryCompletion;

public interface IUpdateMaterialForLaboratoryCompletionMapper
{
    Material MapApprove(Material material, UpdateMaterialForLaboratoryCompletionCommand command);
    Material MapFields(Material material, UpdateMaterialForLaboratoryCompletionCommand command);
    Material MapValidations(Material material, UpdateMaterialForLaboratoryCompletionCommand command);
}

public class UpdateMaterialForLaboratoryCompletionMapper : IUpdateMaterialForLaboratoryCompletionMapper
{
    public Material MapFields(Material material, UpdateMaterialForLaboratoryCompletionCommand command)
    {

        material.Id = command.Id;
        material.ReferenceNumber = command.ReferenceNumber;
        material.Name = command.Name;
        material.Description = command.Description;
        material.Temperature = command.Temperature;
        material.SampleId = command.SampleId;
        material.Lineage = command.Lineage;
        material.Variant = command.Variant;
        material.VariantAssessment = command.VariantAssessment;
        material.StrainDesignation = command.StrainDesignation;
        material.Genotype = command.Genotype;
        material.Serotype = command.Serotype;
        material.DatabaseAccessionId = command.DatabaseAccessionId;
        material.OriginalGeneticSequence = command.OriginalGeneticSequence;
        material.FacilityGSD = command.FacilityGSD;
        material.GMO = command.GMO;
        material.Infectivity = command.Infectivity;
        material.ViralTiter = command.ViralTiter;
        material.IsNew = command.IsNew;
        material.TypeId = command.TypeId;
        material.SuspectedEpidemiologicalOriginId = command.SuspectedEpidemiologicalOriginId;
        material.UnitOfMeasureId = command.UnitOfMeasureId;
        material.UsagePermissionId = command.UsagePermissionId;
        material.GeneticSequenceDataId = command.GeneticSequenceDataId;
        material.InternationalTaxonomyClassificationId = command.InternationalTaxonomyClassificationId;
        material.IsolationHostTypeId = command.IsolationHostTypeId;
        material.CultivabilityTypeId = command.CultivabilityTypeId;
        material.IsolationTechniqueTypeId = command.IsolationTechniqueTypeId;
        material.DeletedOn = null;
        material.WarningEmailCurrentNumberOfVialsThreshold = command.WarningEmailCurrentNumberOfVialsThreshold;
        material.Pathogen = command.Pathogen;
        material.DatabaseUploadedBy = command.DatabaseUploadedBy;
        material.CulturingResult = command.CulturingResult;
        material.CulturingResultDate = command.CulturingResultDate;
        material.QualityControlResult = command.QualityControlResult;
        material.QualityControlResultDate = command.QualityControlResultDate;
        material.GSDAnalysisResult = command.GSDAnalysisResult;
        material.GSDAnalysisResultDate = command.GSDAnalysisResultDate;
        material.GSDUploadingStatus = command.GSDUploadingStatus;
        material.GSDUploadingDate = command.GSDUploadingDate;
        material.BHFShareReadiness = command.BHFShareReadiness;
        material.PublicShare = command.PublicShare;

        if (command.UserPermissions.Contains(PermissionNames.CanEditMaterialShipmentInformation))
        {
            //Shipment Information            
            material.TransportCategoryId = command.TransportCategoryId;
            material.OriginalProductTypeId = command.OriginalProductTypeId;
            material.ProviderBioHubFacilityId = command.ProviderBioHubFacilityId;
            material.ProviderLaboratoryId = command.ProviderLaboratoryId;
            material.Age = command.Age;
            material.PatientStatus = command.PatientStatus;
            material.CollectionDate = command.CollectionDate;
            material.Location = command.Location;
            material.Gender = command.Gender;
            material.FreezingDate = command.FreezingDate;
            material.VirusConcentration = command.VirusConcentration;
            material.ShipmentNumberOfVials = command.ShipmentNumberOfVials;
            material.ShipmentAmount = command.ShipmentAmount;
            material.ShipmentTemperature = command.ShipmentTemperature;
            material.ShipmentUnitOfMeasureId = command.ShipmentUnitOfMeasureId;
            material.CulturingCellLine = command.CulturingCellLine;
            material.CulturingPassagesNumber = command.CulturingPassagesNumber;
            material.TypeOfTransportMedium = command.TypeOfTransportMedium;
            material.BrandOfTransportMedium = command.BrandOfTransportMedium;
            material.ShipmentMaterialCondition = command.ShipmentMaterialCondition;
            material.ShipmentMaterialConditionNote = command.ShipmentMaterialConditionNote;
            material.ProductTypeId = command.ProductTypeId;
        }

        return material;
    }

    public Material MapValidations(Material material, UpdateMaterialForLaboratoryCompletionCommand command)
    {
        material.ReferenceNumberValidation = command.ReferenceNumberValidation;
        material.NameValidation = command.NameValidation;
        material.DescriptionValidation = command.DescriptionValidation;
        material.TypeValidation = command.TypeValidation;
        material.SuspectedEpidemiologicalOriginValidation = command.SuspectedEpidemiologicalOriginValidation;
        material.OriginalProductTypeValidation = command.OriginalProductTypeValidation;
        material.TransportCategoryValidation = command.TransportCategoryValidation;
        material.TemperatureValidation = command.TemperatureValidation;
        material.UnitOfMeasureValidation = command.UnitOfMeasureValidation;
        material.UsagePermissionValidation = command.UsagePermissionValidation;
        material.SampleIdValidation = command.SampleIdValidation;
        material.LineageValidation = command.LineageValidation;
        material.VariantValidation = command.VariantValidation;
        material.VariantAssessmentValidation = command.VariantAssessmentValidation;
        material.StrainDesignationValidation = command.StrainDesignationValidation;
        material.GenotypeValidation = command.GenotypeValidation;
        material.SerotypeValidation = command.SerotypeValidation;
        material.GeneticSequenceDataValidation = command.GeneticSequenceDataValidation;
        material.DatabaseAccessionIdValidation = command.DatabaseAccessionIdValidation;
        material.OriginalGeneticSequenceValidation = command.OriginalGeneticSequenceValidation;
        material.FacilityGSDValidation = command.FacilityGSDValidation;
        material.InternationalTaxonomyClassificationValidation = command.InternationalTaxonomyClassificationValidation;
        material.GMOValidation = command.GMOValidation;
        material.IsolationHostTypeValidation = command.IsolationHostTypeValidation;
        material.CultivabilityTypeValidation = command.CultivabilityTypeValidation;
        material.IsolationTechniqueTypeValidation = command.IsolationTechniqueTypeValidation;
        material.InfectivityValidation = command.InfectivityValidation;
        material.ViralTiterValidation = command.ViralTiterValidation;
        material.CollectionDateValidation = command.CollectionDateValidation;
        material.LocationValidation = command.LocationValidation;
        material.GenderValidation = command.GenderValidation;
        material.AgeValidation = command.AgeValidation;
        material.PatientStatusValidation = command.PatientStatusValidation;
        material.ReferenceNumberComment = command.ReferenceNumberComment;
        material.NameComment = command.NameComment;
        material.DescriptionComment = command.DescriptionComment;
        material.TypeComment = command.TypeComment;
        material.SuspectedEpidemiologicalOriginComment = command.SuspectedEpidemiologicalOriginComment;
        material.OriginalProductTypeComment = command.OriginalProductTypeComment;
        material.TransportCategoryComment = command.TransportCategoryComment;
        material.TemperatureComment = command.TemperatureComment;
        material.UnitOfMeasureComment = command.UnitOfMeasureComment;
        material.UsagePermissionComment = command.UsagePermissionComment;
        material.SampleIdComment = command.SampleIdComment;
        material.LineageComment = command.LineageComment;
        material.VariantComment = command.VariantComment;
        material.VariantAssessmentComment = command.VariantAssessmentComment;
        material.StrainDesignationComment = command.StrainDesignationComment;
        material.GenotypeComment = command.GenotypeComment;
        material.SerotypeComment = command.SerotypeComment;
        material.GeneticSequenceDataComment = command.GeneticSequenceDataComment;
        material.DatabaseAccessionIdComment = command.DatabaseAccessionIdComment;
        material.OriginalGeneticSequenceComment = command.OriginalGeneticSequenceComment;
        material.FacilityGSDComment = command.FacilityGSDComment;
        material.InternationalTaxonomyClassificationComment = command.InternationalTaxonomyClassificationComment;
        material.GMOComment = command.GMOComment;
        material.IsolationHostTypeComment = command.IsolationHostTypeComment;
        material.CultivabilityTypeComment = command.CultivabilityTypeComment;
        material.IsolationTechniqueTypeComment = command.IsolationTechniqueTypeComment;
        material.InfectivityComment = command.InfectivityComment;
        material.ViralTiterComment = command.ViralTiterComment;
        material.CollectionDateComment = command.CollectionDateComment;
        material.LocationComment = command.LocationComment;
        material.GenderComment = command.GenderComment;
        material.AgeComment = command.AgeComment;
        material.PatientStatusComment = command.PatientStatusComment;
        material.ProductTypeValidation = command.ProductTypeValidation;
        material.ProductTypeComment = command.ProductTypeComment;
        material.ShipmentAmountValidation = command.ShipmentAmountValidation;
        material.ShipmentTemperatureValidation = command.ShipmentTemperatureValidation;
        material.ShipmentUnitOfMeasureValidation = command.ShipmentUnitOfMeasureValidation;
        material.CulturingCellLineValidation = command.CulturingCellLineValidation;
        material.CulturingPassagesNumberValidation = command.CulturingPassagesNumberValidation;
        material.TypeOfTransportMediumValidation = command.TypeOfTransportMediumValidation;
        material.BrandOfTransportMediumValidation = command.BrandOfTransportMediumValidation;
        material.DatabaseUploadedByValidation = command.DatabaseUploadedByValidation;
        material.ShipmentNumberOfVialsValidation = command.ShipmentNumberOfVialsValidation;
        material.ShipmentAmountValidation = command.ShipmentAmountValidation;
        material.ShipmentTemperatureValidation = command.ShipmentTemperatureValidation;
        material.ShipmentUnitOfMeasureValidation = command.ShipmentUnitOfMeasureValidation;
        material.CulturingCellLineValidation = command.CulturingCellLineValidation;
        material.CulturingPassagesNumberValidation = command.CulturingPassagesNumberValidation;
        material.TypeOfTransportMediumValidation = command.TypeOfTransportMediumValidation;
        material.BrandOfTransportMediumValidation = command.BrandOfTransportMediumValidation;
        material.MaterialCollectedSpecimenTypesValidation = command.MaterialCollectedSpecimenTypesValidation;
        material.DatabaseUploadedByValidation = command.DatabaseUploadedByValidation;
        material.ShipmentNumberOfVialsComment = command.ShipmentNumberOfVialsComment;
        material.ShipmentAmountComment = command.ShipmentAmountComment;
        material.ShipmentTemperatureComment = command.ShipmentTemperatureComment;
        material.ShipmentUnitOfMeasureComment = command.ShipmentUnitOfMeasureComment;
        material.CulturingCellLineComment = command.CulturingCellLineComment;
        material.CulturingPassagesNumberComment = command.CulturingPassagesNumberComment;
        material.TypeOfTransportMediumComment = command.TypeOfTransportMediumComment;
        material.BrandOfTransportMediumComment = command.BrandOfTransportMediumComment;
        material.MaterialCollectedSpecimenTypesComment = command.MaterialCollectedSpecimenTypesComment;
        material.DatabaseUploadedByComment = command.DatabaseUploadedByComment;
        material.CulturingResultValidation = command.CulturingResultValidation;
        material.CulturingResultDateValidation = command.CulturingResultDateValidation;
        material.QualityControlResultValidation = command.QualityControlResultValidation;
        material.QualityControlResultDateValidation = command.QualityControlResultDateValidation;
        material.GSDAnalysisResultValidation = command.GSDAnalysisResultValidation;
        material.GSDAnalysisResultDateValidation = command.GSDAnalysisResultDateValidation;
        material.GSDUploadingStatusValidation = command.GSDUploadingStatusValidation;
        material.GSDUploadingDateValidation = command.GSDUploadingDateValidation;
        material.CulturingResultComment = command.CulturingResultComment;
        material.CulturingResultDateComment = command.CulturingResultDateComment;
        material.QualityControlResultComment = command.QualityControlResultComment;
        material.QualityControlResultDateComment = command.QualityControlResultDateComment;
        material.GSDAnalysisResultComment = command.GSDAnalysisResultComment;
        material.GSDAnalysisResultDateComment = command.GSDAnalysisResultDateComment;
        material.GSDUploadingStatusComment = command.GSDUploadingStatusComment;
        material.GSDUploadingDateComment = command.GSDUploadingDateComment;
        material.MaterialGSDInfoValidation = command.MaterialGSDInfoValidation;
        material.MaterialGSDInfoComment = command.MaterialGSDInfoComment;
        material.PathogenValidation = command.PathogenValidation;
        material.PathogenComment = command.PathogenComment;
        material.OwnerBioHubFacilityValidation = command.OwnerBioHubFacilityValidation;
        material.OwnerBioHubFacilityComment = command.OwnerBioHubFacilityComment;
        material.FreezingDateValidation = command.FreezingDateValidation;
        material.FreezingDateComment = command.FreezingDateComment;
        material.VirusConcentrationValidation = command.VirusConcentrationValidation;
        material.VirusConcentrationComment = command.VirusConcentrationComment;
        material.DateOfBMEPPReceiptValidation = command.DateOfBMEPPReceiptValidation;
        material.DateOfBMEPPReceiptComment = command.DateOfBMEPPReceiptComment;
        material.ShipmentMaterialConditionValidation = command.ShipmentMaterialConditionValidation;
        material.ShipmentMaterialConditionComment = command.ShipmentMaterialConditionComment;

        return material;
    }

    public Material MapApprove(Material material, UpdateMaterialForLaboratoryCompletionCommand command)
    {
        material.Status = command.Approve == true ? MaterialStatus.Completed : material.Status;
        return material;
    }
}