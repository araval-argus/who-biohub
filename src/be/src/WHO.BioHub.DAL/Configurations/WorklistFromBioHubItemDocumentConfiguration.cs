using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistFromBioHubItemDocumentConfiguration : IEntityTypeConfiguration<WorklistFromBioHubItemDocument>
{
    public void Configure(EntityTypeBuilder<WorklistFromBioHubItemDocument> entity)
    {
        entity.HasKey(wd => new { wd.WorklistFromBioHubItemId, wd.DocumentId });

        entity.Property(wd => wd.IsDocumentFile)
            .IsRequired(false);

        entity.HasOne<WorklistFromBioHubItem>(wd => wd.WorklistFromBioHubItem)
            .WithMany(r => r.WorklistFromBioHubItemDocuments)
            .HasForeignKey(wd => wd.WorklistFromBioHubItemId);


        entity.HasOne<Document>(wd => wd.Document)
            .WithMany(d => d.WorklistFromBioHubItemDocuments)
            .HasForeignKey(wd => wd.DocumentId);
    }

    public new static WorklistFromBioHubItemDocumentConfiguration Default => new WorklistFromBioHubItemDocumentConfiguration();
}
