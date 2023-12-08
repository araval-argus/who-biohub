using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterialForLaboratoryCompletion;

public interface IReadMaterialForLaboratoryCompletionMapper
{
    MaterialForLaboratoryCompletionViewModel Map(Material material);
}

public class ReadMaterialForLaboratoryCompletionMapper : IReadMaterialForLaboratoryCompletionMapper
{
    public MaterialForLaboratoryCompletionViewModel Map(Material material)
    {

        MaterialForLaboratoryCompletionViewModel materialViewModel = new MaterialForLaboratoryCompletionViewModel();

        materialViewModel.Id = material.Id;
        materialViewModel.ReferenceNumber = material.ReferenceNumber;
        materialViewModel.Name = material.Name;
        materialViewModel.Description = material.Description;
        materialViewModel.Temperature = material.Temperature;
        materialViewModel.SampleId = material.SampleId;
        materialViewModel.Lineage = material.Lineage;
        materialViewModel.Variant = material.Variant;
        materialViewModel.VariantAssessment = material.VariantAssessment;
        materialViewModel.StrainDesignation = material.StrainDesignation;
        materialViewModel.Genotype = material.Genotype;
        materialViewModel.Serotype = material.Serotype;
        materialViewModel.DatabaseAccessionId = material.DatabaseAccessionId;
        materialViewModel.OriginalGeneticSequence = material.OriginalGeneticSequence;
        materialViewModel.FacilityGSD = material.FacilityGSD;
        materialViewModel.GMO = material.GMO;
        materialViewModel.Infectivity = material.Infectivity;
        materialViewModel.ViralTiter = material.ViralTiter;
        materialViewModel.TypeId = material.TypeId;
        materialViewModel.SuspectedEpidemiologicalOriginId = material.SuspectedEpidemiologicalOriginId;
        materialViewModel.OriginalProductTypeId = material.OriginalProductTypeId;
        materialViewModel.TransportCategoryId = material.TransportCategoryId;
        materialViewModel.UnitOfMeasureId = material.UnitOfMeasureId;
        materialViewModel.UsagePermissionId = material.UsagePermissionId;
        materialViewModel.GeneticSequenceDataId = material.GeneticSequenceDataId;
        materialViewModel.InternationalTaxonomyClassificationId = material.InternationalTaxonomyClassificationId;
        materialViewModel.IsolationHostTypeId = material.IsolationHostTypeId;
        materialViewModel.CultivabilityTypeId = material.CultivabilityTypeId;
        materialViewModel.IsolationTechniqueTypeId = material.IsolationTechniqueTypeId;

        materialViewModel.ProviderBioHubFacilityId = material.ProviderBioHubFacilityId;
        materialViewModel.ProviderLaboratoryId = material.ProviderLaboratoryId;
        materialViewModel.Age = material.Age;
        materialViewModel.PatientStatus = material.PatientStatus;
        materialViewModel.CollectionDate = material.CollectionDate;
        materialViewModel.Location = material.Location;
        materialViewModel.Gender = material.Gender;
        materialViewModel.Status = material.Status;
        materialViewModel.ReferenceId = material.ReferenceId;
        materialViewModel.OwnerBioHubFacilityId = material.OwnerBioHubFacilityId;
        materialViewModel.ShipmentNumberOfVials = material.ShipmentNumberOfVials;
        materialViewModel.CurrentNumberOfVials = material.CurrentNumberOfVials;
        materialViewModel.WarningEmailCurrentNumberOfVialsThreshold = material.WarningEmailCurrentNumberOfVialsThreshold;
        materialViewModel.ShipmentInformationVisible = !material.ManualCreation;
        materialViewModel.ReferenceNumberValidation = material.ReferenceNumberValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.NameValidation = material.NameValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.DescriptionValidation = material.DescriptionValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.TypeValidation = material.TypeValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.SuspectedEpidemiologicalOriginValidation = material.SuspectedEpidemiologicalOriginValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.OriginalProductTypeValidation = material.OriginalProductTypeValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.TransportCategoryValidation = material.TransportCategoryValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.TemperatureValidation = material.TemperatureValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.UnitOfMeasureValidation = material.UnitOfMeasureValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.UsagePermissionValidation = material.UsagePermissionValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.SampleIdValidation = material.SampleIdValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.LineageValidation = material.LineageValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.VariantValidation = material.VariantValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.VariantAssessmentValidation = material.VariantAssessmentValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.StrainDesignationValidation = material.StrainDesignationValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.GenotypeValidation = material.GenotypeValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.SerotypeValidation = material.SerotypeValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.GeneticSequenceDataValidation = material.GeneticSequenceDataValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.DatabaseAccessionIdValidation = material.DatabaseAccessionIdValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.OriginalGeneticSequenceValidation = material.OriginalGeneticSequenceValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.FacilityGSDValidation = material.FacilityGSDValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.InternationalTaxonomyClassificationValidation = material.InternationalTaxonomyClassificationValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.GMOValidation = material.GMOValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.IsolationHostTypeValidation = material.IsolationHostTypeValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.CultivabilityTypeValidation = material.CultivabilityTypeValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.IsolationTechniqueTypeValidation = material.IsolationTechniqueTypeValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.InfectivityValidation = material.InfectivityValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.ViralTiterValidation = material.ViralTiterValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.CollectionDateValidation = material.CollectionDateValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.LocationValidation = material.LocationValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.GenderValidation = material.GenderValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.AgeValidation = material.AgeValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.PatientStatusValidation = material.PatientStatusValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.ReferenceNumberComment = material.ReferenceNumberComment;
        materialViewModel.NameComment = material.NameComment;
        materialViewModel.DescriptionComment = material.DescriptionComment;
        materialViewModel.TypeComment = material.TypeComment;
        materialViewModel.SuspectedEpidemiologicalOriginComment = material.SuspectedEpidemiologicalOriginComment;
        materialViewModel.OriginalProductTypeComment = material.OriginalProductTypeComment;
        materialViewModel.TransportCategoryComment = material.TransportCategoryComment;
        materialViewModel.TemperatureComment = material.TemperatureComment;
        materialViewModel.UnitOfMeasureComment = material.UnitOfMeasureComment;
        materialViewModel.UsagePermissionComment = material.UsagePermissionComment;
        materialViewModel.SampleIdComment = material.SampleIdComment;
        materialViewModel.LineageComment = material.LineageComment;
        materialViewModel.VariantComment = material.VariantComment;
        materialViewModel.VariantAssessmentComment = material.VariantAssessmentComment;
        materialViewModel.StrainDesignationComment = material.StrainDesignationComment;
        materialViewModel.GenotypeComment = material.GenotypeComment;
        materialViewModel.SerotypeComment = material.SerotypeComment;
        materialViewModel.GeneticSequenceDataComment = material.GeneticSequenceDataComment;
        materialViewModel.DatabaseAccessionIdComment = material.DatabaseAccessionIdComment;
        materialViewModel.OriginalGeneticSequenceComment = material.OriginalGeneticSequenceComment;
        materialViewModel.FacilityGSDComment = material.FacilityGSDComment;
        materialViewModel.InternationalTaxonomyClassificationComment = material.InternationalTaxonomyClassificationComment;
        materialViewModel.GMOComment = material.GMOComment;
        materialViewModel.IsolationHostTypeComment = material.IsolationHostTypeComment;
        materialViewModel.CultivabilityTypeComment = material.CultivabilityTypeComment;
        materialViewModel.IsolationTechniqueTypeComment = material.IsolationTechniqueTypeComment;
        materialViewModel.InfectivityComment = material.InfectivityComment;
        materialViewModel.ViralTiterComment = material.ViralTiterComment;
        materialViewModel.CollectionDateComment = material.CollectionDateComment;
        materialViewModel.LocationComment = material.LocationComment;
        materialViewModel.GenderComment = material.GenderComment;
        materialViewModel.AgeComment = material.AgeComment;
        materialViewModel.PatientStatusComment = material.PatientStatusComment;
        materialViewModel.DateOfBMEPPReceipt = material.DateOfBMEPPReceipt;
        materialViewModel.ProductTypeId = material.ProductTypeId;
        materialViewModel.BHFShareReadiness = material.BHFShareReadiness;
        materialViewModel.PublicShare = material.PublicShare;
        materialViewModel.ProductTypeValidation = material.ProductTypeValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.ProductTypeComment = material.ProductTypeComment;
        materialViewModel.Pathogen = material.Pathogen;
        materialViewModel.FreezingDate = material.FreezingDate;
        materialViewModel.VirusConcentration = material.VirusConcentration;
        materialViewModel.ShipmentAmount = material.ShipmentAmount;
        materialViewModel.ShipmentTemperature = material.ShipmentTemperature;
        materialViewModel.ShipmentUnitOfMeasureId = material.ShipmentUnitOfMeasureId;
        materialViewModel.CulturingCellLine = material.CulturingCellLine;
        materialViewModel.CulturingPassagesNumber = material.CulturingPassagesNumber;
        materialViewModel.TypeOfTransportMedium = material.TypeOfTransportMedium;
        materialViewModel.BrandOfTransportMedium = material.BrandOfTransportMedium;
        materialViewModel.MaterialCollectedSpecimenTypes = material.MaterialCollectedSpecimenTypes.Select(x => x.SpecimenTypeId).ToList();
        materialViewModel.DatabaseUploadedBy = material.DatabaseUploadedBy;
        materialViewModel.PathogenValidation = material.PathogenValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.FreezingDateValidation = material.FreezingDateValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.VirusConcentrationValidation = material.VirusConcentrationValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.ShipmentAmountValidation = material.ShipmentAmountValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.ShipmentTemperatureValidation = material.ShipmentTemperatureValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.ShipmentUnitOfMeasureValidation = material.ShipmentUnitOfMeasureValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.CulturingCellLineValidation = material.CulturingCellLineValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.CulturingPassagesNumberValidation = material.CulturingPassagesNumberValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.TypeOfTransportMediumValidation = material.TypeOfTransportMediumValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.BrandOfTransportMediumValidation = material.BrandOfTransportMediumValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.MaterialCollectedSpecimenTypesValidation = material.MaterialCollectedSpecimenTypesValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.DatabaseUploadedByValidation = material.DatabaseUploadedByValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.PathogenComment = material.PathogenComment;
        materialViewModel.FreezingDateComment = material.FreezingDateComment;
        materialViewModel.VirusConcentrationComment = material.VirusConcentrationComment;
        materialViewModel.ShipmentAmountComment = material.ShipmentAmountComment;
        materialViewModel.ShipmentTemperatureComment = material.ShipmentTemperatureComment;
        materialViewModel.ShipmentUnitOfMeasureComment = material.ShipmentUnitOfMeasureComment;
        materialViewModel.CulturingCellLineComment = material.CulturingCellLineComment;
        materialViewModel.CulturingPassagesNumberComment = material.CulturingPassagesNumberComment;
        materialViewModel.TypeOfTransportMediumComment = material.TypeOfTransportMediumComment;
        materialViewModel.BrandOfTransportMediumComment = material.BrandOfTransportMediumComment;
        materialViewModel.MaterialCollectedSpecimenTypesComment = material.MaterialCollectedSpecimenTypesComment;
        materialViewModel.DatabaseUploadedByComment = material.DatabaseUploadedByComment;
        materialViewModel.CulturingResult = material.CulturingResult;
        materialViewModel.CulturingResultDate = material.CulturingResultDate;
        materialViewModel.QualityControlResult = material.QualityControlResult;
        materialViewModel.QualityControlResultDate = material.QualityControlResultDate;
        materialViewModel.GSDAnalysisResult = material.GSDAnalysisResult;
        materialViewModel.GSDAnalysisResultDate = material.GSDAnalysisResultDate;
        materialViewModel.GSDUploadingStatus = material.GSDUploadingStatus;
        materialViewModel.GSDUploadingDate = material.GSDUploadingDate;
        materialViewModel.CulturingResultValidation = material.CulturingResultValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.CulturingResultDateValidation = material.CulturingResultDateValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.QualityControlResultValidation = material.QualityControlResultValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.QualityControlResultDateValidation = material.QualityControlResultDateValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.GSDAnalysisResultValidation = material.GSDAnalysisResultValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.GSDAnalysisResultDateValidation = material.GSDAnalysisResultDateValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.GSDUploadingStatusValidation = material.GSDUploadingStatusValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.GSDUploadingDateValidation = material.GSDUploadingDateValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.CulturingResultComment = material.CulturingResultComment;
        materialViewModel.CulturingResultDateComment = material.CulturingResultDateComment;
        materialViewModel.QualityControlResultComment = material.QualityControlResultComment;
        materialViewModel.QualityControlResultDateComment = material.QualityControlResultDateComment;
        materialViewModel.GSDAnalysisResultComment = material.GSDAnalysisResultComment;
        materialViewModel.GSDAnalysisResultDateComment = material.GSDAnalysisResultDateComment;
        materialViewModel.GSDUploadingStatusComment = material.GSDUploadingStatusComment;
        materialViewModel.GSDUploadingDateComment = material.GSDUploadingDateComment;
        materialViewModel.DateOfBMEPPReceiptValidation = material.DateOfBMEPPReceiptValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.DateOfBMEPPReceiptComment = material.DateOfBMEPPReceiptComment;
        materialViewModel.ShipmentNumberOfVialsValidation = material.ShipmentNumberOfVialsValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.ShipmentNumberOfVialsComment = material.ShipmentNumberOfVialsComment;
        materialViewModel.MaterialGSDInfo = new List<MaterialGSDInfoDto>();

        if (material.MaterialGSDInfo != null && material.MaterialGSDInfo.Any())
        {
            foreach (var elem in material.MaterialGSDInfo)
            {
                MaterialGSDInfoDto materialGSDInfoDto = new MaterialGSDInfoDto();
                materialGSDInfoDto.Id = elem.Id;
                materialGSDInfoDto.CellLine = elem.CellLine;
                materialGSDInfoDto.GSDFasta = elem.GSDFasta;
                materialGSDInfoDto.GSDType = elem.GSDType;
                materialGSDInfoDto.PassageNumber = elem.PassageNumber;
                materialViewModel.MaterialGSDInfo.Add(materialGSDInfoDto);
            }
        }

        materialViewModel.MaterialGSDInfoValidation = material.MaterialGSDInfoValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.MaterialGSDInfoComment = material.MaterialGSDInfoComment;

        materialViewModel.OwnerBioHubFacilityValidation = material.OwnerBioHubFacilityValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.OwnerBioHubFacilityComment = material.OwnerBioHubFacilityComment;

        materialViewModel.FreezingDateValidation = material.FreezingDateValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.FreezingDateComment = material.FreezingDateComment;

        materialViewModel.VirusConcentrationValidation = material.VirusConcentrationValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.VirusConcentrationComment = material.VirusConcentrationComment;

        materialViewModel.ShipmentMaterialCondition = material.ShipmentMaterialCondition ?? ShipmentMaterialCondition.Intact;
        materialViewModel.ShipmentMaterialConditionNote = material.ShipmentMaterialConditionNote;
        materialViewModel.ShipmentMaterialConditionValidation = material.ShipmentMaterialConditionValidation ?? MaterialValidationSelection.Waiting;
        materialViewModel.ShipmentMaterialConditionComment = material.ShipmentMaterialConditionComment;

        return materialViewModel;
    }
}