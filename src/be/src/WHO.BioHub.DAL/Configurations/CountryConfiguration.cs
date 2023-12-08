using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public static CountryConfiguration Default => new();

    public void Configure(EntityTypeBuilder<Country> builder)
    {
        //builder.HasKey(e => e.Uncode);
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.Uncode)
            .HasDatabaseName("IX_Uncode")
            .IsUnique();

        builder.HasIndex(e => e.Id)
            .HasDatabaseName("IX_Id")
            .IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.Iso2)
            .HasDatabaseName("IX_Iso2");

        builder.HasIndex(e => e.Iso3)
            .HasDatabaseName("IX_Iso3");

        builder.HasIndex(e => e.Name)
            .HasDatabaseName("IX_Name");

        builder.Property(e => e.Uncode)
            .HasMaxLength(3)
            .IsUnicode(false);

        builder.Property(e => e.Iso2)
            .HasMaxLength(2)
            .IsUnicode(false);

        builder.Property(e => e.Iso3)
            .HasMaxLength(3)
            .IsUnicode(false);

        builder.Property(e => e.Name)
            .HasMaxLength(255);

        builder.Property(e => e.FullName)
            .HasMaxLength(255);

        builder.Property(e => e.GmtHour)
            .HasDefaultValueSql("((0))");

        builder.Property(e => e.GmtMin)
            .HasDefaultValueSql("((0))");
    }
}
