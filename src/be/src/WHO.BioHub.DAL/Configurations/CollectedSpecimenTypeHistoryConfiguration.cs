using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class CollectedSpecimenTypeHistoryConfiguration : IEntityTypeConfiguration<CollectedSpecimenTypeHistory>
{
    public void Configure(EntityTypeBuilder<CollectedSpecimenTypeHistory> entity)
    {
        entity.HasKey(wd => new { wd.MaterialLaboratoryAnalysisInformationHistoryId, wd.SpecimenTypeId });

        entity.HasOne<MaterialLaboratoryAnalysisInformationHistory>(wd => wd.MaterialLaboratoryAnalysisInformationHistory)
            .WithMany(r => r.CollectedSpecimenTypes)
            .HasForeignKey(wd => wd.MaterialLaboratoryAnalysisInformationHistoryId);

        entity.HasOne<SpecimenType>(wd => wd.SpecimenType)
            .WithMany(d => d.CollectedSpecimenTypesHistory)
            .HasForeignKey(wd => wd.SpecimenTypeId);
    }

    public new static CollectedSpecimenTypeHistoryConfiguration Default => new CollectedSpecimenTypeHistoryConfiguration();
}
