using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class MaterialCollectedSpecimenTypeConfiguration : IEntityTypeConfiguration<MaterialCollectedSpecimenType>
{
    public void Configure(EntityTypeBuilder<MaterialCollectedSpecimenType> entity)
    {
        entity.HasKey(wd => new { wd.MaterialId, wd.SpecimenTypeId });

        entity.HasOne<Material>(wd => wd.Material)
            .WithMany(r => r.MaterialCollectedSpecimenTypes)
            .HasForeignKey(wd => wd.MaterialId);

        entity.HasOne<SpecimenType>(wd => wd.SpecimenType)
            .WithMany(d => d.MaterialCollectedSpecimenTypes)
            .HasForeignKey(wd => wd.SpecimenTypeId);
    }

    public new static MaterialCollectedSpecimenTypeConfiguration Default => new MaterialCollectedSpecimenTypeConfiguration();
}
