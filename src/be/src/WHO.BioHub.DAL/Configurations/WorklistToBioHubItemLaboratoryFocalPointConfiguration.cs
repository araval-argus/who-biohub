using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistToBioHubItemLaboratoryFocalPointConfiguration : IEntityTypeConfiguration<WorklistToBioHubItemLaboratoryFocalPoint>
{
    public void Configure(EntityTypeBuilder<WorklistToBioHubItemLaboratoryFocalPoint> entity)
    {
        entity.HasKey(wl => new { wl.WorklistToBioHubItemId, wl.UserId });

        entity.HasOne<WorklistToBioHubItem>(wl => wl.WorklistToBioHubItem)
            .WithMany(w => w.WorklistToBioHubItemLaboratoryFocalPoints)
            .HasForeignKey(wl => wl.WorklistToBioHubItemId);


        entity.HasOne<User>(wl => wl.User)
            .WithMany(l => l.WorklistToBioHubItemLaboratoryFocalPoints)
            .HasForeignKey(wl => wl.UserId);

        entity.Property(e => e.Other)
            .HasMaxLength(255)
            .IsRequired(false);
    }

    public new static WorklistToBioHubItemLaboratoryFocalPointConfiguration Default => new WorklistToBioHubItemLaboratoryFocalPointConfiguration();
}
