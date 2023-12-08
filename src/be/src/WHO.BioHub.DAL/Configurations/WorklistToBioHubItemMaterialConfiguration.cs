using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistToBioHubItemMaterialConfiguration : IEntityTypeConfiguration<WorklistToBioHubItemMaterial>
{
    public void Configure(EntityTypeBuilder<WorklistToBioHubItemMaterial> entity)
    {
        entity.HasKey(wd => new { wd.WorklistToBioHubItemId, wd.MaterialId });


        entity.HasOne<WorklistToBioHubItem>(wd => wd.WorklistToBioHubItem)
            .WithMany(r => r.WorklistToBioHubItemMaterials)
            .HasForeignKey(wd => wd.WorklistToBioHubItemId);


        entity.HasOne<Material>(wd => wd.Material)
            .WithMany(d => d.WorklistToBioHubItemMaterials)
            .HasForeignKey(wd => wd.MaterialId);
    }

    public new static WorklistToBioHubItemMaterialConfiguration Default => new WorklistToBioHubItemMaterialConfiguration();
}
