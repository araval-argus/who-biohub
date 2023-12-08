using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class LaboratoryConfiguration : IEntityTypeConfiguration<Laboratory>
{
    public static LaboratoryConfiguration Default => new();

    public void Configure(EntityTypeBuilder<Laboratory> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.Name)
            .HasDatabaseName("IX_Name");

        builder.HasIndex(e => e.Abbreviation)
            .HasDatabaseName("IX_Abbreviation");

        builder.HasIndex(e => e.IsPublicFacing)
           .HasDatabaseName("IX_IsPublicFacing");

        builder.Property(e => e.Abbreviation)
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired(false);


        builder.Property(e => e.Name)
            .HasMaxLength(255);

        builder.Property(e => e.Description)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.Address)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder
            .HasOne(e => e.BSLLevel)
            .WithMany(b => b.Laboratories)
            .HasForeignKey(e => e.BSLLevelId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.Country)
            .WithMany(b => b.Laboratories)
            .HasForeignKey(e => e.CountryId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.Property(e => e.Latitude)
            .IsRequired(false);

        builder.Property(e => e.Longitude)
            .IsRequired(false);
    }
}
