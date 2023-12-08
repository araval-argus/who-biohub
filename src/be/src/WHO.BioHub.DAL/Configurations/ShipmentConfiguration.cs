using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public static ShipmentConfiguration Default => new();

    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        //builder.HasIndex(e => e.Name)
        //    .HasDatabaseName("IX_Name");        

        builder.Property(e => e.ReferenceNumber)
            .HasMaxLength(255);

        builder.Property(e => e.StatusOfRequest)
            .IsRequired(false)
            .HasMaxLength(255);

        //builder
        //  .HasOne(e => e.Material)
        //  .WithMany(b => b.Shipments)
        //  .HasForeignKey(e => e.MaterialId)
        //  .HasPrincipalKey(e => e.Id)
        //  .OnDelete(DeleteBehavior.Restrict)
        //  .IsRequired(false);

        //builder
        //  .HasOne(e => e.UnitOfMeasure)
        //  .WithMany(b => b.Shipments)
        //  .HasForeignKey(e => e.UnitOfMeasureId)
        //  .HasPrincipalKey(e => e.Id)
        //  .OnDelete(DeleteBehavior.Restrict)
        //  .IsRequired(false);

        //builder
        //  .HasOne(e => e.PriorityRequestType)
        //  .WithMany(b => b.Shipments)
        //  .HasForeignKey(e => e.PriorityRequestTypeId)
        //  .HasPrincipalKey(e => e.Id)
        //  .OnDelete(DeleteBehavior.Restrict)
        //  .IsRequired(false);

        //builder
        //  .HasOne(e => e.TransportMode)
        //  .WithMany(b => b.Shipments)
        //  .HasForeignKey(e => e.TransportModeId)
        //  .HasPrincipalKey(e => e.Id)
        //  .OnDelete(DeleteBehavior.Restrict)
        //  .IsRequired(false);

        builder
          .HasOne(e => e.QELaboratory)
          .WithMany(b => b.Shipments)
          .HasForeignKey(e => e.QELaboratoryId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.BioHubFacility)
          .WithMany(b => b.Shipments)
          .HasForeignKey(e => e.BioHubFacilityId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.WorklistToBioHubItem)
          .WithMany(b => b.Shipments)
          .HasForeignKey(e => e.WorklistToBioHubItemId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);

        builder
          .HasOne(e => e.WorklistFromBioHubItem)
          .WithMany(b => b.Shipments)
          .HasForeignKey(e => e.WorklistFromBioHubItemId)
          .HasPrincipalKey(e => e.Id)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired(false);


    }
}
