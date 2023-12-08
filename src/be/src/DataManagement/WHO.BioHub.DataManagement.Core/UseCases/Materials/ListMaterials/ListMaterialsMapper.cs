using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;

public interface IListMaterialsMapper
{
    IEnumerable<MaterialViewModel> Map(IEnumerable<Material> materials);
}

public class ListMaterialsMapper : IListMaterialsMapper
{
    public IEnumerable<MaterialViewModel> Map(IEnumerable<Material> materials)
    {      

        List<MaterialViewModel> list = new List<MaterialViewModel>();

        foreach (var material in materials)
        {
            MaterialViewModel materialViewModel = new MaterialViewModel();

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
            //materialViewModel.ProductionCellLine = material.ProductionCellLine;
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
            materialViewModel.OwnerBioHubFacilityId = material.OwnerBioHubFacilityId;
            materialViewModel.ShipmentInformationVisible = !material.ManualCreation;

            materialViewModel.ProductTypeId = material.ProductTypeId;

            materialViewModel.BHFShareReadiness = material.BHFShareReadiness;
            materialViewModel.PublicShare = material.PublicShare;

            materialViewModel.SharedWithQE = material.WorklistFromBioHubItemMaterials.Any(x => x.WorklistFromBioHubItem?.Status == WorklistFromBioHubStatus.ShipmentCompleted);

            materialViewModel.ShipmentMaterialCondition = material.ShipmentMaterialCondition ?? ShipmentMaterialCondition.Intact;

            list.Add(materialViewModel);
        }

        return list;
    }
}