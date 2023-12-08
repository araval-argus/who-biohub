using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2Configuration : IEntityTypeConfiguration<WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2>
{
    public void Configure(EntityTypeBuilder<WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2> entity)
    {
        entity.HasKey(wd => new { wd.WorklistFromBioHubHistoryItemId, wd.BiosafetyChecklistOfSMTA2Id });

        entity.Property(wd => wd.Flag)
            .IsRequired(false);

        entity.HasOne<WorklistFromBioHubHistoryItem>(wd => wd.WorklistFromBioHubHistoryItem)
            .WithMany(r => r.WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s)
            .HasForeignKey(wd => wd.WorklistFromBioHubHistoryItemId);


        entity.HasOne<BiosafetyChecklistOfSMTA2>(wd => wd.BiosafetyChecklistOfSMTA2)
            .WithMany(d => d.WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2s)
            .HasForeignKey(wd => wd.BiosafetyChecklistOfSMTA2Id);
    }

    public new static WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2Configuration Default => new WorklistFromBioHubHistoryItemBiosafetyChecklistOfSMTA2Configuration();
}
