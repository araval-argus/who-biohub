using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public static DocumentConfiguration Default => new();

    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Base64String)
           .IsRequired(false);


        builder.HasIndex(e => e.Type)
            .HasDatabaseName("IX_Type");

        builder.HasIndex(e => e.Approved)
            .HasDatabaseName("IX_Approved");

        builder.HasIndex(e => e.LaboratoryId)
            .HasDatabaseName("IX_LaboratoryId");

        builder.HasIndex(e => e.BioHubFacilityId)
            .HasDatabaseName("IX_BioHubFacilityId");

        builder.HasIndex(e => e.IsDocumentFile)
            .HasDatabaseName("IX_IsDocumentFile");

        builder
            .HasOne(e => e.OriginalDocumentTemplate)
            .WithMany(b => b.Documents)
            .HasForeignKey(e => e.OriginalDocumentTemplateId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.Laboratory)
            .WithMany(b => b.Documents)
            .HasForeignKey(e => e.LaboratoryId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
           .HasOne(e => e.BioHubFacility)
           .WithMany(b => b.Documents)
           .HasForeignKey(e => e.BioHubFacilityId)
           .HasPrincipalKey(e => e.Id)
           .OnDelete(DeleteBehavior.Restrict)
           .IsRequired(false);

    }
}
