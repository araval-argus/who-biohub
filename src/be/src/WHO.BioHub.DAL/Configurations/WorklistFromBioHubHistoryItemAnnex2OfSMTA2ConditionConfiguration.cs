using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistFromBioHubHistoryItemAnnex2OfSMTA2ConditionConfiguration : IEntityTypeConfiguration<WorklistFromBioHubHistoryItemAnnex2OfSMTA2Condition>
{
    public void Configure(EntityTypeBuilder<WorklistFromBioHubHistoryItemAnnex2OfSMTA2Condition> entity)
    {
        entity.HasKey(wd => new { wd.WorklistFromBioHubHistoryItemId, wd.Annex2OfSMTA2ConditionId });

        entity.Property(wd => wd.Flag)
            .IsRequired(false);

        entity.Property(wd => wd.Comment)
            .HasMaxLength(255)
            .IsRequired(false);

        entity.HasOne<WorklistFromBioHubHistoryItem>(wd => wd.WorklistFromBioHubHistoryItem)
            .WithMany(r => r.WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions)
            .HasForeignKey(wd => wd.WorklistFromBioHubHistoryItemId);


        entity.HasOne<Annex2OfSMTA2Condition>(wd => wd.Annex2OfSMTA2Condition)
            .WithMany(d => d.WorklistFromBioHubHistoryItemAnnex2OfSMTA2Conditions)
            .HasForeignKey(wd => wd.Annex2OfSMTA2ConditionId);
    }

    public new static WorklistFromBioHubHistoryItemAnnex2OfSMTA2ConditionConfiguration Default => new WorklistFromBioHubHistoryItemAnnex2OfSMTA2ConditionConfiguration();
}
