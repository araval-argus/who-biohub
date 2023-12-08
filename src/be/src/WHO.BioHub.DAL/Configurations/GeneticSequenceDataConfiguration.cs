using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class GeneticSequenceDataConfiguration : IEntityTypeConfiguration<GeneticSequenceData>
{
    public static GeneticSequenceDataConfiguration Default => new();

    public void Configure(EntityTypeBuilder<GeneticSequenceData> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.Name)
            .HasDatabaseName("IX_Name");

        builder.HasIndex(e => e.Code)
            .HasDatabaseName("IX_Code");

        builder.Property(e => e.Code)
            .HasMaxLength(255);

        builder.Property(e => e.Name)
            .HasMaxLength(255);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

    }
}
