using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistFromBioHubItemAnnex2OfSMTA2ConditionConfiguration : IEntityTypeConfiguration<WorklistFromBioHubItemAnnex2OfSMTA2Condition>
{
    public void Configure(EntityTypeBuilder<WorklistFromBioHubItemAnnex2OfSMTA2Condition> entity)
    {
        entity.HasKey(wd => new { wd.WorklistFromBioHubItemId, wd.Annex2OfSMTA2ConditionId });

        entity.Property(wd => wd.Flag)
            .IsRequired(false);


        entity.Property(wd => wd.Comment)
            .HasMaxLength(255)
            .IsRequired(false);

        entity.HasOne<WorklistFromBioHubItem>(wd => wd.WorklistFromBioHubItem)
            .WithMany(r => r.WorklistFromBioHubItemAnnex2OfSMTA2Conditions)
            .HasForeignKey(wd => wd.WorklistFromBioHubItemId);


        entity.HasOne<Annex2OfSMTA2Condition>(wd => wd.Annex2OfSMTA2Condition)
            .WithMany(d => d.WorklistFromBioHubItemAnnex2OfSMTA2Conditions)
            .HasForeignKey(wd => wd.Annex2OfSMTA2ConditionId);
    }

    public new static WorklistFromBioHubItemAnnex2OfSMTA2ConditionConfiguration Default => new WorklistFromBioHubItemAnnex2OfSMTA2ConditionConfiguration();
}
