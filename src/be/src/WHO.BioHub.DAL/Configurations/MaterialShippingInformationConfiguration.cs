using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class MaterialShippingInformationConfiguration : IEntityTypeConfiguration<MaterialShippingInformation>
{
    public static MaterialShippingInformationConfiguration Default => new();

    public void Configure(EntityTypeBuilder<MaterialShippingInformation> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Condition)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.AdditionalInformation)
            .HasMaxLength(1000)
            .IsRequired(false);
        

        builder.Property(e => e.MaterialNumber)
            .HasMaxLength(255)
            .IsRequired(false);

        builder
          .HasOne(e => e.WorklistToBioHubItem)
          .WithMany(b => b.MaterialShippingInformations)
          .HasForeignKey(e => e.WorklistToBioHubItemId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.MaterialProduct)
          .WithMany(b => b.MaterialShippingInformations)
          .HasForeignKey(e => e.MaterialProductId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.TransportCategory)
          .WithMany(b => b.MaterialShippingInformations)
          .HasForeignKey(e => e.TransportCategoryId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

    }
}
