using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public static MaterialConfiguration Default => new();

    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.ReferenceNumber)
           .IsUnique();

        //builder.HasIndex(e => e.Name)
        //    .HasDatabaseName("IX_Name");        

        builder.Property(e => e.ReferenceNumber)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.Description)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.SampleId)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.Lineage)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.Variant)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.VariantAssessment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.StrainDesignation)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.Genotype)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.Serotype)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.DatabaseAccessionId)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.Temperature)
            .IsRequired(false);

        builder.Property(e => e.OriginalGeneticSequence).IsRequired(false); //TODO: For the moment, set intentionally to MaxLength. It will be refined as soon as a requirement is received for this property

 
        builder.Property(e => e.FacilityGSD)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.ProductionCellLine)
            .IsRequired(false)
           .HasMaxLength(1000);

        builder.Property(e => e.Infectivity)
            .IsRequired(false)
           .HasMaxLength(1000);

        builder.Property(e => e.ViralTiter)
            .IsRequired(false)
           .HasMaxLength(1000);

        builder.Property(e => e.Location)
            .IsRequired(false)
           .HasMaxLength(255);

        builder.Property(e => e.PatientStatus)
            .IsRequired(false)
           .HasMaxLength(255);

        builder.Property(e => e.Age)
            .IsRequired(false);

        builder.Property(e => e.Gender)
            .IsRequired(false);

        builder.Property(e => e.CollectionDate)
            .IsRequired(false);       

        builder.Property(e => e.CurrentNumberOfVials)
           .IsRequired(false);

        builder.Property(e => e.WarningEmailCurrentNumberOfVialsThreshold)
           .IsRequired(false);

       
       

    builder
           .HasOne(e => e.Type)
           .WithMany(b => b.Materials)
           .HasForeignKey(e => e.TypeId)
           .HasPrincipalKey(e => e.Id)
           .OnDelete(DeleteBehavior.Restrict)
           .IsRequired(false);

        builder
          .HasOne(e => e.SuspectedEpidemiologicalOrigin)
          .WithMany(b => b.Materials)
          .HasForeignKey(e => e.SuspectedEpidemiologicalOriginId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);




        builder
          .HasOne(e => e.TransportCategory)
          .WithMany(b => b.Materials)
          .HasForeignKey(e => e.TransportCategoryId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.UnitOfMeasure)
          .WithMany(b => b.Materials)
          .HasForeignKey(e => e.UnitOfMeasureId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.UsagePermission)
          .WithMany(b => b.Materials)
          .HasForeignKey(e => e.UsagePermissionId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.GeneticSequenceData)
          .WithMany(b => b.Materials)
          .HasForeignKey(e => e.GeneticSequenceDataId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.InternationalTaxonomyClassification)
          .WithMany(b => b.Materials)
          .HasForeignKey(e => e.InternationalTaxonomyClassificationId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.IsolationHostType)
          .WithMany(b => b.Materials)
          .HasForeignKey(e => e.IsolationHostTypeId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.CultivabilityType)
          .WithMany(b => b.Materials)
          .HasForeignKey(e => e.CultivabilityTypeId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.IsolationTechniqueType)
          .WithMany(b => b.Materials)
          .HasForeignKey(e => e.IsolationTechniqueTypeId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.ProviderLaboratory)
          .WithMany(b => b.Materials)
          .HasForeignKey(e => e.ProviderLaboratoryId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.ProviderBioHubFacility)
          .WithMany(b => b.Materials)
          .HasForeignKey(e => e.ProviderBioHubFacilityId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);


        builder
          .HasOne(e => e.OwnerBioHubFacility)
          .WithMany(b => b.OwnedMaterials)
          .HasForeignKey(e => e.OwnerBioHubFacilityId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);


        builder.Property(e => e.ReferenceNumberComment)
             .IsRequired(false)
             .HasMaxLength(1000);

        builder.Property(e => e.NameComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.DescriptionComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.TypeComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.SuspectedEpidemiologicalOriginComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.OriginalProductTypeComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.TransportCategoryComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.TemperatureComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.UnitOfMeasureComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.UsagePermissionComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.SampleIdComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.LineageComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.VariantComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.VariantAssessmentComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.StrainDesignationComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.GenotypeComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.SerotypeComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.GeneticSequenceDataComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.DatabaseAccessionIdComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.OriginalGeneticSequenceComment)
            .IsRequired(false)
            .HasMaxLength(1000);



        builder.Property(e => e.FacilityGSDComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.InternationalTaxonomyClassificationComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.GMOComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.IsolationHostTypeComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.CultivabilityTypeComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.ProductionCellLineComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.IsolationTechniqueTypeComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.InfectivityComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.ViralTiterComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.CollectionDateComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.LocationComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.GenderComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.AgeComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.PatientStatusComment)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.Property(e => e.ReferenceNumberValidation)
             .IsRequired(false);


        builder.Property(e => e.NameValidation)
            .IsRequired(false);


        builder.Property(e => e.DescriptionValidation)
            .IsRequired(false);


        builder.Property(e => e.TypeValidation)
            .IsRequired(false);


        builder.Property(e => e.SuspectedEpidemiologicalOriginValidation)
            .IsRequired(false);


        builder.Property(e => e.OriginalProductTypeValidation)
            .IsRequired(false);


        builder.Property(e => e.TransportCategoryValidation)
            .IsRequired(false);


        builder.Property(e => e.TemperatureValidation)
            .IsRequired(false);


        builder.Property(e => e.UnitOfMeasureValidation)
            .IsRequired(false);


        builder.Property(e => e.UsagePermissionValidation)
            .IsRequired(false);


        builder.Property(e => e.SampleIdValidation)
            .IsRequired(false);


        builder.Property(e => e.LineageValidation)
            .IsRequired(false);


        builder.Property(e => e.VariantValidation)
            .IsRequired(false);


        builder.Property(e => e.VariantAssessmentValidation)
            .IsRequired(false);


        builder.Property(e => e.StrainDesignationValidation)
            .IsRequired(false);


        builder.Property(e => e.GenotypeValidation)
            .IsRequired(false);


        builder.Property(e => e.SerotypeValidation)
            .IsRequired(false);


        builder.Property(e => e.GeneticSequenceDataValidation)
            .IsRequired(false);


        builder.Property(e => e.DatabaseAccessionIdValidation)
            .IsRequired(false);


        builder.Property(e => e.OriginalGeneticSequenceValidation)
            .IsRequired(false);


        builder.Property(e => e.FacilityGSDValidation)
            .IsRequired(false);


        builder.Property(e => e.InternationalTaxonomyClassificationValidation)
            .IsRequired(false);


        builder.Property(e => e.GMOValidation)
            .IsRequired(false);


        builder.Property(e => e.IsolationHostTypeValidation)
            .IsRequired(false);


        builder.Property(e => e.CultivabilityTypeValidation)
            .IsRequired(false);


        builder.Property(e => e.ProductionCellLineValidation)
            .IsRequired(false);


        builder.Property(e => e.IsolationTechniqueTypeValidation)
            .IsRequired(false);


        builder.Property(e => e.InfectivityValidation)
            .IsRequired(false);


        builder.Property(e => e.ViralTiterValidation)
            .IsRequired(false);


        builder.Property(e => e.CollectionDateValidation)
            .IsRequired(false);


        builder.Property(e => e.LocationValidation)
            .IsRequired(false);


        builder.Property(e => e.GenderValidation)
            .IsRequired(false);


        builder.Property(e => e.AgeValidation)
            .IsRequired(false);


        builder.Property(e => e.PatientStatusValidation)
            .IsRequired(false);

        builder
          .HasOne(e => e.LastOperationBy)
          .WithMany(b => b.Materials)
          .HasForeignKey(e => e.LastOperationById)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);



        ////
        ///

        builder
          .HasOne(e => e.ProductType)
          .WithMany(b => b.Materials)
          .HasForeignKey(e => e.ProductTypeId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

       
        builder.Property(e => e.ProductTypeComment)
            .IsRequired(false)
            .HasMaxLength(1000);


        builder.Property(e => e.ProductTypeValidation)
           .IsRequired(false);



        ///

        builder.Property(e => e.Pathogen)
           .HasMaxLength(255)
           .IsRequired(false);

        builder.Property(e => e.PathogenComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.PathogenValidation)
            .IsRequired(false);

        builder.Property(e => e.VirusConcentration)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.FreezingDate)           
           .IsRequired(false);

        builder.Property(e => e.FreezingDateComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.FreezingDateValidation)
            .IsRequired(false);

        builder.Property(e => e.VirusConcentration)
            .HasMaxLength(255)
           .IsRequired(false);

        builder.Property(e => e.VirusConcentrationComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.VirusConcentrationValidation)
            .IsRequired(false);



        builder.Property(e => e.ShipmentNumberOfVials)
           .IsRequired(false);

        builder.Property(e => e.ShipmentNumberOfVialsComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.ShipmentNumberOfVialsValidation)
            .IsRequired(false);


        builder.Property(e => e.ShipmentAmount)
           .IsRequired(false);

        builder.Property(e => e.ShipmentAmountComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.ShipmentAmountValidation)
            .IsRequired(false);


        builder.Property(e => e.ShipmentTemperature)
          .IsRequired(false);

        builder.Property(e => e.ShipmentTemperatureComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.ShipmentTemperatureValidation)
            .IsRequired(false);


        builder.Property(e => e.ShipmentUnitOfMeasureId)
          .IsRequired(false);

        builder.Property(e => e.ShipmentUnitOfMeasureComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.ShipmentUnitOfMeasureValidation)
            .IsRequired(false);



        builder.Property(e => e.CulturingCellLine)
            .HasMaxLength(255)
          .IsRequired(false);

        builder.Property(e => e.CulturingCellLineComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.CulturingCellLineValidation)
            .IsRequired(false);


        builder.Property(e => e.CulturingPassagesNumber)
         .IsRequired(false);

        builder.Property(e => e.CulturingPassagesNumberComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.CulturingPassagesNumberValidation)
            .IsRequired(false);

        builder.Property(e => e.TypeOfTransportMedium)
           .HasMaxLength(255)
         .IsRequired(false);

        builder.Property(e => e.TypeOfTransportMediumComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.TypeOfTransportMediumValidation)
            .IsRequired(false);


        builder.Property(e => e.BrandOfTransportMedium)
           .HasMaxLength(255)
         .IsRequired(false);

        builder.Property(e => e.BrandOfTransportMediumComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.BrandOfTransportMediumValidation)
            .IsRequired(false);


        builder.Property(e => e.DatabaseUploadedBy)
         .IsRequired(false);

        builder.Property(e => e.DatabaseUploadedByComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.DatabaseUploadedByValidation)
            .IsRequired(false);


        builder.Property(e => e.MaterialCollectedSpecimenTypesComment)
           .HasMaxLength(1000)
           .IsRequired(false);

        builder.Property(e => e.MaterialCollectedSpecimenTypesValidation)
            .IsRequired(false);


        builder.Property(e => e.CulturingResult)
      .IsRequired(false);

        builder.Property(e => e.CulturingResultComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.CulturingResultValidation)
            .IsRequired(false);



        builder.Property(e => e.CulturingResultDate)
             .IsRequired(false);

        builder.Property(e => e.CulturingResultDateComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.CulturingResultDateValidation)
            .IsRequired(false);


        builder.Property(e => e.QualityControlResult)
            .IsRequired(false);

        builder.Property(e => e.QualityControlResultComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.QualityControlResultValidation)
            .IsRequired(false);


        builder.Property(e => e.QualityControlResultDate)
            .IsRequired(false);

        builder.Property(e => e.QualityControlResultDateComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.QualityControlResultDateValidation)
            .IsRequired(false);


        builder.Property(e => e.GSDAnalysisResult)
             .IsRequired(false);

        builder.Property(e => e.GSDAnalysisResultComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.GSDAnalysisResultValidation)
            .IsRequired(false);


        builder.Property(e => e.GSDAnalysisResultDate)
            .IsRequired(false);

        builder.Property(e => e.GSDAnalysisResultDateComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.GSDAnalysisResultDateValidation)
            .IsRequired(false);

        builder.Property(e => e.GSDUploadingStatus)
            .IsRequired(false);

        builder.Property(e => e.GSDUploadingStatusComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.GSDUploadingStatusValidation)
            .IsRequired(false);


        builder.Property(e => e.GSDUploadingDate)
         .IsRequired(false);

        builder.Property(e => e.GSDUploadingDateComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.GSDUploadingDateValidation)
            .IsRequired(false);


        builder.Property(e => e.MaterialGSDInfoComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.MaterialGSDInfoValidation)
            .IsRequired(false);


        builder.Property(e => e.OwnerBioHubFacilityValidation)
           .IsRequired(false);


        builder.Property(e => e.OwnerBioHubFacilityComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.DateOfBMEPPReceiptValidation)
         .IsRequired(false);


        builder.Property(e => e.DateOfBMEPPReceiptComment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.ShipmentMaterialCondition)
          .IsRequired(false);
        builder.Property(e => e.ShipmentMaterialConditionNote)
            .HasMaxLength(255)
          .IsRequired(false);
        builder.Property(e => e.ShipmentMaterialConditionValidation)
          .IsRequired(false);
        builder.Property(e => e.ShipmentMaterialConditionComment)
            .HasMaxLength(1000)
          .IsRequired(false);
    }
}
