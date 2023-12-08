using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class MaterialLaboratoryAnalysisInformationHistoryConfiguration : IEntityTypeConfiguration<MaterialLaboratoryAnalysisInformationHistory>
{
    public static MaterialLaboratoryAnalysisInformationHistoryConfiguration Default => new();

    public void Configure(EntityTypeBuilder<MaterialLaboratoryAnalysisInformationHistory> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.MaterialNumber)
            .HasDatabaseName("IX_MaterialNumber");        

        builder.Property(e => e.MaterialNumber)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.VirusConcentration)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.CulturingCellLine)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.TypeOfTransportMedium)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.BrandOfTransportMedium)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.AccessionNumberInGSDDatabase)
              .HasMaxLength(255)
              .IsRequired(false);

        builder.Property(e => e.GSDUploadedToDatabase)
           .IsRequired(false);

        builder
          .HasOne(e => e.DatabaseUsedForGSDUploading)
          .WithMany(b => b.MaterialLaboratoryAnalysisInformationHistory)
          .HasForeignKey(e => e.DatabaseUsedForGSDUploadingId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.MaterialShippingInformationHistory)
          .WithMany(b => b.MaterialLaboratoryAnalysisInformationHistory)
          .HasForeignKey(e => e.MaterialShippingInformationHistoryId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.UnitOfMeasure)
          .WithMany(b => b.MaterialLaboratoryAnalysisInformationHistory)
          .HasForeignKey(e => e.UnitOfMeasureId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);


    }
}
