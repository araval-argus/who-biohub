using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistFromBioHubHistoryItemDocumentConfiguration : IEntityTypeConfiguration<WorklistFromBioHubHistoryItemDocument>
{
    public void Configure(EntityTypeBuilder<WorklistFromBioHubHistoryItemDocument> entity)
    {
        entity.HasKey(wd => new { wd.WorklistFromBioHubHistoryItemId, wd.DocumentId });

        entity.Property(wd => wd.IsDocumentFile)
           .IsRequired(false);

        entity.HasOne<WorklistFromBioHubHistoryItem>(wd => wd.WorklistFromBioHubHistoryItem)
            .WithMany(r => r.WorklistFromBioHubHistoryItemDocuments)
            .HasForeignKey(wd => wd.WorklistFromBioHubHistoryItemId);

        entity.HasOne<Document>(wd => wd.Document)
            .WithMany(d => d.WorklistFromBioHubHistoryItemDocuments)
            .HasForeignKey(wd => wd.DocumentId);
    }

    public new static WorklistFromBioHubHistoryItemDocumentConfiguration Default => new WorklistFromBioHubHistoryItemDocumentConfiguration();
}
