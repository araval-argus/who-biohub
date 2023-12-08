﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class WorklistFromBioHubHistoryItemFeedbackConfiguration : IEntityTypeConfiguration<WorklistFromBioHubHistoryItemFeedback>
{
    public static WorklistFromBioHubHistoryItemFeedbackConfiguration Default => new();

    public void Configure(EntityTypeBuilder<WorklistFromBioHubHistoryItemFeedback> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Text)
            .HasMaxLength(5000)
            .IsRequired(false);
        builder
             .HasOne(e => e.PostedBy)
             .WithMany(b => b.WorklistFromBioHubHistoryItemFeedbacks)
             .HasForeignKey(e => e.PostedById)
             .HasPrincipalKey(e => e.Id)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired(false);

        builder
             .HasOne(e => e.WorklistFromBioHubHistoryItem)
             .WithMany(b => b.WorklistFromBioHubHistoryItemFeedbacks)
             .HasForeignKey(e => e.WorklistFromBioHubHistoryItemId)
             .HasPrincipalKey(e => e.Id)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired(false);
    }
}
