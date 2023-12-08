using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.DAL.Configurations;

namespace WHO.BioHub.DAL
{
    public class BioHubDbContext : DbContext
    {
        public BioHubDbContext([NotNull] DbContextOptions options) : base(options) { }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Laboratory> Laboratories { get; set; }
        public virtual DbSet<BSLLevel> BSLLevels { get; set; }
        public virtual DbSet<BioHubFacility> BioHubFacilities { get; set; }
        public virtual DbSet<MaterialType> MaterialTypes { get; set; }
        public virtual DbSet<MaterialProduct> MaterialProducts { get; set; }
        public virtual DbSet<TemperatureUnitOfMeasure> TemperatureUnitOfMeasures { get; set; }
        public virtual DbSet<MaterialUsagePermission> MaterialUsagePermissions { get; set; }
        public virtual DbSet<GeneticSequenceData> GeneticSequenceDatas { get; set; }
        public virtual DbSet<InternationalTaxonomyClassification> InternationalTaxonomyClassifications { get; set; }
        public virtual DbSet<IsolationHostType> IsolationHostTypes { get; set; }
        public virtual DbSet<CultivabilityType> CultivabilityTypes { get; set; }
        public virtual DbSet<IsolationTechniqueType> IsolationTechniqueTypes { get; set; }
        public virtual DbSet<TransportCategory> TransportCategories { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<TransportMode> TransportModes { get; set; }
        public virtual DbSet<PriorityRequestType> PriorityRequestTypes { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRequest> UserRequests { get; set; }
        public virtual DbSet<UserRequestStatus> UserRequestStatuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<DocumentTemplate> DocumentTemplates { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<WorklistToBioHubEmail> WorklistToBioHubEmails { get; set; }
        public virtual DbSet<WorklistToBioHubHistoryItem> WorklistToBioHubHistoryItems { get; set; }
        public virtual DbSet<WorklistToBioHubHistoryItemDocument> WorklistToBioHubHistoryItemDocuments { get; set; }
        public virtual DbSet<WorklistToBioHubItem> WorklistToBioHubItems { get; set; }
        public virtual DbSet<WorklistToBioHubItemDocument> WorklistToBioHubItemDocuments { get; set; }
        public virtual DbSet<MaterialShippingInformation> MaterialShippingInformations { get; set; }
        public virtual DbSet<MaterialClinicalDetail> MaterialClinicalDetails { get; set; }
        public virtual DbSet<WorklistToBioHubItemLaboratoryFocalPoint> WorklistToBioHubItemLaboratoryFocalPoints { get; set; }
        public virtual DbSet<MaterialShippingInformationHistory> MaterialShippingInformationsHistory { get; set; }
        public virtual DbSet<MaterialClinicalDetailHistory> MaterialClinicalDetailsHistory { get; set; }
        public virtual DbSet<WorklistToBioHubHistoryItemLaboratoryFocalPoint> WorklistToBioHubHistoryItemLaboratoryFocalPoints { get; set; }
        public virtual DbSet<BookingForm> BookingForms { get; set; }
        public virtual DbSet<BookingFormPickupUser> BookingFormPickupUsers { get; set; }

        public virtual DbSet<BookingFormHistory> BookingFormsHistory { get; set; }
        public virtual DbSet<BookingFormPickupUserHistory> BookingFormPickupUsersHistory { get; set; }
        public virtual DbSet<Courier> Couriers { get; set; }
        public virtual DbSet<BookingFormCourierUser> BookingFormCourierUsers { get; set; }
        public virtual DbSet<BookingFormCourierUserHistory> BookingFormCourierUsersHistory { get; set; }

        public virtual DbSet<WorklistToBioHubItemFeedback> WorklistToBioHubItemFeedback { get; set; }
        public virtual DbSet<WorklistToBioHubHistoryItemFeedback> WorklistToBioHubHistoryItemFeedback { get; set; }
        public virtual DbSet<WorklistFromBioHubEmail> WorklistFromBioHubEmails { get; set; }
        public virtual DbSet<WorklistFromBioHubHistoryItem> WorklistFromBioHubHistoryItems { get; set; }
        public virtual DbSet<WorklistFromBioHubHistoryItemDocument> WorklistFromBioHubHistoryItemDocuments { get; set; }
        public virtual DbSet<WorklistFromBioHubItem> WorklistFromBioHubItems { get; set; }
        public virtual DbSet<WorklistFromBioHubItemDocument> WorklistFromBioHubItemDocuments { get; set; }
        public virtual DbSet<WorklistFromBioHubItemFeedback> WorklistFromBioHubItemFeedback { get; set; }
        public virtual DbSet<WorklistFromBioHubHistoryItemFeedback> WorklistFromBioHubHistoryItemFeedback { get; set; }

        public virtual DbSet<BiosafetyChecklistOfSMTA2> BiosafetyChecklistOfSMTA2s { get; set; }

        public virtual DbSet<Annex2OfSMTA2Condition> Annex2OfSMTA2Conditions { get; set; }

        public virtual DbSet<WorklistFromBioHubHistoryItemAnnex2OfSMTA2Condition> WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions { get; set; }
        public virtual DbSet<WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2> WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s { get; set; }
        public virtual DbSet<WorklistFromBioHubHistoryItemMaterial> WorklistFromBioHubHistoryItemMaterials { get; set; }
        public virtual DbSet<WorklistFromBioHubItemAnnex2OfSMTA2Condition> WorklistFromBioHubItemAnnex2OfSMTA2Conditions { get; set; }
        public virtual DbSet<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2> WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s { get; set; }
        public virtual DbSet<WorklistFromBioHubItemMaterial> WorklistFromBioHubItemMaterials { get; set; }
        public virtual DbSet<WorklistFromBioHubHistoryItemLaboratoryFocalPoint> WorklistFromBioHubHistoryItemLaboratoryFocalPoints { get; set; }
        public virtual DbSet<WorklistFromBioHubItemLaboratoryFocalPoint> WorklistFromBioHubItemLaboratoryFocalPoints { get; set; }
        public virtual DbSet<WorklistItemUsedReferenceNumber> WorklistItemUsedReferenceNumbers { get; set; }
        public virtual DbSet<WorklistToBioHubItemMaterial> WorklistToBioHubItemMaterials { get; set; }
        public virtual DbSet<WorklistToBioHubItemBioHubFacilityFocalPoint> WorklistToBioHubItemBioHubFacilityFocalPoints { get; set; }
        public virtual DbSet<WorklistToBioHubHistoryItemBioHubFacilityFocalPoint> WorklistToBioHubHistoryItemBioHubFacilityFocalPoints { get; set; }
        public virtual DbSet<MaterialHistory> MaterialsHistory { get; set; }

        public virtual DbSet<SMTA1WorkflowEmail> SMTA1WorkflowEmails { get; set; }
        public virtual DbSet<SMTA1WorkflowHistoryItem> SMTA1WorkflowHistoryItems { get; set; }
        public virtual DbSet<SMTA1WorkflowHistoryItemDocument> SMTA1WorkflowHistoryItemDocuments { get; set; }
        public virtual DbSet<SMTA1WorkflowItem> SMTA1WorkflowItems { get; set; }
        public virtual DbSet<SMTA1WorkflowItemDocument> SMTA1WorkflowItemDocuments { get; set; }

        public virtual DbSet<SMTA2WorkflowEmail> SMTA2WorkflowEmails { get; set; }
        public virtual DbSet<SMTA2WorkflowHistoryItem> SMTA2WorkflowHistoryItems { get; set; }
        public virtual DbSet<SMTA2WorkflowHistoryItemDocument> SMTA2WorkflowHistoryItemDocuments { get; set; }
        public virtual DbSet<SMTA2WorkflowItem> SMTA2WorkflowItems { get; set; }
        public virtual DbSet<SMTA2WorkflowItemDocument> SMTA2WorkflowItemDocuments { get; set; }
        public virtual DbSet<WorklistFromBioHubItemBiosafetyChecklistThreadComment> WorklistFromBioHubItemBiosafetyChecklistThreadComments { get; set; }
        public virtual DbSet<WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComment> WorklistFromBioHubHistoryItemBiosafetyChecklistThreadComments { get; set; }

        public virtual DbSet<Resource> Resources { get; set; }

        public virtual DbSet<MaterialLaboratoryAnalysisInformation> MaterialLaboratoryAnalysisInformation { get; set; }
        public virtual DbSet<SpecimenType> SpecimenTypes { get; set; }
        public virtual DbSet<CollectedSpecimenType> CollectedSpecimenTypes { get; set; }

        public virtual DbSet<MaterialLaboratoryAnalysisInformationHistory> MaterialLaboratoryAnalysisInformationHistory { get; set; }
        public virtual DbSet<CollectedSpecimenTypeHistory> CollectedSpecimenTypesHistory { get; set; }

        public virtual DbSet<MaterialCollectedSpecimenType> MaterialCollectedSpecimenTypes { get; set; }
        public virtual DbSet<MaterialCollectedSpecimenTypeHistory> MaterialCollectedSpecimenTypesHistory { get; set; }


        public virtual DbSet<MaterialGSDInfo> MaterialGSDInfo { get; set; }

        public virtual DbSet<MaterialGSDInfoHistory> MaterialGSDInfoHistory { get; set; }

        public virtual DbSet<BioHubFacilityHistory> BioHubFacilitiesHistory { get; set; }
        public virtual DbSet<CourierHistory> CouriersHistory { get; set; }
        public virtual DbSet<LaboratoryHistory> LaboratoriesHistory { get; set; }
        public virtual DbSet<UserHistory> UsersHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(BSLLevelConfiguration.Default);
            modelBuilder.ApplyConfiguration(CountryConfiguration.Default);
            modelBuilder.ApplyConfiguration(LaboratoryConfiguration.Default);
            modelBuilder.ApplyConfiguration(BioHubFacilityConfiguration.Default);
            modelBuilder.ApplyConfiguration(MaterialProductConfiguration.Default);
            modelBuilder.ApplyConfiguration(MaterialTypeConfiguration.Default);
            modelBuilder.ApplyConfiguration(TemperatureUnitOfMeasureConfiguration.Default);
            modelBuilder.ApplyConfiguration(MaterialUsagePermissionConfiguration.Default);
            modelBuilder.ApplyConfiguration(GeneticSequenceDataConfiguration.Default);
            modelBuilder.ApplyConfiguration(InternationalTaxonomyClassificationConfiguration.Default);
            modelBuilder.ApplyConfiguration(IsolationHostTypeConfiguration.Default);
            modelBuilder.ApplyConfiguration(CultivabilityTypeConfiguration.Default);
            modelBuilder.ApplyConfiguration(IsolationTechniqueTypeConfiguration.Default);
            modelBuilder.ApplyConfiguration(TransportCategoryConfiguration.Default);
            modelBuilder.ApplyConfiguration(MaterialConfiguration.Default);
            modelBuilder.ApplyConfiguration(TransportModeConfiguration.Default);
            modelBuilder.ApplyConfiguration(PriorityRequestTypeConfiguration.Default);
            modelBuilder.ApplyConfiguration(ShipmentConfiguration.Default);
            modelBuilder.ApplyConfiguration(RoleConfiguration.Default);
            modelBuilder.ApplyConfiguration(UserRequestConfiguration.Default);
            modelBuilder.ApplyConfiguration(UserRequestStatusConfiguration.Default);
            modelBuilder.ApplyConfiguration(UserConfiguration.Default);
            modelBuilder.ApplyConfiguration(PermissionConfiguration.Default);
            modelBuilder.ApplyConfiguration(RolePermissionConfiguration.Default);
            modelBuilder.ApplyConfiguration(DocumentTemplateConfiguration.Default);
            modelBuilder.ApplyConfiguration(DocumentConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistToBioHubEmailConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistToBioHubHistoryItemConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistToBioHubHistoryItemDocumentConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistToBioHubItemConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistToBioHubItemDocumentConfiguration.Default);

            modelBuilder.ApplyConfiguration(MaterialShippingInformationConfiguration.Default);
            modelBuilder.ApplyConfiguration(MaterialClinicalDetailConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistToBioHubItemLaboratoryFocalPointConfiguration.Default);
            modelBuilder.ApplyConfiguration(MaterialShippingInformationHistoryConfiguration.Default);
            modelBuilder.ApplyConfiguration(MaterialClinicalDetailHistoryConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistToBioHubHistoryItemLaboratoryFocalPointConfiguration.Default);

            modelBuilder.ApplyConfiguration(BookingFormConfiguration.Default);
            modelBuilder.ApplyConfiguration(BookingFormPickupUserConfiguration.Default);

            modelBuilder.ApplyConfiguration(BookingFormHistoryConfiguration.Default);
            modelBuilder.ApplyConfiguration(BookingFormPickupUserHistoryConfiguration.Default);

            modelBuilder.ApplyConfiguration(CourierConfiguration.Default);
            modelBuilder.ApplyConfiguration(BookingFormCourierUserConfiguration.Default);
            modelBuilder.ApplyConfiguration(BookingFormCourierUserHistoryConfiguration.Default);

            modelBuilder.ApplyConfiguration(WorklistToBioHubItemFeedbackConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistToBioHubHistoryItemFeedbackConfiguration.Default);

            modelBuilder.ApplyConfiguration(WorklistFromBioHubEmailConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubHistoryItemConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubHistoryItemDocumentConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubItemConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubItemDocumentConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubItemFeedbackConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubHistoryItemFeedbackConfiguration.Default);
            modelBuilder.ApplyConfiguration(Annex2OfSMTA2ConditionConfiguration.Default);
            modelBuilder.ApplyConfiguration(BiosafetyChecklistOfSMTA2Configuration.Default);

            modelBuilder.ApplyConfiguration(WorklistFromBioHubHistoryItemAnnex2OfSMTA2ConditionConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2Configuration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubHistoryItemMaterialConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubItemAnnex2OfSMTA2ConditionConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Configuration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubItemMaterialConfiguration.Default);

            modelBuilder.ApplyConfiguration(WorklistFromBioHubHistoryItemLaboratoryFocalPointConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubItemLaboratoryFocalPointConfiguration.Default);

            modelBuilder.ApplyConfiguration(WorklistItemUsedReferenceNumberConfiguration.Default);

            modelBuilder.ApplyConfiguration(WorklistToBioHubItemMaterialConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistToBioHubItemBioHubFacilityFocalPointConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistToBioHubHistoryItemBioHubFacilityFocalPointConfiguration.Default);
            modelBuilder.ApplyConfiguration(MaterialHistoryConfiguration.Default);


            modelBuilder.ApplyConfiguration(SMTA1WorkflowEmailConfiguration.Default);
            modelBuilder.ApplyConfiguration(SMTA1WorkflowHistoryItemConfiguration.Default);
            modelBuilder.ApplyConfiguration(SMTA1WorkflowHistoryItemDocumentConfiguration.Default);
            modelBuilder.ApplyConfiguration(SMTA1WorkflowItemConfiguration.Default);
            modelBuilder.ApplyConfiguration(SMTA1WorkflowItemDocumentConfiguration.Default);


            modelBuilder.ApplyConfiguration(SMTA2WorkflowEmailConfiguration.Default);
            modelBuilder.ApplyConfiguration(SMTA2WorkflowHistoryItemConfiguration.Default);
            modelBuilder.ApplyConfiguration(SMTA2WorkflowHistoryItemDocumentConfiguration.Default);
            modelBuilder.ApplyConfiguration(SMTA2WorkflowItemConfiguration.Default);
            modelBuilder.ApplyConfiguration(SMTA2WorkflowItemDocumentConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubItemBiosafetyChecklistThreadCommentConfiguration.Default);
            modelBuilder.ApplyConfiguration(WorklistFromBioHubHistoryItemBiosafetyChecklistThreadCommentConfiguration.Default);
            modelBuilder.ApplyConfiguration(ResourceConfiguration.Default);
            modelBuilder.ApplyConfiguration(MaterialLaboratoryAnalysisInformationConfiguration.Default);
            modelBuilder.ApplyConfiguration(SpecimenTypeConfiguration.Default);
            modelBuilder.ApplyConfiguration(CollectedSpecimenTypeConfiguration.Default);

            modelBuilder.ApplyConfiguration(MaterialLaboratoryAnalysisInformationHistoryConfiguration.Default);      
            modelBuilder.ApplyConfiguration(CollectedSpecimenTypeHistoryConfiguration.Default);

            modelBuilder.ApplyConfiguration(MaterialCollectedSpecimenTypeConfiguration.Default);
            modelBuilder.ApplyConfiguration(MaterialCollectedSpecimenTypeHistoryConfiguration.Default);

            modelBuilder.ApplyConfiguration(MaterialGSDInfoConfiguration.Default);
            modelBuilder.ApplyConfiguration(MaterialGSDInfoHistoryConfiguration.Default);

            modelBuilder.ApplyConfiguration(BioHubFacilityHistoryConfiguration.Default);

            modelBuilder.ApplyConfiguration(CourierHistoryConfiguration.Default);
            modelBuilder.ApplyConfiguration(LaboratoryHistoryConfiguration.Default);
            modelBuilder.ApplyConfiguration(UserHistoryConfiguration.Default);         

        }
    }
}
