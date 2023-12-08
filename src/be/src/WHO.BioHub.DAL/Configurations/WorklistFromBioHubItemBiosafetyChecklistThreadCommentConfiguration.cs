using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

internal class WorklistFromBioHubItemBiosafetyChecklistThreadCommentConfiguration : IEntityTypeConfiguration<WorklistFromBioHubItemBiosafetyChecklistThreadComment>
{
    public static WorklistFromBioHubItemBiosafetyChecklistThreadCommentConfiguration Default => new();

    public void Configure(EntityTypeBuilder<WorklistFromBioHubItemBiosafetyChecklistThreadComment> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Text)
            .HasMaxLength(5000)
            .IsRequired(false);
        builder
             .HasOne(e => e.PostedBy)
             .WithMany(b => b.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments)
             .HasForeignKey(e => e.PostedById)
             .HasPrincipalKey(e => e.Id)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired(false);

        builder
             .HasOne(e => e.WorklistFromBioHubItem)
             .WithMany(b => b.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Comments)
             .HasForeignKey(e => e.WorklistFromBioHubItemId)
             .HasPrincipalKey(e => e.Id)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired(false);
    }
}
