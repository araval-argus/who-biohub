using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class WorklistFromBioHubHistoryItemConfiguration : IEntityTypeConfiguration<WorklistFromBioHubHistoryItem>
{
    public static WorklistFromBioHubHistoryItemConfiguration Default => new();

    public void Configure(EntityTypeBuilder<WorklistFromBioHubHistoryItem> builder)
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

        builder.Property(e => e.BiosafetyChecklistApprovalComment)
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

        builder.Property(e => e.SavedBiosafetyChecklistThreadComment)
          .HasMaxLength(1000)
          .IsRequired(false);

        //# 54317
        builder.Property(e => e.Annex2OfSMTA2SignatureText)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.BookingFormOfSMTA2SignatureText)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(e => e.BiosafetyChecklistOfSMTA2SignatureText)
            .HasMaxLength(255)
            .IsRequired(false);
        ///////

        builder
            .HasOne(e => e.RequestInitiationFromBioHubFacility)
            .WithMany(b => b.WorklistFromBioHubHistoryItems)
            .HasForeignKey(e => e.RequestInitiationFromBioHubFacilityId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.RequestInitiationToLaboratory)
            .WithMany(b => b.WorklistFromBioHubHistoryItems)
            .HasForeignKey(e => e.RequestInitiationToLaboratoryId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.LastOperationUser)
            .WithMany(b => b.WorklistFromBioHubHistoryItems)
            .HasForeignKey(e => e.LastOperationUserId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder
            .HasOne(e => e.WorklistFromBioHubItem)
            .WithMany(b => b.WorklistFromBioHubHistoryItems)
            .HasForeignKey(e => e.WorklistFromBioHubItemId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(true);
    }
}
