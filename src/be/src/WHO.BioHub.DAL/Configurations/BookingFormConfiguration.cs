using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class BookingFormConfiguration : IEntityTypeConfiguration<BookingForm>
{
    public static BookingFormConfiguration Default => new();

    public void Configure(EntityTypeBuilder<BookingForm> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.TotalAmount)            
            .IsRequired(false);

        builder.Property(e => e.TotalNumberOfVials)
            .IsRequired(false);

        builder.Property(e => e.NumberOfInnerPackagingAndSize)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.Date)
            .IsRequired(false);

        builder.Property(e => e.RequestDateOfPickup)
            .IsRequired(false);

        builder.Property(e => e.TemperatureTransportCondition)
            .IsRequired(false);

        builder.Property(e => e.EstimateDateOfPickup)
            .IsRequired(false);

        builder.Property(e => e.DateOfPickup)
            .IsRequired(false);

        builder.Property(e => e.DateOfDelivery)
            .IsRequired(false);

        builder.Property(e => e.ShipmentReferenceNumber)
            .HasMaxLength(255)
            .IsRequired(false);

        builder
          .HasOne(e => e.WorklistToBioHubItem)
          .WithMany(b => b.BookingForms)
          .HasForeignKey(e => e.WorklistToBioHubItemId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.WorklistFromBioHubItem)
          .WithMany(b => b.BookingForms)
          .HasForeignKey(e => e.WorklistFromBioHubItemId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.MaterialProduct)
          .WithMany(b => b.BookingForms)
          .HasForeignKey(e => e.MaterialProductId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.TransportCategory)
          .WithMany(b => b.BookingForms)
          .HasForeignKey(e => e.TransportCategoryId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.TransportMode)
          .WithMany(b => b.BookingForms)
          .HasForeignKey(e => e.TransportModeId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
         .HasOne(e => e.Courier)
         .WithMany(b => b.BookingForms)
         .HasForeignKey(e => e.CourierId)
         .HasPrincipalKey(e => e.Id)
         .OnDelete(DeleteBehavior.Restrict)
         .IsRequired(false);

    }
}
