using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class SMTA2WorkflowHistoryItemDocumentConfiguration : IEntityTypeConfiguration<SMTA2WorkflowHistoryItemDocument>
{
    public void Configure(EntityTypeBuilder<SMTA2WorkflowHistoryItemDocument> entity)
    {
        entity.HasKey(wd => new { wd.SMTA2WorkflowHistoryItemId, wd.DocumentId });

        entity.Property(wd => wd.IsDocumentFile)
           .IsRequired(false);

        entity.HasOne<SMTA2WorkflowHistoryItem>(wd => wd.SMTA2WorkflowHistoryItem)
            .WithMany(r => r.SMTA2WorkflowHistoryItemDocuments)
            .HasForeignKey(wd => wd.SMTA2WorkflowHistoryItemId);

        entity.HasOne<Document>(wd => wd.Document)
            .WithMany(d => d.SMTA2WorkflowHistoryItemDocuments)
            .HasForeignKey(wd => wd.DocumentId);
    }

    public new static SMTA2WorkflowHistoryItemDocumentConfiguration Default => new SMTA2WorkflowHistoryItemDocumentConfiguration();
}
