using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistToBioHubItemBioHubFacilityFocalPointConfiguration : IEntityTypeConfiguration<WorklistToBioHubItemBioHubFacilityFocalPoint>
{
    public void Configure(EntityTypeBuilder<WorklistToBioHubItemBioHubFacilityFocalPoint> entity)
    {
        entity.HasKey(wl => new { wl.WorklistToBioHubItemId, wl.UserId });

        entity.HasOne<WorklistToBioHubItem>(wl => wl.WorklistToBioHubItem)
            .WithMany(w => w.WorklistToBioHubItemBioHubFacilityFocalPoints)
            .HasForeignKey(wl => wl.WorklistToBioHubItemId);


        entity.HasOne<User>(wl => wl.User)
            .WithMany(l => l.WorklistToBioHubItemBioHubFacilityFocalPoints)
            .HasForeignKey(wl => wl.UserId);

        entity.Property(e => e.Other)
            .HasMaxLength(255)
            .IsRequired(false);
    }

    public new static WorklistToBioHubItemBioHubFacilityFocalPointConfiguration Default => new WorklistToBioHubItemBioHubFacilityFocalPointConfiguration();
}
