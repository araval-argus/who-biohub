using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterial;

public interface IReadMaterialMapper
{
    MaterialViewModel Map(Material material, RoleType roleType, Guid? instituteId);
}

public class ReadMaterialMapper : IReadMaterialMapper
{
    public MaterialViewModel Map(Material material, RoleType roleType, Guid? instituteId)
    {


        MaterialViewModel materialViewModel = MapBase(material);


        if (roleType == RoleType.WHO || (roleType == RoleType.Laboratory && material.ProviderLaboratoryId == instituteId) ||
            (roleType == RoleType.BioHubFacility && material.OwnerBioHubFacilityId == instituteId))
        {

            materialViewModel.Description = material.Description;
            materialViewModel.SampleId = material.SampleId;
            materialViewModel.StrainDesignation = material.StrainDesignation;
            materialViewModel.DatabaseAccessionId = material.DatabaseAccessionId;
            materialViewModel.OriginalGeneticSequence = material.OriginalGeneticSequence;
            materialViewModel.Infectivity = material.Infectivity;
            materialViewModel.ViralTiter = material.ViralTiter;
            materialViewModel.OriginalProductTypeId = material.OriginalProductTypeId;
            materialViewModel.TransportCategoryId = material.TransportCategoryId;
            materialViewModel.GeneticSequenceDataId = material.GeneticSequenceDataId;
            materialViewModel.IsolationHostTypeId = material.IsolationHostTypeId;
            materialViewModel.CultivabilityTypeId = material.CultivabilityTypeId;
            materialViewModel.IsolationTechniqueTypeId = material.IsolationTechniqueTypeId;
            materialViewModel.Age = material.Age;
            materialViewModel.PatientStatus = material.PatientStatus;
            materialViewModel.CollectionDate = material.CollectionDate;
            materialViewModel.Location = material.Location;
            materialViewModel.Gender = material.Gender;
            materialViewModel.Status = material.Status;
            materialViewModel.ReferenceId = material.ReferenceId;

            materialViewModel.ShipmentNumberOfVials = material.ShipmentNumberOfVials;
            materialViewModel.CurrentNumberOfVials = material.CurrentNumberOfVials;
            materialViewModel.WarningEmailCurrentNumberOfVialsThreshold = material.WarningEmailCurrentNumberOfVialsThreshold;
            materialViewModel.ShipmentInformationVisible = !material.ManualCreation;



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
            materialViewModel.ShipmentInformationVisible = true;

            materialViewModel.ShipmentMaterialCondition = material.ShipmentMaterialCondition ?? ShipmentMaterialCondition.Intact;
            materialViewModel.ShipmentMaterialConditionNote = material.ShipmentMaterialConditionNote;
        }


        return materialViewModel;
    }



    public MaterialViewModel MapBase(Material material)
    {

        MaterialViewModel materialViewModel = new MaterialViewModel();

        materialViewModel.Id = material.Id;
        materialViewModel.ReferenceNumber = material.ReferenceNumber;
        materialViewModel.Name = material.Name;
        materialViewModel.Temperature = material.Temperature;
        materialViewModel.Lineage = material.Lineage;
        materialViewModel.Variant = material.Variant;
        materialViewModel.VariantAssessment = material.VariantAssessment;
        materialViewModel.GMO = material.GMO;
        materialViewModel.TypeId = material.TypeId;
        materialViewModel.SuspectedEpidemiologicalOriginId = material.SuspectedEpidemiologicalOriginId;
        materialViewModel.UnitOfMeasureId = material.UnitOfMeasureId;
        materialViewModel.UsagePermissionId = material.UsagePermissionId;
        materialViewModel.InternationalTaxonomyClassificationId = material.InternationalTaxonomyClassificationId;
        materialViewModel.ProviderBioHubFacilityId = material.ProviderBioHubFacilityId;
        materialViewModel.ProviderLaboratoryId = material.ProviderLaboratoryId;
        materialViewModel.OwnerBioHubFacilityId = material.OwnerBioHubFacilityId;
        materialViewModel.DateOfBMEPPReceipt = material.DateOfBMEPPReceipt;
        materialViewModel.ProductTypeId = material.ProductTypeId;
        materialViewModel.BHFShareReadiness = material.BHFShareReadiness ?? Readiness.NotReady;
        materialViewModel.PublicShare = material.PublicShare ?? YesNoOption.No;
        materialViewModel.Pathogen = material.Pathogen;
        materialViewModel.ShipmentInformationVisible = false;
        materialViewModel.CulturingResult = material.CulturingResult;
        materialViewModel.CulturingResultDate = material.CulturingResultDate;
        materialViewModel.QualityControlResult = material.QualityControlResult;
        materialViewModel.QualityControlResultDate = material.QualityControlResultDate;
        materialViewModel.GSDAnalysisResult = material.GSDAnalysisResult;
        materialViewModel.GSDAnalysisResultDate = material.GSDAnalysisResultDate;
        materialViewModel.GSDUploadingStatus = material.GSDUploadingStatus;
        materialViewModel.GSDUploadingDate = material.GSDUploadingDate;
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

        return materialViewModel;
    }
}