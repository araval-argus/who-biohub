using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;


internal class LaboratoryHistoryConfiguration : IEntityTypeConfiguration<LaboratoryHistory>
{
    public static LaboratoryHistoryConfiguration Default => new();

    public void Configure(EntityTypeBuilder<LaboratoryHistory> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();        

        builder.HasIndex(e => e.LastOperationDate)
            .HasDatabaseName("IX_LastOperationDate");

        builder.HasIndex(e => e.LaboratoryId)
           .HasDatabaseName("IX_LaboratoryId");

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
            .WithMany(b => b.LaboratoriesHistory)
            .HasForeignKey(e => e.BSLLevelId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.Country)
            .WithMany(b => b.LaboratoriesHistory)
            .HasForeignKey(e => e.CountryId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.Property(e => e.Latitude)
            .IsRequired(false);

        builder.Property(e => e.Longitude)
            .IsRequired(false);

        builder
            .HasOne(e => e.Laboratory)
            .WithMany(b => b.LaboratoriesHistory)
            .HasForeignKey(e => e.LaboratoryId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}

