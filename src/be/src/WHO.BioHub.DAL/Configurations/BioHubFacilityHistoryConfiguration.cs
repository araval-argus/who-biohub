using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;


internal class BioHubFacilityHistoryConfiguration : IEntityTypeConfiguration<BioHubFacilityHistory>
{
    public static BioHubFacilityHistoryConfiguration Default => new();

    public void Configure(EntityTypeBuilder<BioHubFacilityHistory> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
             

        builder.HasIndex(e => e.LastOperationDate)
            .HasDatabaseName("IX_LastOperationDate");

        builder.HasIndex(e => e.BioHubFacilityId)
           .HasDatabaseName("IX_BioHubFacilityId");

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
            .WithMany(b => b.BioHubFacilitiesHistory)
            .HasForeignKey(e => e.BSLLevelId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.Country)
            .WithMany(b => b.BioHubFacilitiesHistory)
            .HasForeignKey(e => e.CountryId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.Property(e => e.Latitude)
           ;

        builder.Property(e => e.Longitude)
            ;

        builder
            .HasOne(e => e.BioHubFacility)
            .WithMany(b => b.BioHubFacilitiesHistory)
            .HasForeignKey(e => e.BioHubFacilityId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}

