using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistFromBioHubItemLaboratoryFocalPointConfiguration : IEntityTypeConfiguration<WorklistFromBioHubItemLaboratoryFocalPoint>
{
    public void Configure(EntityTypeBuilder<WorklistFromBioHubItemLaboratoryFocalPoint> entity)
    {
        entity.HasKey(wl => new { wl.WorklistFromBioHubItemId, wl.UserId });

        entity.HasOne<WorklistFromBioHubItem>(wl => wl.WorklistFromBioHubItem)
            .WithMany(w => w.WorklistFromBioHubItemLaboratoryFocalPoints)
            .HasForeignKey(wl => wl.WorklistFromBioHubItemId);


        entity.HasOne<User>(wl => wl.User)
            .WithMany(l => l.WorklistFromBioHubItemLaboratoryFocalPoints)
            .HasForeignKey(wl => wl.UserId);

        entity.Property(e => e.Other)
            .HasMaxLength(255)
            .IsRequired(false);
    }

    public new static WorklistFromBioHubItemLaboratoryFocalPointConfiguration Default => new WorklistFromBioHubItemLaboratoryFocalPointConfiguration();
}
