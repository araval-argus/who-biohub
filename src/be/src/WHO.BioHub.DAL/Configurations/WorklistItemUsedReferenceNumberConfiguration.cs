using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class WorklistItemUsedReferenceNumberConfiguration : IEntityTypeConfiguration<WorklistItemUsedReferenceNumber>
{
    public static WorklistItemUsedReferenceNumberConfiguration Default => new();

    public void Configure(EntityTypeBuilder<WorklistItemUsedReferenceNumber> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.ReferenceNumber)
            .HasDatabaseName("IX_ReferenceNumber")
           .IsUnique();

    }
}
