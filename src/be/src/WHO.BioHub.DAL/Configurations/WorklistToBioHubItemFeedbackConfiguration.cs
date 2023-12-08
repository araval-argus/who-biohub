using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class WorklistToBioHubItemFeedbackConfiguration : IEntityTypeConfiguration<WorklistToBioHubItemFeedback>
{
    public static WorklistToBioHubItemFeedbackConfiguration Default => new();

    public void Configure(EntityTypeBuilder<WorklistToBioHubItemFeedback> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Text)
            .HasMaxLength(5000)
            .IsRequired(false);
        builder
             .HasOne(e => e.PostedBy)
             .WithMany(b => b.WorklistToBioHubItemFeedbacks)
             .HasForeignKey(e => e.PostedById)
             .HasPrincipalKey(e => e.Id)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired(false);

        builder
             .HasOne(e => e.WorklistToBioHubItem)
             .WithMany(b => b.WorklistToBioHubItemFeedbacks)
             .HasForeignKey(e => e.WorklistToBioHubItemId)
             .HasPrincipalKey(e => e.Id)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired(false);
    }
}
