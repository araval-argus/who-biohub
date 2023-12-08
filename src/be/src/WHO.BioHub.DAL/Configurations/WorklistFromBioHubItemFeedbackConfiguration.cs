using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class WorklistFromBioHubItemFeedbackConfiguration : IEntityTypeConfiguration<WorklistFromBioHubItemFeedback>
{
    public static WorklistFromBioHubItemFeedbackConfiguration Default => new();

    public void Configure(EntityTypeBuilder<WorklistFromBioHubItemFeedback> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Text)
            .HasMaxLength(5000)
            .IsRequired(false);
        builder
             .HasOne(e => e.PostedBy)
             .WithMany(b => b.WorklistFromBioHubItemFeedbacks)
             .HasForeignKey(e => e.PostedById)
             .HasPrincipalKey(e => e.Id)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired(false);

        builder
             .HasOne(e => e.WorklistFromBioHubItem)
             .WithMany(b => b.WorklistFromBioHubItemFeedbacks)
             .HasForeignKey(e => e.WorklistFromBioHubItemId)
             .HasPrincipalKey(e => e.Id)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired(false);
    }
}
