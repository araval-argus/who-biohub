using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class MaterialClinicalDetailHistoryConfiguration : IEntityTypeConfiguration<MaterialClinicalDetailHistory>
{
    public static MaterialClinicalDetailHistoryConfiguration Default => new();

    public void Configure(EntityTypeBuilder<MaterialClinicalDetailHistory> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.MaterialNumber)
            .HasDatabaseName("IX_MaterialNumber");
    

        builder.Property(e => e.MaterialNumber)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.PatientStatus)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.Note)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.Condition)
            .IsRequired(false);

        builder.Property(e => e.Gender)
           .IsRequired(false);

        builder
          .HasOne(e => e.MaterialShippingInformationHistory)
          .WithMany(b => b.MaterialClinicalDetailsHistory)
          .HasForeignKey(e => e.MaterialShippingInformationHistoryId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.IsolationHostType)
          .WithMany(b => b.MaterialClinicalDetailsHistory)
          .HasForeignKey(e => e.IsolationHostTypeId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);
    }
}
