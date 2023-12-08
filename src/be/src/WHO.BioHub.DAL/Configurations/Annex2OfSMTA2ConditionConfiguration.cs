using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class Annex2OfSMTA2ConditionConfiguration : IEntityTypeConfiguration<Annex2OfSMTA2Condition>
{
    public static Annex2OfSMTA2ConditionConfiguration Default => new();

    public void Configure(EntityTypeBuilder<Annex2OfSMTA2Condition> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.Current)
            .HasDatabaseName("IX_Current");

        builder.Property(e => e.PointNumber)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.Condition)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.Current)
            .IsRequired(true);

        builder.Property(e => e.FlagPresetValue)
           .IsRequired(false);

    }
}
