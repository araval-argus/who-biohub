using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Configuration : IEntityTypeConfiguration<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2>
{
    public void Configure(EntityTypeBuilder<WorklistFromBioHubItemBiosafetyChecklistOfSMTA2> entity)
    {
        entity.HasKey(wd => new { wd.WorklistFromBioHubItemId, wd.BiosafetyChecklistOfSMTA2Id });

        entity.Property(wd => wd.Flag)
            .IsRequired(false);

        entity.HasOne<WorklistFromBioHubItem>(wd => wd.WorklistFromBioHubItem)
            .WithMany(r => r.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s)
            .HasForeignKey(wd => wd.WorklistFromBioHubItemId);


        entity.HasOne<BiosafetyChecklistOfSMTA2>(wd => wd.BiosafetyChecklistOfSMTA2)
            .WithMany(d => d.WorklistFromBioHubItemBiosafetyChecklistOfSMTA2s)
            .HasForeignKey(wd => wd.BiosafetyChecklistOfSMTA2Id);
    }

    public new static WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Configuration Default => new WorklistFromBioHubItemBiosafetyChecklistOfSMTA2Configuration();
}
