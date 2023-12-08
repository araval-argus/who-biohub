using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.PublicData.Core.UseCases.Materials.ListMaterials;

public interface IListMaterialMapper
{
    IEnumerable<MaterialPublicViewModel> Map(IEnumerable<Material> materials);
}

public class ListMaterialMapper : IListMaterialMapper
{
    public IEnumerable<MaterialPublicViewModel> Map(IEnumerable<Material> materials)
    {
        List<MaterialPublicViewModel> materialPublicViewModels = new List<MaterialPublicViewModel>();

        foreach (var material in materials)
        {
            MaterialPublicViewModel materialPublicViewModel = new()
            {
                Id = material.Id,
                Name = material.Name,
                ReferenceNumber = material.ReferenceNumber,
                TypeId = material.TypeId,
                SuspectedEpidemiologicalOriginId = material.SuspectedEpidemiologicalOriginId,
                OriginalProductTypeId = material.OriginalProductTypeId,
                Temperature = material.Temperature,
                UnitOfMeasureId = material.UnitOfMeasureId,
                UsagePermissionId = material.UsagePermissionId,
                Lineage = material.Lineage,
                Variant = material.Variant,
                VariantAssessment = material.VariantAssessment,
                GeneticSequenceDataId = material.GeneticSequenceDataId,
                InternationalTaxonomyClassificationId = material.InternationalTaxonomyClassificationId,
                GMO = material.GMO,
                IsolationHostTypeId = material.IsolationHostTypeId,
                ProviderLaboratoryId = material.ProviderLaboratoryId,
                ProviderBioHubFacilityId = material.ProviderBioHubFacilityId,
                BioHubFacilityDeliveryDate = GetSharedDate(material)
            };
            materialPublicViewModels.Add(materialPublicViewModel);
        }


        return materialPublicViewModels;
    }

    public DateTime? GetBioHubFacilityDeliveryDate (Material material)
    {
        var worklistToBioHubItemMaterial = material.WorklistToBioHubItemMaterials?.FirstOrDefault(x => x.MaterialId == material.Id);

        if (worklistToBioHubItemMaterial != null)
        {
            var bookingForm = worklistToBioHubItemMaterial.WorklistToBioHubItem?.BookingForms.FirstOrDefault(x => x.TransportCategoryId == material.TransportCategoryId);
            if (bookingForm != null)
            {
                return bookingForm.DateOfDelivery;
            }
        }

        return null;        
    }

    public DateTime? GetSharedDate(Material material)
    {       

        if (material.MaterialsHistory == null || !material.MaterialsHistory.Any())
        {
            return material.LastOperationDate;
        }
        else
        {
            return material.MaterialsHistory.Where(x => x.PublicShare == YesNoOption.No).OrderByDescending(x => x.CreationDate).FirstOrDefault()?.CreationDate;
        }
                
    }
}