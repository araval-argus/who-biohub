using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.UpdateMaterialForBioHubFacilityCompletion;

public interface IUpdateMaterialForBioHubFacilityCompletionMapper
{
    Material MapFields(Material material, UpdateMaterialForBioHubFacilityCompletionCommand command);
    Material MapApprove(Material material, UpdateMaterialForBioHubFacilityCompletionCommand command);
}

public class UpdateMaterialForBioHubFacilityCompletionMapper : IUpdateMaterialForBioHubFacilityCompletionMapper
{
    public Material MapFields(Material material, UpdateMaterialForBioHubFacilityCompletionCommand command)
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
        material.ProviderBioHubFacilityId = command.ProviderBioHubFacilityId;
        material.ProviderLaboratoryId = command.ProviderLaboratoryId;
        material.Age = command.Age;
        material.PatientStatus = command.PatientStatus;
        material.CollectionDate = command.CollectionDate;
        material.Location = command.Location;
        material.Gender = command.Gender;
        material.WarningEmailCurrentNumberOfVialsThreshold = command.WarningEmailCurrentNumberOfVialsThreshold;

        material.ProductTypeId = command.ProductTypeId;


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

        //Shipment Information

        if (command.UserPermissions.Contains(PermissionNames.CanEditMaterialShipmentInformation))
        {
            //material.DateOfBMEPPReceipt = command.DateOfBMEPPReceipt;
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

        }
        return material;
    }

    public Material MapApprove(Material material, UpdateMaterialForBioHubFacilityCompletionCommand command)
    {                 

        material.Status = command.Approve == true ? MaterialStatus.WaitingForLaboratoryCompletion : material.Status;

        return material;
    }

}