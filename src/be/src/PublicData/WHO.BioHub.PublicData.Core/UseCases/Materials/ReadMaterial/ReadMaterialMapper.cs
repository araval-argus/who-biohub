using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Materials.ReadMaterial;

public interface IReadMaterialMapper
{
    MaterialPublicViewModel Map(Material material);
}

public class ReadMaterialMapper : IReadMaterialMapper
{
    public MaterialPublicViewModel Map(Material material)
    {
        MaterialPublicViewModel materialPublicViewModel = new()
        {
            Id = material.Id,
            Name = material.Name,
            ReferenceNumber = material.ReferenceNumber,
            TypeId = material.TypeId,
            SuspectedEpidemiologicalOriginId = material.SuspectedEpidemiologicalOriginId,
            OriginalProductTypeId = material.OriginalProductTypeId,
            ProductTypeId = material.ProductTypeId,
            DateOfBMEPPReceipt = material.DateOfBMEPPReceipt,
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
            BioHubFacilityDeliveryDate = GetBioHubFacilityDeliveryDate(material)
        };

        return materialPublicViewModel;
    }

    public DateTime? GetBioHubFacilityDeliveryDate(Material material)
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
}