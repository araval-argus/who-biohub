using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistFromBioHubItemMaterialConfiguration : IEntityTypeConfiguration<WorklistFromBioHubItemMaterial>
{
    public void Configure(EntityTypeBuilder<WorklistFromBioHubItemMaterial> entity)
    {
        entity.HasKey(wd => new { wd.WorklistFromBioHubItemId, wd.MaterialId });

        entity.Property(wd => wd.Quantity)
            .IsRequired(false);

        entity.Property(wd => wd.Amount)
            .IsRequired(false);

        entity.Property(wd => wd.Note)
            .HasMaxLength(255)
            .IsRequired(false);

        entity.HasOne<WorklistFromBioHubItem>(wd => wd.WorklistFromBioHubItem)
            .WithMany(r => r.WorklistFromBioHubItemMaterials)
            .HasForeignKey(wd => wd.WorklistFromBioHubItemId);


        entity.HasOne<Material>(wd => wd.Material)
            .WithMany(d => d.WorklistFromBioHubItemMaterials)
            .HasForeignKey(wd => wd.MaterialId);
    }

    public new static WorklistFromBioHubItemMaterialConfiguration Default => new WorklistFromBioHubItemMaterialConfiguration();
}
