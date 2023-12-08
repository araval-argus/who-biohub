using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class WorklistToBioHubHistoryItemFeedbackConfiguration : IEntityTypeConfiguration<WorklistToBioHubHistoryItemFeedback>
{
    public static WorklistToBioHubHistoryItemFeedbackConfiguration Default => new();

    public void Configure(EntityTypeBuilder<WorklistToBioHubHistoryItemFeedback> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Text)
            .HasMaxLength(5000)
            .IsRequired(false);
        builder
             .HasOne(e => e.PostedBy)
             .WithMany(b => b.WorklistToBioHubHistoryItemFeedbacks)
             .HasForeignKey(e => e.PostedById)
             .HasPrincipalKey(e => e.Id)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired(false);

        builder
             .HasOne(e => e.WorklistToBioHubHistoryItem)
             .WithMany(b => b.WorklistToBioHubHistoryItemFeedbacks)
             .HasForeignKey(e => e.WorklistToBioHubHistoryItemId)
             .HasPrincipalKey(e => e.Id)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired(false);
    }
}
