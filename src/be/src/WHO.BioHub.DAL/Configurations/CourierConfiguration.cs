using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class CourierConfiguration : IEntityTypeConfiguration<Courier>
{
    public static CourierConfiguration Default => new();

    public void Configure(EntityTypeBuilder<Courier> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => e.Name)
            .HasDatabaseName("IX_Name");


        builder.Property(e => e.WHOAccountNumber)
            .HasMaxLength(255)
            .IsUnicode(false)
            .IsRequired(false);

        builder.Property(e => e.Email)
            .HasMaxLength(255);

        builder.Property(e => e.BusinessPhone)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.Name)
            .HasMaxLength(255);

        builder.Property(e => e.Description)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(e => e.Address)
            .HasMaxLength(255)
            .IsRequired(false);


        builder
            .HasOne(e => e.Country)
            .WithMany(b => b.Couriers)
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
