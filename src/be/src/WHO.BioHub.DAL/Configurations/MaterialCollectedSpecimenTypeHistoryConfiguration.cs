using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class MaterialCollectedSpecimenTypeHistoryConfiguration : IEntityTypeConfiguration<MaterialCollectedSpecimenTypeHistory>
{
    public void Configure(EntityTypeBuilder<MaterialCollectedSpecimenTypeHistory> entity)
    {
        entity.HasKey(wd => new { wd.MaterialHistoryId, wd.SpecimenTypeId });

        entity.HasOne<MaterialHistory>(wd => wd.MaterialHistory)
            .WithMany(r => r.MaterialCollectedSpecimenTypesHistory)
            .HasForeignKey(wd => wd.MaterialHistoryId);

        entity.HasOne<SpecimenType>(wd => wd.SpecimenType)
            .WithMany(d => d.MaterialCollectedSpecimenTypesHistory)
            .HasForeignKey(wd => wd.SpecimenTypeId);
    }

    public new static MaterialCollectedSpecimenTypeHistoryConfiguration Default => new MaterialCollectedSpecimenTypeHistoryConfiguration();
}
