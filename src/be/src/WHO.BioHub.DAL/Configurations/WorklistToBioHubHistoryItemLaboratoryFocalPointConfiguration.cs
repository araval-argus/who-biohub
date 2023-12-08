using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistToBioHubHistoryItemLaboratoryFocalPointConfiguration : IEntityTypeConfiguration<WorklistToBioHubHistoryItemLaboratoryFocalPoint>
{
    public void Configure(EntityTypeBuilder<WorklistToBioHubHistoryItemLaboratoryFocalPoint> entity)
    {
        entity.HasKey(wl => new { wl.WorklistToBioHubHistoryItemId, wl.UserId });

        entity.HasOne<WorklistToBioHubHistoryItem>(wl => wl.WorklistToBioHubHistoryItem)
            .WithMany(w => w.WorklistToBioHubHistoryItemLaboratoryFocalPoints)
            .HasForeignKey(wl => wl.WorklistToBioHubHistoryItemId);


        entity.HasOne<User>(wl => wl.User)
            .WithMany(l => l.WorklistToBioHubHistoryItemLaboratoryFocalPoints)
            .HasForeignKey(wl => wl.UserId);

        entity.Property(e => e.Other)
            .HasMaxLength(255)
            .IsRequired(false);
    }

    public new static WorklistToBioHubItemLaboratoryFocalPointConfiguration Default => new WorklistToBioHubItemLaboratoryFocalPointConfiguration();
}
