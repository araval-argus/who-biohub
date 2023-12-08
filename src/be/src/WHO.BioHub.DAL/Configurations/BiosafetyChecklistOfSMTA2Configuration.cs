using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class BiosafetyChecklistOfSMTA2Configuration : IEntityTypeConfiguration<BiosafetyChecklistOfSMTA2>
{
    public static BiosafetyChecklistOfSMTA2Configuration Default => new();

    public void Configure(EntityTypeBuilder<BiosafetyChecklistOfSMTA2> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.Current)
            .HasDatabaseName("IX_Current");

        builder.Property(e => e.Condition)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.Current)
            .IsRequired(true);

        builder.Property(e => e.FlagPresetValue)
            .IsRequired(false);

    }
}
