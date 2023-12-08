using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class BioHubFacilityConfiguration : IEntityTypeConfiguration<BioHubFacility>
{
    public static BioHubFacilityConfiguration Default => new();

    public void Configure(EntityTypeBuilder<BioHubFacility> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.Id)
            .HasDatabaseName("IX_Id")
            .IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.Name)
            .HasDatabaseName("IX_Name");

        builder.HasIndex(e => e.Abbreviation)
            .HasDatabaseName("IX_Abbreviation");

        builder.Property(e => e.Abbreviation)
            .HasMaxLength(20)
            .IsUnicode(false);


        builder.Property(e => e.Name)
            .HasMaxLength(255);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.Address)
            .HasMaxLength(1000);

        builder
                .HasOne(e => e.BSLLevel)
                .WithMany(b => b.BioHubFacilities)
                .HasForeignKey(e => e.BSLLevelId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

        builder
            .HasOne(e => e.Country)
            .WithMany(b => b.BioHubFacilities)
            .HasForeignKey(e => e.CountryId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasIndex(e => e.IsPublicFacing)
          .HasDatabaseName("IX_IsPublicFacing");
    }
}
