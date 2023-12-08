using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.CreateMaterial;

public interface ICreateMaterialMapper
{
    Material Map(CreateMaterialCommand command);
}

public class CreateMaterialMapper : ICreateMaterialMapper
{
    public Material Map(CreateMaterialCommand command)
    {        

        Material material = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            ReferenceNumber = command.ReferenceNumber,
            Name = command.Name,
            Description = command.Description,
            Temperature = command.Temperature,
            SampleId = command.SampleId,
            Lineage = command.Lineage,
            Variant = command.Variant,
            VariantAssessment = command.VariantAssessment,
            StrainDesignation = command.StrainDesignation,
            Genotype = command.Genotype,
            Serotype = command.Serotype,
            DatabaseAccessionId = command.DatabaseAccessionId,
            OriginalGeneticSequence = command.OriginalGeneticSequence,

            FacilityGSD = command.FacilityGSD,
            GMO = command.GMO,            
            Infectivity = command.Infectivity,
            ViralTiter = command.ViralTiter,
            IsNew = command.IsNew,
            TypeId = command.TypeId,
            SuspectedEpidemiologicalOriginId = command.SuspectedEpidemiologicalOriginId,
            OriginalProductTypeId = command.OriginalProductTypeId,
            TransportCategoryId = command.TransportCategoryId,
            UnitOfMeasureId = command.UnitOfMeasureId,
            UsagePermissionId = command.UsagePermissionId,
            GeneticSequenceDataId = command.GeneticSequenceDataId,
            InternationalTaxonomyClassificationId = command.InternationalTaxonomyClassificationId,
            IsolationHostTypeId = command.IsolationHostTypeId,
            CultivabilityTypeId = command.CultivabilityTypeId,
            IsolationTechniqueTypeId = command.IsolationTechniqueTypeId,
            DeletedOn = null,
            ProviderBioHubFacilityId = command.ProviderBioHubFacilityId,
            ProviderLaboratoryId = command.ProviderLaboratoryId,
            Age = command.Age,
            PatientStatus = command.PatientStatus,
            CollectionDate = command.CollectionDate,
            Location = command.Location,
            Gender = command.Gender,
            LastOperationById = command.UserId,
            LastOperationDate = DateTime.UtcNow,
            OwnerBioHubFacilityId = command.OwnerBioHubFacilityId,
            WarningEmailCurrentNumberOfVialsThreshold = 10,
            CurrentNumberOfVials = 0,
            ManualCreation = true,

            ShipmentNumberOfVials = command.ShipmentNumberOfVials,
            DateOfBMEPPReceipt = command.DateOfBMEPPReceipt,

            ProductTypeId = command.ProductTypeId,

            BHFShareReadiness = Readiness.Ready,
            PublicShare = command.PublicShare,
        };

        return material;
    }
}