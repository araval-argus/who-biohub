using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHO.BioHub.PublicData.Core.Dto
{
    public class MaterialPublicViewModel
    {
        public Guid Id { get; set; }
        public string ReferenceNumber { get; set; }
        public string Name { get; set; }
        public Guid? TypeId { get; set; }
        public Guid? SuspectedEpidemiologicalOriginId { get; set; }
        public Guid? OriginalProductTypeId { get; set; }
        public Guid? ProductTypeId { get; set; }
        public double? Temperature { get; set; }
        public Guid? UnitOfMeasureId { get; set; }
        public Guid? UsagePermissionId { get; set; }
        public string Lineage { get; set; }
        public string Variant { get; set; }
        public string VariantAssessment { get; set; }
        public Guid? GeneticSequenceDataId { get; set; }
        public Guid? InternationalTaxonomyClassificationId { get; set; }
        public bool GMO { get; set; }
        public Guid? IsolationHostTypeId { get; set; }
        public Guid? ProviderLaboratoryId { get; set; }
        public Guid? ProviderBioHubFacilityId { get; set; }
        public DateTime? BioHubFacilityDeliveryDate { get; set; }
        public DateTime? DateOfBMEPPReceipt { get; set; }
    }
}
