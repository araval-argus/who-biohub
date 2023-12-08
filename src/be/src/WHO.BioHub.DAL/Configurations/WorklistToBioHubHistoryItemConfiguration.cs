using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class WorklistToBioHubHistoryItemConfiguration : IEntityTypeConfiguration<WorklistToBioHubHistoryItem>
{
    public static WorklistToBioHubHistoryItemConfiguration Default => new();

    public void Configure(EntityTypeBuilder<WorklistToBioHubHistoryItem> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Comment)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_Status");

        builder.Property(e => e.Annex2Comment)
           .HasMaxLength(1000)
           .IsRequired(false);

        builder.Property(e => e.Annex2ApprovalComment)
           .HasMaxLength(1000)
           .IsRequired(false);

        builder.Property(e => e.BookingFormApprovalComment)
         .HasMaxLength(1000)
         .IsRequired(false);

        builder.Property(e => e.WaitForArrivalConditionCheckApprovalComment)
         .HasMaxLength(1000)
         .IsRequired(false);

        builder.Property(e => e.WHODocumentRegistrationNumber)
         .HasMaxLength(255)
         .IsRequired(false);

        builder.Property(e => e.ReferenceNumber)
        .HasMaxLength(255)
        .IsRequired(false);

        //# 54317
        builder.Property(e => e.Annex2OfSMTA1SignatureText)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.BookingFormOfSMTA1SignatureText)
            .HasMaxLength(255)
            .IsRequired(false);
        ///////

        builder
            .HasOne(e => e.RequestInitiationToBioHubFacility)
            .WithMany(b => b.WorklistToBioHubHistoryItems)
            .HasForeignKey(e => e.RequestInitiationToBioHubFacilityId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.RequestInitiationFromLaboratory)
            .WithMany(b => b.WorklistToBioHubHistoryItems)
            .HasForeignKey(e => e.RequestInitiationFromLaboratoryId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.LastOperationUser)
            .WithMany(b => b.WorklistToBioHubHistoryItems)
            .HasForeignKey(e => e.LastOperationUserId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.WorklistToBioHubItem)
            .WithMany(b => b.WorklistToBioHubHistoryItems)
            .HasForeignKey(e => e.WorklistToBioHubItemId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(true);
    }
}
