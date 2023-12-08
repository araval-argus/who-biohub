using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DAL.Configurations;

public class SMTA1WorkflowHistoryItemDocumentConfiguration : IEntityTypeConfiguration<SMTA1WorkflowHistoryItemDocument>
{
    public void Configure(EntityTypeBuilder<SMTA1WorkflowHistoryItemDocument> entity)
    {
        entity.HasKey(wd => new { wd.SMTA1WorkflowHistoryItemId, wd.DocumentId });

        entity.Property(wd => wd.IsDocumentFile)
           .IsRequired(false);

        entity.HasOne<SMTA1WorkflowHistoryItem>(wd => wd.SMTA1WorkflowHistoryItem)
            .WithMany(r => r.SMTA1WorkflowHistoryItemDocuments)
            .HasForeignKey(wd => wd.SMTA1WorkflowHistoryItemId);

        entity.HasOne<Document>(wd => wd.Document)
            .WithMany(d => d.SMTA1WorkflowHistoryItemDocuments)
            .HasForeignKey(wd => wd.DocumentId);
    }

    public new static SMTA1WorkflowHistoryItemDocumentConfiguration Default => new SMTA1WorkflowHistoryItemDocumentConfiguration();
}
