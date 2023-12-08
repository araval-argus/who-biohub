using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class TemperatureUnitOfMeasureConfiguration : IEntityTypeConfiguration<TemperatureUnitOfMeasure>
{
    public static TemperatureUnitOfMeasureConfiguration Default => new();

    public void Configure(EntityTypeBuilder<TemperatureUnitOfMeasure> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.Name)
            .HasDatabaseName("IX_Name");

        builder.Property(e => e.Name)
            .HasMaxLength(255);

        builder.Property(e => e.Unit)
            .HasMaxLength(255);

    }
}
