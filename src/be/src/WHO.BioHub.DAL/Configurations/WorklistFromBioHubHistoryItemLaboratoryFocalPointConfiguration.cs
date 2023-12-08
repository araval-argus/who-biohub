using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistFromBioHubHistoryItemLaboratoryFocalPointConfiguration : IEntityTypeConfiguration<WorklistFromBioHubHistoryItemLaboratoryFocalPoint>
{
    public void Configure(EntityTypeBuilder<WorklistFromBioHubHistoryItemLaboratoryFocalPoint> entity)
    {
        entity.HasKey(wl => new { wl.WorklistFromBioHubHistoryItemId, wl.UserId });

        entity.HasOne<WorklistFromBioHubHistoryItem>(wl => wl.WorklistFromBioHubHistoryItem)
            .WithMany(w => w.WorklistFromBioHubHistoryItemLaboratoryFocalPoints)
            .HasForeignKey(wl => wl.WorklistFromBioHubHistoryItemId);


        entity.HasOne<User>(wl => wl.User)
            .WithMany(l => l.WorklistFromBioHubHistoryItemLaboratoryFocalPoints)
            .HasForeignKey(wl => wl.UserId);

        entity.Property(e => e.Other)
            .HasMaxLength(255)
            .IsRequired(false);
    }

    public new static WorklistFromBioHubItemLaboratoryFocalPointConfiguration Default => new WorklistFromBioHubItemLaboratoryFocalPointConfiguration();
}
