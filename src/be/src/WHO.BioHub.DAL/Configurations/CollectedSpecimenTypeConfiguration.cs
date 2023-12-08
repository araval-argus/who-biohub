using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class CollectedSpecimenTypeConfiguration : IEntityTypeConfiguration<CollectedSpecimenType>
{
    public void Configure(EntityTypeBuilder<CollectedSpecimenType> entity)
    {
        entity.HasKey(wd => new { wd.MaterialLaboratoryAnalysisInformationId, wd.SpecimenTypeId });

        entity.HasOne<MaterialLaboratoryAnalysisInformation>(wd => wd.MaterialLaboratoryAnalysisInformation)
            .WithMany(r => r.CollectedSpecimenTypes)
            .HasForeignKey(wd => wd.MaterialLaboratoryAnalysisInformationId);

        entity.HasOne<SpecimenType>(wd => wd.SpecimenType)
            .WithMany(d => d.CollectedSpecimenTypes)
            .HasForeignKey(wd => wd.SpecimenTypeId);
    }

    public new static CollectedSpecimenTypeConfiguration Default => new CollectedSpecimenTypeConfiguration();
}
