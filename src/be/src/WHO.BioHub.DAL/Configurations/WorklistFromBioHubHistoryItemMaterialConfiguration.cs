using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistFromBioHubHistoryItemMaterialConfiguration : IEntityTypeConfiguration<WorklistFromBioHubHistoryItemMaterial>
{
    public void Configure(EntityTypeBuilder<WorklistFromBioHubHistoryItemMaterial> entity)
    {
        entity.HasKey(wd => new { wd.WorklistFromBioHubHistoryItemId, wd.MaterialId });

        entity.Property(wd => wd.Quantity)
            .IsRequired(false);

        entity.Property(wd => wd.Amount)
            .IsRequired(false);

        entity.Property(wd => wd.Note)
            .HasMaxLength(255)
            .IsRequired(false);

        entity.HasOne<WorklistFromBioHubHistoryItem>(wd => wd.WorklistFromBioHubHistoryItem)
            .WithMany(r => r.WorklistFromBioHubHistoryItemMaterials)
            .HasForeignKey(wd => wd.WorklistFromBioHubHistoryItemId);


        entity.HasOne<Material>(wd => wd.Material)
            .WithMany(d => d.WorklistFromBioHubHistoryItemMaterials)
            .HasForeignKey(wd => wd.MaterialId);
    }

    public new static WorklistFromBioHubHistoryItemMaterialConfiguration Default => new WorklistFromBioHubHistoryItemMaterialConfiguration();
}
