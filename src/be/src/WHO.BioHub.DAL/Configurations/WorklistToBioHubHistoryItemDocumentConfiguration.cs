using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class WorklistToBioHubHistoryItemDocumentConfiguration : IEntityTypeConfiguration<WorklistToBioHubHistoryItemDocument>
{
    public void Configure(EntityTypeBuilder<WorklistToBioHubHistoryItemDocument> entity)
    {
        entity.HasKey(wd => new { wd.WorklistToBioHubHistoryItemId, wd.DocumentId });

        entity.Property(wd => wd.IsDocumentFile)
           .IsRequired(false);

        entity.HasOne<WorklistToBioHubHistoryItem>(wd => wd.WorklistToBioHubHistoryItem)
            .WithMany(r => r.WorklistToBioHubHistoryItemDocuments)
            .HasForeignKey(wd => wd.WorklistToBioHubHistoryItemId);

        entity.HasOne<Document>(wd => wd.Document)
            .WithMany(d => d.WorklistToBioHubHistoryItemDocuments)
            .HasForeignKey(wd => wd.DocumentId);
    }

    public new static WorklistToBioHubHistoryItemDocumentConfiguration Default => new WorklistToBioHubHistoryItemDocumentConfiguration();
}
